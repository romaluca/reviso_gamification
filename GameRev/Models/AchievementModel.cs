using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRev.Models
{
    public class AchievementModel
    {
        public int ID { get; set; }
        public DateTime ACHIEV_DATE { get; set; }
        public Byte NOTIFIED { get; set; }      
        public string ACHIEVEMENT_TITLE { get; set; }
        public byte GOAL_LEVEL { get; set; }
        public string ACHIEVEMENT_MESSAGE { get; set; }  
        public byte NOTIFICATION_TYPE { get; set; }
        public string NOTIFICATION_IMAGE { get; set; }

    }
}