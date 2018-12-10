using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherReward.Models;

namespace TeacherReward.Controllers {
	public class HomeController : Controller {
		private Users userDetails;
		private Department userDepart;
		private const int BASETIME = 420; //基本学时
		private const int MAXINPUT = 4; //最大填入的信息数目，以便之后加输入框

		/// <summary>
		/// 检查权限
		/// </summary>
		private int Check() {
			if (Session["userID"] == null) {
				return 1;
			} else if (Session["isAdmin"].ToString() == "True") {
				return 2;
			} else return 0;
		}

		/// <summary>
		/// 获取当前用户的用户信息和部门信息
		/// </summary>
		private void CommonWork() {
			TeacherRewardEntities db = new TeacherRewardEntities();

			string userID = Session["userID"].ToString();
			this.userDetails = db.Users.Where(x => x.ID == userID).FirstOrDefault();

			int userdp = this.userDetails.Department;
			this.userDepart = db.Department.Where(x => x.ID == userdp).FirstOrDefault();

			ViewBag.curUserID = this.userDetails.ID.ToString();
			ViewBag.curUserName = this.userDetails.Name.ToString();
			ViewBag.userDepart = this.userDepart.Name.ToString();

			db.Dispose();
		}

		/// <summary>
		/// 取得当前最大值
		/// </summary>
		private string[] getHighSocores() {
			string[] Score = new string[6];
			TeacherRewardEntities db = new TeacherRewardEntities();
			var ThisDepartInfos = db.TeacherInfo.Where(x => x.Depart == this.userDepart.ID);
			if (ThisDepartInfos.FirstOrDefault() == null) {
				Score[0] = Score[1] = Score[2] = Score[3] = Score[4] = Score[5] = "无信息";
				return Score;
			} else {
				Score[0] = ThisDepartInfos.OrderByDescending(x => x.extTeachingTime).First().extTeachingTime.ToString();
				Score[1] = ThisDepartInfos.OrderByDescending(x => x.Prize).First().Prize.ToString();
				Score[2] = ThisDepartInfos.OrderByDescending(x => x.extProject).First().extProject.ToString();
				Score[3] = ThisDepartInfos.OrderByDescending(x => x.extTeachThesis).First().extTeachThesis.ToString();
				Score[4] = ThisDepartInfos.OrderByDescending(x => x.extPublishThesis).First().extPublishThesis.ToString();
				Score[5] = ThisDepartInfos.OrderByDescending(x => x.extTask).First().extTask.ToString();

				return Score;
			}
		}

		/// <summary>
		/// 以下get函数为DropdownList准备字典
		/// </summary>
		/// 
		private void getDepartmentList() {
			TeacherRewardEntities db = new TeacherRewardEntities();
			Dictionary<int, string> dic = new Dictionary<int, string> {
				{ 0, "全部" }
			};
			var departs = db.Department.Select(x => x);
			foreach (var i in departs) {
				dic.Add(i.ID, i.Name);
			}
			ViewBag.Department = dic;
			ViewBag.SearchByDepart = new SelectList(dic, "Key", "Value");
			db.Dispose();
		}

		private void getSortList() {
			var orderBylist = new Dictionary<string, string> {
				{ "不排序", "none" },
				{ "升序排序", "low" },
				{ "降序排序", "high"}
			};
			ViewBag.sortBy = new SelectList(orderBylist, "Value", "Key");
		}

		private void getisSelectedList() {
			ViewBag.Checked = new Dictionary<bool, string> {
				{ false, "未审核" },
				{ true, "已审核" }
			};
		}

		private void getPrizeList() {
			var PrizeList = new Dictionary<int, string> {
				{ -1, "无"},
				{ 2, "三等奖"},
				{ 3, "二等奖"},
				{ 4, "一等奖"}

			};
			ViewBag.PrizeList_ = PrizeList;
			ViewBag.PrizeList = new SelectList(PrizeList, "Key", "Value");
		}

		private void getPrizeLevelList() {
			var LevelList = new Dictionary<int, string> {
				{ -1, "无"},
				{ 1, "省市级" },
				{ 2, "国家级"}
			};
			ViewBag.PrizeLevelList_ = LevelList;
			ViewBag.PrizeLevelList = new SelectList(LevelList, "Key", "Value");
		}

		private void getProjectLevelList() {
			var LevelList = new Dictionary<int, string> {
				{ -1, "无"},
				{ 0, "校级"},
				{ 1, "省市级" },
				{ 2, "国家级"}
			};
			ViewBag.ProjectLevelList_ = LevelList;
			ViewBag.ProjectLevelList = new SelectList(LevelList, "Key", "Value");
		}

