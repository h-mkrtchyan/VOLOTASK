//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Books_AttributesValue
    {
        public int ID { get; set; }
        public int BooksID { get; set; }
        public int AttributesID { get; set; }
        public string Value { get; set; }
    
        public virtual Attribute Attribute { get; set; }
        public virtual Book Book { get; set; }
    }
}
