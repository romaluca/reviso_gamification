using GameRev.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameRev.Controllers
{
    public class GoalsApiController : ApiController
    {
        // GET: api/GoalsApi
        public IEnumerable<GoalModel> Get()
        {
            string sql = @"SELECT * FROM GOALS";

            List<GoalModel> goalsList = new List<GoalModel>();

            using (SqlConnection con = new SqlConnection(DBSettings.ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;

                using (DataSet ds = new DataSet())
                {
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        foreach (DataRow row in dt.Rows)
                        {
                            GoalModel newGoal = new GoalModel();

                            newGoal.ID = (int)row["ID"];
                            newGoal.NAME = (string)row["NAME"];
                            newGoal.DESCR = (string)row["DESCR"];

                            if (row["USR_ID"] != System.DBNull.Value)
                                newGoal.USR_ID = (int)row["USR_ID"];
                            if (row["USR_GROUP_ID"] != System.DBNull.Value)
                                newGoal.USR_GROUP_ID = (int?)row["USR_GROUP_ID"];
                            if (row["USR_TYPE_ID"] != System.DBNull.Value)
                                newGoal.USR_TYPE_ID = (int?)row["USR_TYPE_ID"];
                            newGoal.FORMULA_TARGET = (string)row["FORMULA_TARGET"];
                            if (row["NOTIFICATION_TYPE"] != System.DBNull.Value)
                                newGoal.NOTIFICATION_TYPE = (byte)row["NOTIFICATION_TYPE"];
                            if (row["NOTIFICATION_IMAGE"] != System.DBNull.Value)
                                newGoal.NOTIFICATION_IMAGE = (string)row["NOTIFICATION_IMAGE"];

                            newGoal.RULES = GetRules(newGoal.ID);

                            goalsList.Add(newGoal);
                        }
                    }
                }

                con.Close();
            }


            return goalsList; //JsonConvert.SerializeObject(goalsList, Formatting.Indented);
        }

        private IEnumerable<GoalRuleModel> GetRules(int goalId)
        {
            string sql = @"SELECT * FROM GOAL_RULES WHERE GOAL_ID = " + goalId;

            List<GoalRuleModel> rules = new List<GoalRuleModel>();

            using (SqlConnection con = new SqlConnection(DBSettings.ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;

                using (DataSet ds = new DataSet())
                {
                    adapter.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        foreach (DataRow row in dt.Rows)
                        {
                            GoalRuleModel rule = new GoalRuleModel();

                            rule.ID = (int)row["ID"];
                            rule.FORMULA = (string)row["FORMULA"];
                            rule.GOAL_LEVEL = (byte)row["GOAL_LEVEL"];
                            rule.ACHIEVEMENT_TITLE = (string)row["ACHIEVEMENT_TITLE"];
                            if (row["ACHIEVEMENT_MESSAGE"] != System.DBNull.Value)
                                rule.ACHIEVEMENT_MESSAGE = (string)row["ACHIEVEMENT_MESSAGE"];


                            rules.Add(rule);
                        }
                    }
                }

                con.Close();
            }


            return rules;
        }

        // GET: api/GoalsApi/5
        public GoalModel Get(int id)
        {
            return Get().Where(g => g.ID == id).FirstOrDefault();
        }

        // POST: api/GoalsApi
        public void Post([FromBody]string value)
        {

            GoalModel goalModel = JsonConvert.DeserializeObject<GoalModel>(value);
            if (goalModel != null)
            {
                string sqlInsertGaol = @"
INSERT INTO [dbo].[GOALS]
           ([NAME]
           ,[DESCR]
           ,[USR_ID]
           ,[USR_GROUP_ID]
           ,[USR_TYPE_ID]
           ,[FORMULA_TARGET]
           ,[NOTIFICATION_TYPE]
           ,[NOTIFICATION_IMAGE])
     VALUES
           ('" + goalModel.NAME + @"'
           ,'" + goalModel.DESCR + @"'
           ," + ((goalModel.USR_ID.HasValue) ? goalModel.USR_ID.Value.ToString() : "NULL") + @"
           ," + ((goalModel.USR_GROUP_ID.HasValue) ? goalModel.USR_GROUP_ID.Value.ToString() : "NULL") + @"
            ," + ((goalModel.USR_TYPE_ID.HasValue) ? goalModel.USR_TYPE_ID.Value.ToString() : "NULL") + @"
           ,'" + goalModel.FORMULA_TARGET.ToString() + @"'
           ," + goalModel.NOTIFICATION_TYPE.ToString() + @"
           ,'" + ((goalModel.USR_TYPE_ID.HasValue) ? goalModel.USR_TYPE_ID.Value.ToString() : "NULL") + @"')";


                using (SqlConnection con = new SqlConnection(DBSettings.ConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand(sqlInsertGaol);
                    cmd.Connection = con;
                    
                    cmd.ExecuteNonQuery();



     //               foreach(GoalRuleModel rule in goalModel.RULES)
     //               {
     //                   cmd.CommandText = @"INSERT INTO [dbo].[GOAL_RULES]
     //      ([GOAL_ID]
     //      ,[FORMULA]
     //      ,[GOAL_LEVEL]
     //      ,[ACHIEVEMENT_TITLE]
     //      ,[ACHIEVEMENT_MESSAGE])
     //VALUES
     //      (<GOAL_ID, int,>
     //      ,<FORMULA, varchar(max),>
     //      ,<GOAL_LEVEL, tinyint,>
     //      ,<ACHIEVEMENT_TITLE, varchar(1000),>
     //      ,<ACHIEVEMENT_MESSAGE, varchar(max),>)";
     //               }
                }
            }
        }

        // PUT: api/GoalsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GoalsApi/5
        public void Delete(int id)
        {
        }
    }
}