		private void getParticipateList() {
			var ParticipateList = new Dictionary<int, string> {
				{ -1, "无"},
				{ 0, "第三参与人及以后"},
				{ 1, "第二参与人" },
				{ 2, "第一参与人"},
				{ 3, "项目负责人"}
			};
			ViewBag.ParticipateList_ = ParticipateList;
			ViewBag.ParticipateList = new SelectList(ParticipateList, "Key", "Value");
		}

		private void getThesisAuthorList() {
			var AuthorList = new Dictionary<int, string> {
				{ -1, "无"},
				{ 1, "第三作者及以后"},
				{ 2, "第二作者"},
				{ 3, "第一作者"}
			};
			ViewBag.ThesisAuthorList_ = AuthorList;
			ViewBag.ThesisAuthorList = new SelectList(AuthorList, "Key", "Value");
		}

		private void getPublishThesisLevelList() {
			var LevelList = new Dictionary<int, string> {
				{ -1, "无"},
				{ 3, "核心/重点刊物"},
				{ 2, "实用新型专利"},
				{ 1, "SCI/EI、发明专利、软件著作权"}
			};
			ViewBag.PublishThesisLevelList_ = LevelList;
			ViewBag.PublishThesisLevelList = new SelectList(LevelList, "Key", "Value");
		}

		private void getCrosswiseTaskList() {
			var LevelList = new Dictionary<int, string> {
				{ -1, "无"},
				{ 1, "理工类 < 10万"},
				{2, "理工类 >= 10万"},
				{3, "理工类 >= 20万"},
				{4, "人文社科艺术管理类 < 1万"},
				{5, "人文社科艺术管理类类 >= 1万"},
				{6, "人文社科艺术管理类类 >= 2万"}
			};
			ViewBag.CrosswiseTaskList_ = LevelList;
			ViewBag.CrosswiseTaskList = new SelectList(LevelList, "Key", "Value");
		}

		private void getThesisorMaterial() {
			var List = new Dictionary<int, string> {
				{ -1, "无"},
				{ 1, "论文"},
				{2, "教材"},
			};
			ViewBag.ThesisorMaterialList_ = List;
			ViewBag.ThesisorMaterialList = new SelectList(List, "Key", "Value");
		}

		private void getListsReady() {
			getPrizeLevelList();
			getPrizeList();
			getProjectLevelList();
			getParticipateList();
			getThesisAuthorList();
			getPublishThesisLevelList();
			getCrosswiseTaskList();
			getThesisorMaterial();
		}

		/// <summary>
		/// 以下Cal函数计算分值
		/// 需要计算基本分和超项目单位数的条目均返回两个元素的数组
		/// 第一个数为基本分，第二个数为超项目单位数
		/// </summary>
		/// <returns></returns>
		//课时分数
		private int[] Cal_TeachingTimeScore(int TeachingTime) { //分基本分和超分
			int[] Score = new int[] { 0, 0 };

			if (TeachingTime > BASETIME || TeachingTime == BASETIME) {
				Score[0] = 37;
				if (TeachingTime > BASETIME) {
					Score[1] = TeachingTime - BASETIME;
				}
			}

			return Score;
		}

		//获奖分数
		private int Cal_PrizeScore( int[] Prizes, int[] PrizeLevels) {//按单位计数
			int Score = 0;

			for (int i = 0; i < MAXINPUT; i++) {
				if (PrizeLevels[i] == 1) { //省奖
					switch (Prizes[i]) {
						case 2:
							Score += 2;
							break;
						case 3:
							Score += 3;
							break;
						case 4:
							Score += 4;
							break;
						default:
							break;
					}
				} else if (PrizeLevels[i] == 2) {//国奖
					switch (Prizes[i]) { 
						case 2:
							Score += 3;
							break;
						case 3:
							Score += 4;
							break;
						case 4:
							Score += 5;
							break;
						default:
							break;
					}
				}
			}

			return Score;
		}

		//项目分数
		private double[] Cal_ProjectScore(int[] Projects) { //按单位计数
			double[] Score = { 0, 0 };

			for (int i = 0; i < MAXINPUT; i++) {
				double eachScore = 0;
				switch (Projects[i]) {//每个项目的分值
					case 0:
						eachScore += 0.5;
						break;
					case 1:
						eachScore += 1;
						break;
					case 2:
						eachScore += 2;
						break;
					case 3:
						eachScore += 3;
						break;
					default:
						break;
				}
				if (eachScore > Score[0]) { //查找分数最高的项目
					Score[0] = eachScore;
				}
				Score[1] += eachScore; //所有项目的分数和
			}

			Score[1] -= Score[0]; //除去最高项

			return Score;
		}

