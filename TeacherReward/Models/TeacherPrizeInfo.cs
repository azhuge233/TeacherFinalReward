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

	public partial class TeacherPrizeInfo {
		public string ID { get; set; }

		[StringLength(20, ErrorMessage = "长度不能大于20个字符")]
		public string Prize1Name { get; set; }
		public string Prize1Level { get; set; }
		public string Prize1Rank { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string Prize1Comment { get; set; }

		[StringLength(20, ErrorMessage = "长度不能大于20个字符")]
		public string Prize2Name { get; set; }
		public string Prize2Level { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string Prize2Comment { get; set; }
		public string Prize2Rank { get; set; }

		[StringLength(20, ErrorMessage = "长度不能大于20个字符")]
		public string Prize3Name { get; set; }
		public string Prize3Level { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string Prize3Comment { get; set; }
		public string Prize3Rank { get; set; }

		[StringLength(20, ErrorMessage = "长度不能大于20个字符")]
		public string Prize4Name { get; set; }
		public string Prize4Level { get; set; }
		public string Prize4Rank { get; set; }
		[StringLength(30, ErrorMessage = "长度不能大于30个字符")]
		public string Prize4Comment { get; set; }
	}
}