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

	public partial class TeacherTeachThesisInfo {
		public string ID { get; set; }

		public string TeachThesis1Type { get; set; }
		public string TeachThesis1Author { get; set; }
		[StringLength(25, ErrorMessage = "长度不能大于25个字符")]
		public string TeachThesis1Name { get; set; }
		public string TeachThesis1Publisher { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string TeachThesis1Comment { get; set; }

		public string TeachThesis2Type { get; set; }
		public string TeachThesis2Author { get; set; }
		[StringLength(25, ErrorMessage = "长度不能大于25个字符")]
		public string TeachThesis2Name { get; set; }
		public string TeachThesis2Publisher { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string TeachThesis2Comment { get; set; }

		public string TeachThesis3Type { get; set; }
		public string TeachThesis3Author { get; set; }
		[StringLength(25, ErrorMessage = "长度不能大于25个字符")]
		public string TeachThesis3Name { get; set; }
		public string TeachThesis3Publisher { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string TeachThesis3Comment { get; set; }

		public string TeachThesis4Type { get; set; }
		public string TeachThesis4Author { get; set; }
		[StringLength(25, ErrorMessage = "长度不能大于25个字符")]
		public string TeachThesis4Name { get; set; }
		public string TeachThesis4Publisher { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string TeachThesis4Comment { get; set; }
	}
}
