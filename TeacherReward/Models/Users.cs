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

	public partial class Users {
		[DisplayName("用户名：")]
		[Required(ErrorMessage = "请输入用户名")]
		public string ID { get; set; }

		public string Name { get; set; }

		public int Department { get; set; }

		[DisplayName("密码：")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "请输入密码")]
		public string Password { get; set; }

		[DisplayName("新密码：")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "请输入新密码")]
		[StringLength(16, ErrorMessage = "密码长度不能小于8位", MinimumLength = 8)]
		public string NewPassword { get; set; }

		[DisplayName("确认密码：")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "请再次输入密码")]
		[Compare("NewPassword", ErrorMessage = "密码不一致")]
		public string ConfirmPassword { get; set; }

		public bool isAdmin { get; set; }

		public string ErrMsg { get; set; }

		public virtual Department Department1 { get; set; }
	}
}