		//教学论文分数
		private int[] Cal_TeachThesisScore(int[] TeachThesis, int[] TeachThesisType) { //基本量以分计数，超标量以单位计
			int[] Score = { 0, 0 };

			for (int i = 0; i < MAXINPUT; i++) {
				int eachScore = 0;
				if (TeachThesisType[i] == 1) {
					switch (TeachThesis[i]) {
						case 1:
							eachScore += 1;
							break;
						case 2:
							eachScore += 2;
							break;
						case 3:
							eachScore += 3;
							break;
						default:
							break;
					}
				} else if (TeachThesisType[i] == 2) {
					switch (TeachThesis[i]) {
						case 1:
							eachScore += 2;
							break;
						case 2:
							eachScore += 4;
							break;
						case 3:
							eachScore += 6;
							break;
						default:
							break;
					}
				}

				if (eachScore > Score[0]) { //最高分
					Score[0] = eachScore;
				}
				Score[1] += eachScore; //所有分
			}

			if (Score[0] > 0) {
				Score[1] -= Score[0]; //除去最高分
				Score[0] = 1; //最高分即得基本分1分
			}

			return Score;
		}

		//教学成果奖分数
		private int Cal_TeachingAwardScore(int TeachingAward) { //以分计数
			return TeachingAward;
		}

		//发表论文分数
		private double[] Cal_PublishThesisScore(int[] PThesisLevels, int[] PThesis) { //以单位计
			double[] Score = { 0, 0 };

			for (int i = 0; i < MAXINPUT; i++) {
				double eachScore = 0;
				if (PThesisLevels[i] == 3) { //核心、重点刊物
					switch (PThesis[i]) {
						case 1:
							eachScore += 0.5;
							break;
						case 2:
							eachScore += 1;
							break;
						case 3:
							eachScore += 2;
							break;
						default:
							break;
					}
				} else if (PThesisLevels[i] == 2) { //实用新型专利*0.5系数
					switch (PThesis[i]) {
						case 1:
							eachScore += 0.5;
							break;
						case 2:
							eachScore += 1;
							break;
						case 3:
							eachScore += 1.5;
							break;
						default:
							break;
					}
				} else if (PThesisLevels[i] == 1) { // SCI/EI、发明专利、软件著作权
					switch (PThesis[i]) {
						case 1:
							eachScore += 1;
							break;
						case 2:
							eachScore += 2;
							break;
						case 3:
							eachScore += 3;
							break;
						default:
							break;
					}
				}
				if (eachScore > Score[0]) {
					Score[0] = eachScore;
				}
				Score[1] += eachScore;
			}

			//Score[1] -= Score[0];

			return Score;
		}

		//纵向课题分数
		private double[] Cal_TaskScore(int[] Tasks) { //以单位计
			double[] Score = { 0, 0 };

			for (int i = 0; i < MAXINPUT; i++) {
				double eachScore = 0;
				switch (Tasks[i]) {
					case 0:
						eachScore += 0.5;
						break;
					case 1:
						eachScore += 1;
						break;
					case 2:
						eachScore += 2;
						break;
					case 3:
						eachScore += 3;
						break;
					default:
						break;
				}
				if (eachScore > Score[0]) {
					Score[0] = eachScore;
				}
				Score[1] += eachScore;
			}

			//Score[1] -= Score[0];

			return Score;
		}

		//横向课题分数
		private double Cal_CrosswiseTaskScore(int CTask) { //以分数计
			double Score = 0;

			switch (CTask) {
				case 1:
					Score += 0.5;
					break;
				case 2:
					Score += 1;
					break;
				case 3:
					Score += 2;
					break;
				case 4:
					Score += 0.5;
					break;
				case 5:
					Score += 1;
					break;
				case 6:
					Score += 2;
					break;
				default:
					break;
			}

			return Score;
		}

