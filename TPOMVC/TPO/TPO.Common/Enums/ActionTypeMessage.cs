using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.Common.Enums
{
    public enum ActionTypeMessage
    {
        //Most used
        SuccessfulSave,
        SuccessfulDelete,
        FailedSave,
        FailedDelete, 

        //Generic 
        Info,
        Success,
        Warning,
        Error
    }
}
