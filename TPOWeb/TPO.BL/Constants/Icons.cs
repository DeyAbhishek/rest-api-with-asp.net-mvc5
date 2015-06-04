using System;
using System.Collections.Generic;

namespace TPO.BL.Constants
{
    class Icons
    {
        //bootstrap build in icons, simply need a span to display.
        public static readonly Dictionary<string, string> ButtonIcons =
            new Dictionary<string, string>{
                {"Save","<span class='glyphicon glyphicon-floppy-saved'></span>"},
                {"Create","<span class='glyphicon glyphicon-plus'></span>"},
                {"Delete","<span class='glyphicon glyphicon-trash'></span>"},
                {"Edit","<span class='glyphicon glyphicon-pencil'></span>"},
                {"Details","<span class='glyphicon glyphicon-eye-open'></span>"},
                {"Cancel","<span class='glyphicon glyphicon-remove'></span>"},
                {"Confirm","<span class='glyphicon glyphicon-ok'></span>"}
            };
    }
}
