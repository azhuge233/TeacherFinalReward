
namespace TeacherReward.Models {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using TeacherReward.Models;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;

	public class AllInfo {
		public TeacherInfo TInfo { get; set; }
		public TeacherCrosswiseTaskInfo TCTask { get; set; }
		public TeacherPrizeInfo TPrize { get; set; }
		public TeacherProjectInfo TProject { get; set; }
		public TeacherPublishThesisInfo TPThesis { get; set; }
		public TeacherTaskInfo TTask { get; set; }
		public TeacherTeachThesisInfo TTThesis { get; set; }
		public TeachingAwardInfo TAward { get; set; }
		public Deduction TDeduction { get; set; }
	}
}