using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.DL.Models
{
    public interface IMessage
    {
        string Name { get; set; }
        string Value { get; set; }
    }
}
