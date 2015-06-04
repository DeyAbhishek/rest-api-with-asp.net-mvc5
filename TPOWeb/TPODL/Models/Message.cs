using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.DL.Models
{
    public class Message : IMessage
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
