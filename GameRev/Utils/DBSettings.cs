using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRev
{
    public static class DBSettings
    {
        private static string _connectionString = "Data Source=**REMOVED**;Initial Catalog=HACKNIGHT_TSPESARO;Persist Security Info=True;User ID=**REMOVED**;Password=**REMOVED**";
        public static string ConnectionString { get { return _connectionString; } }
    }
}