using GameRev.Models;
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
    public class AchievementsApiController : ApiController
    {
        // GET: api/AchievementsApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AchievementsApi/5
        public IEnumerable<AchievementModel> Get(int id)
        {
            string sql = @"SELECT
      USER_ACHIEVEMENTS.ID AS USER_ACHIEVEMENTS_ID,
      USER_ACHIEVEMENTS.ACHIEV_DATE AS USER_ACHIEVEMENTS_ACHIEV_DATE,
      USER_ACHIEVEMENTS.NOTIFIED AS USER_ACHIEVEMENTS_NOTIFIED,
      GOAL_RULES.GOAL_LEVEL AS GOAL_RULES_GOAL_LEVEL, 
      GOAL_RULES.ACHIEVEMENT_TITLE AS GOAL_RULES_ACHIEVEMENT_TITLE,
      GOAL_RULES.ACHIEVEMENT_MESSAGE AS GOAL_RULES_ACHIEVEMENT_MESSAGE,
      GOALS.NOTIFICATION_TYPE AS GOALS_NOTIFICATION_TYPE,
      GOALS.NOTIFICATION_IMAGE AS GOALS_NOTIFICATION_IMAGE

FROM

    USER_ACHIEVEMENTS
INNER JOIN
    GOAL_RULES
ON

    USER_ACHIEVEMENTS.GOAL_RULE_ID = GOAL_RULES.ID
INNER JOIN

    GOALS
ON

    GOAL_RULES.GOAL_ID = GOALS.ID
WHERE

    USER_ACHIEVEMENTS.USR_ID = " + id + @"

    AND USER_ACHIEVEMENTS.NOTIFIED = 0-- COMMENTARE PER ELENCO ACHIEVEMENTS COMPLETO
   AND GOALS.NOTIFICATION_TYPE = 0-- COMMENTARE PER ELENCO ACHIEVEMENTS COMPLETO";



            List<AchievementModel> achievList = new List<AchievementModel>();

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
                            AchievementModel achiev = new AchievementModel();

                            achiev.ID = (int)row["USER_ACHIEVEMENTS_ID"];
                            achiev.ACHIEV_DATE = (DateTime )row["USER_ACHIEVEMENTS_ACHIEV_DATE"];
                            achiev.NOTIFIED = (Byte)row["USER_ACHIEVEMENTS_NOTIFIED"];
                            achiev.GOAL_LEVEL = (Byte)row["GOAL_RULES_GOAL_LEVEL"];
                            achiev.ACHIEVEMENT_TITLE = (string)row["GOAL_RULES_ACHIEVEMENT_TITLE"];

                            if (row["GOAL_RULES_ACHIEVEMENT_MESSAGE"] != System.DBNull.Value)
                                achiev.ACHIEVEMENT_MESSAGE = (string)row["GOAL_RULES_ACHIEVEMENT_MESSAGE"];

                            achiev.NOTIFICATION_TYPE = (byte)row["GOALS_NOTIFICATION_TYPE"];

                            if (row["GOALS_NOTIFICATION_IMAGE"] != System.DBNull.Value)
                                achiev.NOTIFICATION_IMAGE = (string)row["GOALS_NOTIFICATION_IMAGE"];                                                   
                            
                            achievList.Add(achiev);
                        }
                    }
                }

                con.Close();
            }


            return achievList;

        }

        // POST: api/AchievementsApi
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/AchievementsApi/5
        public void Put(int id, [FromBody]string value)
        {

            string sql = @"	UPDATE USER_ACHIEVEMENTS 
	                        SET NOTIFIED = 1
	                        WHERE ID = @id ";
            using (SqlConnection con = new SqlConnection(DBSettings.ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.VarChar)).Value = id;
                cmd.ExecuteNonQuery();
            }

        }

        // DELETE: api/AchievementsApi/5
        public void Delete(int id)
        {
        }
    }
}
