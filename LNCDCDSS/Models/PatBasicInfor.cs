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
    
    public partial class PatBasicInfor
    {
        public PatBasicInfor()
        {
            this.VisitRecord = new HashSet<VisitRecord>();
            this.SimpleADdata = new HashSet<SimpleADdata>();
        }
    
        public string Id { get; set; }
        public int DoctorAccountId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string Education { get; set; }
        public string Job { get; set; }
        public string Phone { get; set; }
        public string FamilyMember { get; set; }
        public string ChiefDoctor { get; set; }
    
        public virtual PatDisease PatDisease { get; set; }
        public virtual DoctorAccount DoctorAccount { get; set; }
        public virtual ICollection<VisitRecord> VisitRecord { get; set; }
        public virtual PatPhysicalExam PatPhysicalExam { get; set; }
        public virtual ICollection<SimpleADdata> SimpleADdata { get; set; }
    }
}