		/// <summary>
		/// 以下收集详细信息
		/// </summary>
		private void putPrizeInfo(ref AllInfo form, int[] Prizes, int[] PrizeLevels) {
			for (int i = 0; i < MAXINPUT; i++) {
				switch (i) {
					case 0:
						if (form.TPrize.Prize1Name == null) form.TPrize.Prize1Name = "无";
						form.TPrize.Prize1Level = ViewBag.PrizeLevelList_[PrizeLevels[i]];
						form.TPrize.Prize1Rank = ViewBag.PrizeList_[Prizes[i]];
						break;
					case 1:
						if (form.TPrize.Prize2Name == null) form.TPrize.Prize2Name = "无";
						form.TPrize.Prize2Level = ViewBag.PrizeLevelList_[PrizeLevels[i]];
						form.TPrize.Prize2Rank = ViewBag.PrizeList_[Prizes[i]];
						break;
					case 2:
						if (form.TPrize.Prize3Name == null) form.TPrize.Prize3Name = "无";
						form.TPrize.Prize3Level = ViewBag.PrizeLevelList_[PrizeLevels[i]];
						form.TPrize.Prize3Rank = ViewBag.PrizeList_[Prizes[i]];
						break;
					case 3:
						if (form.TPrize.Prize4Name == null) form.TPrize.Prize4Name = "无";
						form.TPrize.Prize4Level = ViewBag.PrizeLevelList_[PrizeLevels[i]];
						form.TPrize.Prize4Rank = ViewBag.PrizeList_[Prizes[i]];
						break;
					default:
						break;
				}
			}
		}

		private void putProjectInfo(ref AllInfo form, int[] Projects, int[] ProjectLevels) {
			for (int i = 0; i < MAXINPUT; i++) {
				switch (i) {
					case 0:
						if (form.TProject.Project1Name == null) form.TProject.Project1Name = "无";
						if (form.TProject.Project1StartTime == null) form.TProject.Project1StartTime = "无";
						if (form.TProject.Project1EndTime == null) form.TProject.Project1EndTime = "无";
						form.TProject.Project1Level = ViewBag.ProjectLevelList_[ProjectLevels[i]];
						form.TProject.Project1Role = ViewBag.ParticipateList_[Projects[i]];
						break;
					case 1:
						if (form.TProject.Project2Name == null) form.TProject.Project2Name = "无";
						if (form.TProject.Project2StartTime == null) form.TProject.Project2StartTime = "无";
						if (form.TProject.Project2EndTime == null) form.TProject.Project2EndTime = "无";
						form.TProject.Project2Level = ViewBag.ProjectLevelList_[ProjectLevels[i]];
						form.TProject.Project2Role = ViewBag.ParticipateList_[Projects[i]];
						break;
					case 2:
						if (form.TProject.Project3Name == null) form.TProject.Project3Name = "无";
						if (form.TProject.Project3StartTime == null) form.TProject.Project3StartTime = "无";
						if (form.TProject.Projectk3EndTime == null) form.TProject.Projectk3EndTime = "无";
						form.TProject.Project3Level = ViewBag.ProjectLevelList_[ProjectLevels[i]];
						form.TProject.Project3Role = ViewBag.ParticipateList_[Projects[i]];
						break;
					case 3:
						if (form.TProject.Project4Name == null) form.TProject.Project4Name = "无";
						if (form.TProject.Project4StartTime == null) form.TProject.Project4StartTime = "无";
						if (form.TProject.Project4EndTime == null) form.TProject.Project4EndTime = "无";
						form.TProject.Project4Level = ViewBag.ProjectLevelList_[ProjectLevels[i]];
						form.TProject.Project4Role = ViewBag.ParticipateList_[Projects[i]];
						break;
					default:
						break;
				}
			}
		}

		private void putTeachThesisInfo(ref AllInfo form, int[] TThesis, int[] TThesisLevels) {
			for (int i = 0; i < MAXINPUT; i++) {
				switch (i) {
					case 0:
						if (form.TTThesis.TeachThesis1Name == null) form.TTThesis.TeachThesis1Name = "无";
						form.TTThesis.TeachThesis1Author = ViewBag.ThesisAuthorList_[TThesis[i]];
						form.TTThesis.TeachThesis1Type = ViewBag.ThesisorMaterialList_[TThesisLevels[i]];
						break;
					case 1:
						if (form.TTThesis.TeachThesis2Name == null) form.TTThesis.TeachThesis2Name = "无";
						form.TTThesis.TeachThesis2Author = ViewBag.ThesisAuthorList_[TThesis[i]];
						form.TTThesis.TeachThesis2Type = ViewBag.ThesisorMaterialList_[TThesisLevels[i]];
						break;
					case 2:
						if (form.TTThesis.TeachThesis3Name == null) form.TTThesis.TeachThesis3Name = "无";
						form.TTThesis.TeachThesis3Author = ViewBag.ThesisAuthorList_[TThesis[i]];
						form.TTThesis.TeachThesis3Type = ViewBag.ThesisorMaterialList_[TThesisLevels[i]];
						break;
					case 3:
						if (form.TTThesis.TeachThesis4Name == null) form.TTThesis.TeachThesis4Name = "无";
						form.TTThesis.TeachThesis4Author = ViewBag.ThesisAuthorList_[TThesis[i]];
						form.TTThesis.TeachThesis4Type = ViewBag.ThesisorMaterialList_[TThesisLevels[i]];
						break;
					default:
						break;
				}
			}
		}

