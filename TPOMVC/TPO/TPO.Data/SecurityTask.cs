//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TPO.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class SecurityTask
    {
        public SecurityTask()
        {
            this.webpages_Roles = new HashSet<webpages_Roles>();
        }
    
        public int SecurityTaskId { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}