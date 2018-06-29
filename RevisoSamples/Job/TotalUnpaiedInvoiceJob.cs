using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RevisoSamples.DTO;
using RevisoSamples.Rest;
using RevisoScheduler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;

namespace RevisoSamples.Job
{
    public class TotalUnpaiedInvoiceJob : RevisoJob
    {
        public override void DoJob()
        {
            Console.WriteLine("Starting TotalUpaiedInvoiceJob...");
            RestClient client = RevisoRestClient.CreateClient();
            RestRequest request = new RestRequest("v2/invoices/unpaid?pageSize=10000", Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                string err = String.Format("{0} => {1}", response.StatusCode, response.ErrorMessage);
                throw new InvalidOperationException(err);
            }

            string json = response.Content;
            // deserializzazione e test delle proprietà principali
            JsonObject dto = JsonConvert.DeserializeObject<JsonObject>(json);
            JArray array = dto["collection"] as JArray;
            if (array == null)
                throw new InvalidOperationException("array");

            List<Invoice> listInvoice = JsonConvert.DeserializeObject<List<Invoice>>(array.ToString());
            AchievementLevel level = Achievements.ElaborateTotalUnpaied2018Invoice(listInvoice);
            string connectionString = "Data Source=NB-FMARCHETTI2;Initial Catalog=HACKNIGHT_TSPESARO;Persist Security Info=True;User ID=sa;Password=teamsystem";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    int goalRuleId = GetUpaiedGoalRuleByLevel(level);
                    string sqlCommand = null;
                    if (level == AchievementLevel.Zero)
                    {
                        sqlCommand = "DELETE FROM USER_ACHIEVEMENTS WHERE USR_ID = 1 AND GOAL_RULE_ID IN (10, 11, 12)";
                    }
                    else if (level == AchievementLevel.One)
                    {
                        sqlCommand = String.Format(@"DELETE FROM USER_ACHIEVEMENTS WHERE USR_ID = 1 AND GOAL_RULE_ID IN(11, 12)
IF NOT EXISTS (SELECT TOP 1 1 FROM USER_ACHIEVEMENTS WHERE USR_ID = 1 AND GOAL_RULE_ID = {0})
BEGIN
    INSERT INTO USER_ACHIEVEMENTS (USR_ID, GOAL_RULE_ID) VALUES (1, {0})
END;", goalRuleId);
                    }
                    else if (level == AchievementLevel.Two)
                    {
                        sqlCommand = String.Format(@"DELETE FROM USER_ACHIEVEMENTS WHERE USR_ID = 1 AND GOAL_RULE_ID IN(12)
IF NOT EXISTS (SELECT TOP 1 1 FROM USER_ACHIEVEMENTS WHERE USR_ID = 1 AND GOAL_RULE_ID = {0})
BEGIN
    INSERT INTO USER_ACHIEVEMENTS (USR_ID, GOAL_RULE_ID) VALUES (1, {0})
END;", goalRuleId);
                    }
                    else
                    {
                        sqlCommand = String.Format(@"IF NOT EXISTS (SELECT TOP 1 1 FROM USER_ACHIEVEMENTS WHERE USR_ID = 1 AND GOAL_RULE_ID = {0})
BEGIN
    INSERT INTO USER_ACHIEVEMENTS (USR_ID, GOAL_RULE_ID) VALUES (1, {0})
END;", goalRuleId);
                    }
                    command.CommandText = sqlCommand;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private int GetUpaiedGoalRuleByLevel(AchievementLevel level)
        {
            if (level == AchievementLevel.One)
                return 10;
            if (level == AchievementLevel.Two)
                return 11;
            if (level == AchievementLevel.Three)
                return 12;
            throw new InvalidOperationException("level out of range!");
        }

        public override string GetName()
        {
            return "TotalUnpaiedInvoiceJob";
        }

        public override int GetRepetitionIntervalTime()
        {
            return 5000;
        }

        public override bool IsRepeatable()
        {
            return true;
        }
    }
}