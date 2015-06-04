using System;

namespace TPO.Common.DTOs
{
    public class UnitOfMeasureTypeDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public DateTime DateEntered { get; set; }
        public string Enteredby { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }

        #region Code Constants
        public const string AREA = "A";
        public const string COUNT = "C";
        public const string FORCE = "F";
        public const string LENGTH = "L";
        public const string SPEED = "S";
        public const string THICKNESS = "T";
        public const string WEIGHT = "W";
        public const string TEMPERATURE = "Y";
        public const string WIDTH = "D";

        #endregion
    }
}
