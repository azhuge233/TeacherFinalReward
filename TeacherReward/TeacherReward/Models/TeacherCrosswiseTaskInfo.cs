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
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

	public partial class TeacherCrosswiseTaskInfo
    {
        public string ID { get; set; }
		[MaxLength(25, ErrorMessage = "最大长度25个字符")]
        public string CTaskName { get; set; }
        public string CTaskLevel { get; set; }
		[MaxLength(20, ErrorMessage = "最大长度20个字符")]
		public string CTaskStartTime { get; set; }
		[MaxLength(20, ErrorMessage = "最大长度20个字符")]
		public string CTaskEndTime { get; set; }
    }
}
