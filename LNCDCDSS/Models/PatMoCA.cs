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
    
    public partial class PatMoCA
    {
        public int Id { get; set; }
        public string MC1 { get; set; }
        public string MC2 { get; set; }
        public string MC3 { get; set; }
        public string MC4 { get; set; }
        public string MC5 { get; set; }
        public string MC6 { get; set; }
        public string MC7 { get; set; }
        public string MC8 { get; set; }
        public string MC9 { get; set; }
        public string Total { get; set; }
    
        public virtual VisitRecord VisitRecord { get; set; }
    }
}
