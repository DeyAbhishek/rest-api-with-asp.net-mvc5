using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPO.Web.Core
{
    public class ResponseMessage
    {
        public string ActionMessage { get; set; }
        public string ActionType { get; set; }
        public string ActionStatus { get; set; }
    }
}