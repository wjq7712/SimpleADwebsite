//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LNCDCDSS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatRecentDrug
    {
        public PatRecentDrug()
        {
            this.Drug = new HashSet<Drug>();
        }
    
        public int Id { get; set; }
        public string Drugcatogary { get; set; }
        public int VisitRecordId { get; set; }
    
        public virtual VisitRecord VisitRecord { get; set; }
        public virtual ICollection<Drug> Drug { get; set; }
    }
}