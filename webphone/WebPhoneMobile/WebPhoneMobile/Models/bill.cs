//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebPhoneMobile.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bill()
        {
            this.billdetail = new HashSet<billdetail>();
        }
    
        public int bill_id { get; set; }
        public string client_id { get; set; }
        public System.DateTime date_order { get; set; }
        public string descriptions { get; set; }
    
        public virtual client client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<billdetail> billdetail { get; set; }
    }
}