		private void putPublishThesisInfo(ref AllInfo form, int[] PThesis, int[] PThesisLevels) {
			for (int i = 0; i < 4; i++) {
				switch (i) {
					case 0:
						if (form.TPThesis.PublishThesis1Name == null) form.TPThesis.PublishThesis1Name = "无";
						form.TPThesis.PublishThesis1Author = ViewBag.ThesisAuthorList_[PThesis[i]];
						form.TPThesis.PublishThesis1Type = ViewBag.PublishThesisLevelList_[PThesisLevels[i]];
						break;
					case 1:
						if (form.TPThesis.PublishThesis2Name == null) form.TPThesis.PublishThesis2Name = "无";
						form.TPThesis.PublishThesis2Author = ViewBag.ThesisAuthorList_[PThesis[i]];
						form.TPThesis.PublishThesis2Type = ViewBag.PublishThesisLevelList_[PThesisLevels[i]];
						break;
					case 2:
						if (form.TPThesis.PublishThesis3Name == null) form.TPThesis.PublishThesis3Name = "无";
						form.TPThesis.PublishThesis3Author = ViewBag.ThesisAuthorList_[PThesis[i]];
						form.TPThesis.PublishThesis3Type = ViewBag.PublishThesisLevelList_[PThesisLevels[i]];
						break;
					case 3:
						if (form.TPThesis.PublishThesis4Name == null) form.TPThesis.PublishThesis4Name = "无";
						form.TPThesis.PublishThesis4Author = ViewBag.ThesisAuthorList_[PThesis[i]];
						form.TPThesis.PublishThesis4Type = ViewBag.PublishThesisLevelList_[PThesisLevels[i]];
						break;
					default:
						break;
				}
			}
		}

		private void putTaskInfo(ref AllInfo form, int[] Tasks, int[] TaskLevels) {
			for (int i = 0; i < MAXINPUT; i++) {
				switch (i) {
					case 0:
						if (form.TTask.Task1Name == null) form.TTask.Task1Name = "无";
						if (form.TTask.Task1StartTime == null) form.TTask.Task1StartTime = "无";
						if (form.TTask.Task1EndTime == null) form.TTask.Task1EndTime = "无";
						form.TTask.Task1Level = ViewBag.ProjectLevelList_[TaskLevels[i]];
						form.TTask.Task1Role = ViewBag.ParticipateList_[Tasks[i]];
						break;
					case 1:
						if (form.TTask.Task2Name == null) form.TTask.Task2Name = "无";
						if (form.TTask.Task2StartTime == null) form.TTask.Task2StartTime = "无";
						if (form.TTask.Task2EndTime == null) form.TTask.Task2EndTime = "无";
						form.TTask.Task2Level = ViewBag.ProjectLevelList_[TaskLevels[i]];
						form.TTask.Task2Role = ViewBag.ParticipateList_[Tasks[i]];
						break;
					case 2:
						if (form.TTask.Task3Name == null) form.TTask.Task3Name = "无";
						if (form.TTask.Task3StartTime == null) form.TTask.Task3StartTime = "无";
						if (form.TTask.Task3EndTime == null) form.TTask.Task3EndTime = "无";
						form.TTask.Task3Level = ViewBag.ProjectLevelList_[TaskLevels[i]];
						form.TTask.Task3Role = ViewBag.ParticipateList_[Tasks[i]];
						break;
					case 3:
						if (form.TTask.Task4Name == null) form.TTask.Task4Name = "无";
						if (form.TTask.Task4StartTime == null) form.TTask.Task4StartTime = "无";
						if (form.TTask.Task4EndTime == null) form.TTask.Task4EndTime = "无";
						form.TTask.Task4Level = ViewBag.ProjectLevelList_[TaskLevels[i]];
						form.TTask.Task4Role = ViewBag.ParticipateList_[Tasks[i]];
						break;
					default:
						break;
				}
			}
		}

		private void putCrosswiseTaskInfo(ref AllInfo form, int CTaskLevel) {
			if (form.TCTask.CTaskName == null) form.TCTask.CTaskName = "无";
			if (form.TCTask.CTaskStartTime == null) form.TCTask.CTaskStartTime = "无";
			if (form.TCTask.CTaskEndTime == null) form.TCTask.CTaskEndTime = "无";
			form.TCTask.CTaskLevel = ViewBag.CrosswiseTaskList_[CTaskLevel];
		}

		/// <summary>
		/// 以下为视图控制
		/// </summary>
		public ActionResult Success() {
			CommonWork();
			return View();
		}

