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

	public partial class TeacherPublishThesisInfo {
		public string ID { get; set; }

		[StringLength(25, ErrorMessage = "长度不能大于25个字符")]
		public string PublishThesis1Name { get; set; }
		[StringLength(20, ErrorMessage = "长度不能大于20个字符")]
		public string PublishThesis1Time { get; set; }
		public string PublishThesis1Type { get; set; }
		public string PublishThesis1Author { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string PublishThesis1Comment { get; set; }

		[StringLength(25, ErrorMessage = "长度不能大于25个字符")]
		public string PublishThesis2Name { get; set; }
		[StringLength(20, ErrorMessage = "长度不能大于20个字符")]
		public string PublishThesis2Time { get; set; }
		public string PublishThesis2Type { get; set; }
		public string PublishThesis2Author { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string PublishThesis2Comment { get; set; }

		[StringLength(25, ErrorMessage = "长度不能大于25个字符")]
		public string PublishThesis3Name { get; set; }
		[StringLength(20, ErrorMessage = "长度不能大于20个字符")]
		public string PublishThesis3Time { get; set; }
		public string PublishThesis3Type { get; set; }
		public string PublishThesis3Author { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string PublishThesis3Comment { get; set; }

		[StringLength(25, ErrorMessage = "长度不能大于25个字符")]
		public string PublishThesis4Name { get; set; }
		[StringLength(20, ErrorMessage = "长度不能大于20个字符")]
		public string PublishThesis4Time { get; set; }
		public string PublishThesis4Type { get; set; }
		public string PublishThesis4Author { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string PublishThesis4Comment { get; set; }
	}
}
