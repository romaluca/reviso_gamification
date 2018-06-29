using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRev.Utils
{
    public static class WebApiHelper
    {
        public static string BaseUri
        {
            get { return "http://localhost:6589/api"; }
        }
    }
}