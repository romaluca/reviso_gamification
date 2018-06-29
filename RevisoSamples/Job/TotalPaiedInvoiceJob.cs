using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RevisoSamples.DTO;
using RevisoSamples.Rest;
using RevisoScheduler;
using System;
using System.Collections.Generic;
using System.Net;
using System.Data.SqlClient;

namespace RevisoSamples.Job
{
    public class TotalPaiedInvoiceJob : RevisoJob
    {
        public override void DoJob()
        {
            Console.WriteLine("Starting TotalPaiedInvoiceJob...");
            RestClient client = RevisoRestClient.CreateClient();
            RestRequest request = new RestRequest("v2/invoices/paid?pageSize=10000", Method.GET);
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
            AchievementLevel level = Achievements.ElaborateTotalPaied2018Invoice(listInvoice);
            string connectionString = "Data Source=NB-FMARCHETTI2;Initial Catalog=HACKNIGHT_TSPESARO;Persist Security Info=True;User ID=sa;Password=teamsystem";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    int goalRuleId = 13;
                    string sqlCommand = null;
                    if (level == AchievementLevel.Zero)
                    {
                        sqlCommand = String.Format("DELETE FROM USER_ACHIEVEMENTS WHERE USR_ID = 1 AND GOAL_RULE_ID = {0}", goalRuleId);
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

        public override string GetName()
        {
            return "TotalPaiedInvoiceJob";
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