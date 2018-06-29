using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameRev.Models
{
    //ID INT IDENTITY,
    //NAME VARCHAR(100) NOT NULL,
    //DESCR VARCHAR(MAX) NOT NULL,
    //USR_ID INT NULL,
    //USR_GROUP_ID INT NULL,
    //USR_TYPE_ID INT NULL,
    //FORMULA_TARGET VARCHAR(1000) NOT NULL,
    //NOTIFICATION_TYPE TINYINT NOT NULL, -- 0=APP NOTIFICATION, 1=WHATSAPP MESSAGE, 2=EMAIL
    //NOTIFICATION_IMAGE VARCHAR(1000) NULL -- PATH

    public class GoalModel
    {
        private static Dictionary<byte, string> listItems;


        static GoalModel()
        {
            listItems = new Dictionary<byte, string>();
            listItems.Add(0, "App notification");
            listItems.Add(1, "WhatsApp");
            listItems.Add(2, "E-mail");
        }

        public GoalModel()
        {
            RULES = new List<GoalRuleModel>();
        }



        public static string DecodeNotificationTypeDescr(GoalModel m)
        {
            return listItems[m.NOTIFICATION_TYPE];
        }
               

        public int ID { get; set; }

        [Required]
        [DisplayName("Nome")]
        public string NAME { get; set; }

        [Required]
        [DisplayName("Descrizione")]
        public string DESCR { get; set; }

        [DisplayName("ID utente")]
        public int? USR_ID { get; set; }

        [DisplayName("ID gruppo utenti")]
        public int? USR_GROUP_ID { get; set; }

        [DisplayName("Tipo utente")]
        public int? USR_TYPE_ID { get; set; }

        [Required]
        [DisplayName("Reviso target object")]
        public string FORMULA_TARGET { get; set; }

        [Required]
        [DisplayName("Tipo notifica")]
        public byte NOTIFICATION_TYPE { get; set; }
        
        [DisplayName("Immagine notifica")]
        public string NOTIFICATION_IMAGE { get; set; }

        public IEnumerable<GoalRuleModel> RULES { get; set; }

    }


    // ID INT IDENTITY,
    //GOAL_ID INT NOT NULL,
    //         FORMULA VARCHAR(MAX) NOT NULL,
    //         GOAL_LEVEL TINYINT NOT NULL DEFAULT 0,
    //ACHIEVEMENT_TITLE VARCHAR(1000) NOT NULL,
    //         ACHIEVEMENT_MESSAGE VARCHAR(MAX) NULL -- HTML FORMATTED
    public class GoalRuleModel
    {
        public int IsPersisted { get; set; }

        public int IsDeleted { get; set; }

        public int ID { get; set; }

        [Required]
        [DisplayName("Formula")]
        public string FORMULA { get; set; }

        [Required]
        [DisplayName("Titolo")]
        public string ACHIEVEMENT_TITLE { get; set; }

        [Required]
        [DisplayName("Livello")]
        public byte GOAL_LEVEL { get; set; }

        [Required]
        [DisplayName("Messaggio")]
        public string ACHIEVEMENT_MESSAGE { get; set; }
    }
}