using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPO.BL.Constants
{
    public class MessageKeys
    {
        // response messages
        public const string ResponseMessageFailNoId = "ResponseMessageFailNoId";
        public const string ResponseMessageFailNoRecord = "ResponseMessageFailNoRecord";
        public const string ResponseMessageFailSave = "ResponseMessageFailSave";
        public const string ResponseMessageFailDelete = "ResponseMessageFailDelete";
        public const string ResponseMessageSuccessSave = "ResponseMessageSuccessSave";
        public const string ResponseMessageSuccessDelete = "ResponseMessageSuccessDelete";
        public const string ResponseMessageMaterialAssociated = "ResponseMessageMaterialAssociated";

        // response types
        public const string ResponseTypeSuccess = "ResponseTypeSuccess";
        public const string ResponseTypeInfo = "ResponseTypeInfo";
        public const string ResponseTypeError = "ResponseTypeError";
        public const string ResponseTypeWarning = "ResponseTypeWarning";

    }
}
