//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeacherReward.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeacherScore
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Depart { get; set; }
        public double KPIScore { get; set; }
        public double CPIScore { get; set; }
        public double FinalScore { get; set; }
        public bool isSelected { get; set; }
    
        public virtual Department Department { get; set; }
    }
}