		public ActionResult Error() {
			CommonWork();
			return View();
		}

		public ActionResult Index() {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Admin");
			} else {
				CommonWork();

				if (this.userDetails.Password == this.userDetails.ID) {
					ViewBag.PWDChanged = false;
				} else {
					ViewBag.PWDChanged = true;
				}

				ViewBag.HighScore = getHighSocores();

				return View();
			}
		}

		/// <summary>
		/// 查看账号信息
		/// </summary>
		/// <returns></returns>
		public ActionResult AccDetails() {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Admin");
			} else {
				CommonWork();
				if (userDetails.isAdmin.ToString() == "True") {
					ViewBag.isAdmin = "系主任";
				} else {
					ViewBag.isAdmin = "教师";
				}

				return View();
			}
		}

		/// <summary>
		/// 修改密码页面
		/// </summary>
		/// <returns></returns>
		public ActionResult ChangePwd() {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Admin");
			} else {
				CommonWork();
				return View();
			}
		}

		/// <summary>
		/// 执行修改密码
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult ChangePwd(Users user) {
			CommonWork();
			if (this.userDetails.Password != user.Password) {
				user.ErrMsg = "原密码输入错误";
				return View(user);
			}
			using (TeacherRewardEntities db = new TeacherRewardEntities()) {
				var updateuser = db.Users.Single(x => x.ID == userDetails.ID);
				updateuser.Password = user.NewPassword;
				try {
					db.Configuration.ValidateOnSaveEnabled = false;
					db.SaveChanges();
					db.Configuration.ValidateOnSaveEnabled = true;
					return RedirectToAction("Logout", "Login");
				} catch (Exception) {
					db.Dispose();
					return RedirectToAction("Index", "Home");
				}
			}
		}

		/// <summary>
		/// 提交信息页面
		/// </summary>
		/// <returns></returns>
		public ActionResult PostInfo() {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Admin");
			} else {
				CommonWork();
				TeacherRewardEntities db = new TeacherRewardEntities();
				var thisTeacherScore = db.TeacherScore.Where(x => x.ID == userDetails.ID).FirstOrDefault();
				if (thisTeacherScore != null && thisTeacherScore.FinalScore != 0) { //主任已计算总分
					ViewBag.AlreadyCaled = true;
				} else {
					ViewBag.AlreadyCaled = false;
				}

				getListsReady();

				return View();
			}
		}

		/// <summary>
		/// 参数好多啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊！！:<
		/// 头大
		/// post由服务器处理程序的处理能力决定
		/// </summary>
		[HttpPost]
		public ActionResult Submit(AllInfo form, int Project1Level, int Project2Level, int Project3Level, int Project4Level, int Task1Level, int Task2Level, int Task3Level, int Task4Level, int CTask, int Task1, int Task2, int Task3, int Task4, int PThesisLevel1, int PThesisLevel2, int PThesisLevel3, int PThesisLevel4, int PThesis1, int PThesis2, int PThesis3, int PThesis4, int TeachingAward, int TeachThesis1Type, int TeachThesis2Type, int TeachThesis3Type, int TeachThesis4Type, int TeachThesis1, int TeachThesis2, int TeachThesis3, int TeachThesis4, int Project1, int Project2, int Project3, int Project4, int PrizeLevel1, int PrizeLevel2, int PrizeLevel3, int PrizeLevel4, int Prize1, int Prize2, int Prize3, int Prize4) {
			CommonWork();
			getListsReady();

			//计算各分值
			int[] TeachingTimeScore = Cal_TeachingTimeScore(Convert.ToInt32(form.TInfo.baseTeachingTime));
			int PrizeScore = Cal_PrizeScore(new int[] { Prize1, Prize2, Prize3, Prize4 }, new int[] { PrizeLevel1, PrizeLevel2, PrizeLevel3, PrizeLevel4 });
			double[] ProjectScore = Cal_ProjectScore(new int[] { Project1, Project2, Project3, Project4 });
			int[] TeachThesisScore = Cal_TeachThesisScore(new int[] { TeachThesis1, TeachThesis2, TeachThesis3, TeachThesis4 }, new int[] { TeachThesis1Type, TeachThesis2Type, TeachThesis3Type, TeachThesis4Type });
			int TeachingAwardScore = Cal_TeachingAwardScore(TeachingAward);
			double[] PublishThesisScore = Cal_PublishThesisScore(new int[] { PThesisLevel1, PThesisLevel2, PThesisLevel3, PThesisLevel4 }, new int[] { PThesis1, PThesis2, PThesis3, PThesis4 });
			double[] TaskScore = Cal_TaskScore(new int[] { Task1, Task2, Task3, Task4 });
			double CrosswiseTaskScore = Cal_CrosswiseTaskScore(CTask);

			//竞赛、项目、教学论文/教材、发表论文、纵向课题、横向课题的详细信息
			putPrizeInfo(ref form, new int[] { Prize1, Prize2, Prize3, Prize4 }, new int[] { PrizeLevel1, PrizeLevel2, PrizeLevel3, PrizeLevel4 });
			putProjectInfo(ref form, new int[] { Project1, Project2, Project3, Project4 }, new int[] { Project1Level, Project2Level, Project3Level, Project4Level });
			putTeachThesisInfo(ref form, new int[] { TeachThesis1, TeachThesis2, TeachThesis3, TeachThesis4 }, new int[] { TeachThesis1Type, TeachThesis2Type, TeachThesis3Type, TeachThesis4Type });
			putPublishThesisInfo(ref form, new int[] { PThesis1, PThesis2, PThesis3, PThesis4 }, new int[] { PThesisLevel1, PThesisLevel2, PThesisLevel3, PThesisLevel4 });
			putTaskInfo(ref form, new int[] { Task1, Task2, Task3, Task4 }, new int[] { Task1Level, Task2Level, Task3Level, Task4Level });
			putCrosswiseTaskInfo(ref form, CTask);
			form.TCTask.ID = form.TPrize.ID = form.TProject.ID = form.TPThesis.ID = form.TTask.ID = form.TTThesis.ID = userDetails.ID;

			TeacherRewardEntities db = new TeacherRewardEntities();
			//新数据
			TeacherInfo insertion = new TeacherInfo();
			TeacherScore newScore = new TeacherScore();
			TeacherPrizeInfo newPrizeInfo = new TeacherPrizeInfo();
			TeacherProjectInfo newProjectInfo = new TeacherProjectInfo();
			TeacherTeachThesisInfo newTeachThesisInfo = new TeacherTeachThesisInfo();
			TeacherPublishThesisInfo newPublishThesisInfo = new TeacherPublishThesisInfo();
			TeacherTaskInfo newTaskInfo = new TeacherTaskInfo();
			TeacherCrosswiseTaskInfo newCTaskInfo = new TeacherCrosswiseTaskInfo();
			//重新提交数据
			var curTeacherInfo = db.TeacherInfo.Where(x => x.ID == userDetails.ID).FirstOrDefault();
			var curPrizeInfo = db.TeacherPrizeInfo.Where(x => x.ID == userDetails.ID).FirstOrDefault();
			var curProjectInfo = db.TeacherProjectInfo.Where(x => x.ID == userDetails.ID).FirstOrDefault();
			var curTThesisInfo = db.TeacherTeachThesisInfo.Where(x => x.ID == userDetails.ID).FirstOrDefault();
			var curPThesisInfo = db.TeacherPublishThesisInfo.Where(x => x.ID == userDetails.ID).FirstOrDefault();
			var curTaskInfo = db.TeacherTaskInfo.Where(x => x.ID == userDetails.ID).FirstOrDefault();
			var curCTaskInfo = db.TeacherCrosswiseTaskInfo.Where(x => x.ID == userDetails.ID).FirstOrDefault();

			if (curTeacherInfo != null) {
				curTeacherInfo.baseTeachingTime = TeachingTimeScore[0];
				curTeacherInfo.extTeachingTime = TeachingTimeScore[1];
				curTeacherInfo.Prize = PrizeScore;
				curTeacherInfo.baseProject = ProjectScore[0];
				curTeacherInfo.extProject = ProjectScore[1];
				curTeacherInfo.baseTeachThesis = TeachThesisScore[0];
				curTeacherInfo.extTeachThesis = TeachThesisScore[1];
				curTeacherInfo.TeachingAward = TeachingAwardScore;
				curTeacherInfo.basePublishThesis = PublishThesisScore[0];
				curTeacherInfo.extPublishThesis = PublishThesisScore[1];
				curTeacherInfo.baseTask = TaskScore[0];
				curTeacherInfo.extTask = TaskScore[1];
				curTeacherInfo.CrosswiseTask = CrosswiseTaskScore;

				curPrizeInfo = form.TPrize;
				curProjectInfo = form.TProject;
				curTThesisInfo = form.TTThesis;
				curPThesisInfo = form.TPThesis;
				curTaskInfo = form.TTask;
				curCTaskInfo = form.TCTask;
			} else {
				insertion.ID = userDetails.ID;
				insertion.Name = userDetails.Name;
				insertion.Depart = userDepart.ID;
				insertion.baseTeachingTime = TeachingTimeScore[0];
				insertion.extTeachingTime = TeachingTimeScore[1];
				insertion.Prize = PrizeScore;
				insertion.baseProject = ProjectScore[0];
				insertion.extProject = ProjectScore[1];
				insertion.baseTeachThesis = TeachThesisScore[0];
				insertion.extTeachThesis = TeachThesisScore[1];
				insertion.TeachingAward = TeachingAwardScore;
				insertion.basePublishThesis = PublishThesisScore[0];
				insertion.extPublishThesis = PublishThesisScore[1];
				insertion.baseTask = TaskScore[0];
				insertion.extTask = TaskScore[1];
				insertion.CrosswiseTask = CrosswiseTaskScore;

				newScore.ID = userDetails.ID;
				newScore.Name = userDetails.Name;
				newScore.Depart = userDepart.ID;
				newScore.isSelected = false;

				newCTaskInfo = form.TCTask;
				newPrizeInfo = form.TPrize;
				newProjectInfo = form.TProject;
				newTeachThesisInfo = form.TTThesis;
				newPublishThesisInfo = form.TPThesis;
				newTaskInfo = form.TTask;
			}
			//写入数据库
			try {
				if (curTeacherInfo == null) { //如果没有旧条目，则添加新条目
					db.TeacherInfo.Add(insertion);
					db.TeacherScore.Add(newScore);
					db.TeacherPrizeInfo.Add(newPrizeInfo);
					db.TeacherProjectInfo.Add(newProjectInfo);
					db.TeacherPublishThesisInfo.Add(newPublishThesisInfo);
					db.TeacherTeachThesisInfo.Add(newTeachThesisInfo);
					db.TeacherTaskInfo.Add(newTaskInfo);
					db.TeacherCrosswiseTaskInfo.Add(newCTaskInfo);
				}
				db.SaveChanges();
				return RedirectToAction("Success", "Home");
			} catch (Exception) {
				return RedirectToAction("Error", "Home");
			} finally {
				db.Dispose();
			}
		}

		/// <summary>
		/// 信息公示页面
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult ShowAll(string ID = null, string Name = null, int SearchByDepart = 0, string sortBy = "none", bool ShowCheck = false) {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Admin");
			} else {
				CommonWork();

				getDepartmentList();
				getSortList();
				getisSelectedList();

				TeacherRewardEntities db = new TeacherRewardEntities();
				var TeacherScores = db.TeacherScore.Select(x => x);

				//筛选信息
				if (ShowCheck == true) {
					TeacherScores = TeacherScores.Where(x => x.isSelected == true);
				}
				if (!String.IsNullOrEmpty(ID)) {
					TeacherScores = TeacherScores.Where(x => x.ID.Contains(ID));
				}
				if (!String.IsNullOrEmpty(Name)) {
					TeacherScores = TeacherScores.Where(x => x.Name.Contains(Name));
				}
				if (SearchByDepart != 0) {
					TeacherScores = TeacherScores.Where(x => x.Depart == SearchByDepart);
				}
				switch (sortBy) {
					case "low":
						TeacherScores = TeacherScores.OrderBy(x => x.FinalScore);
						break;
					case "high":
						TeacherScores = TeacherScores.OrderByDescending(x => x.FinalScore);
						break;
					default:
						break;
				}
				
				return View(TeacherScores);
			}
		}

		/// <summary>
		/// 查看详情页面
		/// </summary>
		/// <returns></returns>
		public ActionResult ShowDetail(string ID = null) {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Admin");
			} else {
				if (ID == null ) {
					return RedirectToAction("Index", "Home");
				}
				CommonWork();
				getDepartmentList();

				using (TeacherRewardEntities db = new TeacherRewardEntities()) {
					AllInfo allinfo = new AllInfo();
					allinfo.TInfo = db.TeacherInfo.Where(x => x.ID == ID).FirstOrDefault();
					allinfo.TCTask = db.TeacherCrosswiseTaskInfo.Where(x => x.ID == ID).FirstOrDefault();
					allinfo.TPrize = db.TeacherPrizeInfo.Where(x => x.ID == ID).FirstOrDefault();
					allinfo.TProject = db.TeacherProjectInfo.Where(x => x.ID == ID).FirstOrDefault();
					allinfo.TPThesis = db.TeacherPublishThesisInfo.Where(x => x.ID == ID).FirstOrDefault();
					allinfo.TTThesis = db.TeacherTeachThesisInfo.Where(x => x.ID == ID).FirstOrDefault();
					allinfo.TTask = db.TeacherTaskInfo.Where(x => x.ID == ID).FirstOrDefault();
					return View(allinfo);
				}
			}
		}
	}
}