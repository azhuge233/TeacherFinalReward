using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherReward.Models;

namespace TeacherReward.Controllers
{
    public class AdminController : Controller
    {
        Users userDetails;
        Department userDepart;
		private static int ShowUpScore = 4;

		/// <summary>
		/// 检查权限
		/// </summary>
		private int Check() {
            if (Session["userID"] == null) {
                return 1;
            } else if (Session["isAdmin"].ToString() == "False") {
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
		}

		/// <summary>
		/// 取得当前最大值
		/// </summary>
		private string[] getHighSocores() {
			CommonWork();
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

		private void getPerformList() {
			var List = new Dictionary<string, int> {
				{ "优", 4 },
				{ "良", 3 },
				{ "中", 2 },
				{ "合格", 1 },
				{ "不合格", 0 }
			};
			ViewBag.PerformList = new SelectList(List, "Value", "Key");
		}

		/// <summary>
		/// 以下Cal函数计算评分
		/// </summary>
		/// <returns></returns>
		private int Cal_PerformScore(int Perform) {
			int Score = 0;

			switch (Perform) {
				case 4:
					Score = 10;
					break;
				case 3:
					Score = 9;
					break;
				case 2:
					Score = 8;
					break;
				case 1:
					Score = 7;
					break;
				default:
					break;
			}

			return Score;
		}

		private int Cal_DutyTimeScore(int DutyTime) {
			int Score = 0;

			switch (DutyTime) {
				case 4:
					Score = 5;
					break;
				case 3:
					Score = 4;
					break;
				case 2:
					Score = 3;
					break;
				case 1:
					Score = 2;
					break;
				case 0:
					Score = 1;
					break;
				default:
					break;
			}

			return Score;
		}

		private int Cal_ShowupScore(int beLate, int didnotShowup) {
			int Score = ShowUpScore;

			Score -= beLate * 2 + didnotShowup * 2;

			return Score;
		}

		private int Cal_DepartWorkScore(int DepartWork) {
			int Score = 0;

			switch (DepartWork) {
				case 4:
					Score = 6;
					break;
				case 3:
					Score = 5;
					break;
				case 2:
					Score = 4;
					break;
				case 1:
					Score = 3;
					break;
				case 0:
					Score = 1;
					break;
				default:
					break;
			}

			return Score;
		}

		private double Cal_KPIScore(TeacherInfo T, string[] HighScores) {
			double Score = 0;

			Score += T.ShiDe;
			Score += T.baseTeachingTime;
			Score += (3 / Convert.ToInt32(HighScores[0])) * T.extTeachingTime;
			Score += Convert.ToDouble(T.BossScore) * 0.4;
			Score += Convert.ToDouble(T.SuperScore) * 0.3;
			Score += Convert.ToDouble(T.StuScore) * 0.3;
			Score += T.DutyTime;
			Score += (5 / Convert.ToInt32(HighScores[1])) * T.Prize;
			Score += T.baseProject * 4 / 3;
			Score += (3 / Convert.ToInt32(HighScores[2])) * T.extProject;
			Score += T.baseTeachThesis;
			Score += (1 / Convert.ToInt32(HighScores[3])) * T.extTeachThesis;
			Score += T.TeachingAward;

			return Math.Round(Score, 2, MidpointRounding.AwayFromZero);
		}

		private double Cal_CPIScore(TeacherInfo T, string[] HighScores) {
			double Score = 0;

			Score += (5 / Convert.ToInt32(HighScores[4])) * T.extPublishThesis;
			Score += (3 / Convert.ToInt32(HighScores[5])) * T.extTask;
			Score += T.CrosswiseTask;
			Score += T.Showup;
			Score += T.DepartWorks;

			return Math.Round(Score, 2, MidpointRounding.AwayFromZero);
		}

		/// <summary>
		/// 以下为视图控制
		/// </summary>
		/// <returns></returns>
		public ActionResult Error() {
			CommonWork();
			return View();
		}

		public ActionResult Success() {
			CommonWork();
			return View();
		}

		public ActionResult Index() {
            int res = Check();
            if (res == 1) {
                return RedirectToAction("Index", "Login");
            } else if (res == 2) {
                return RedirectToAction("Index", "Home");
            } else {
                CommonWork();

                if (userDetails.Password == userDetails.ID) {
                    ViewBag.PWDChanged = false;
                } else {
                    ViewBag.PWDChanged = true;
                }

				ViewBag.HighScore = getHighSocores();

				return View();
            }
        }

        public ActionResult AccDetails() {
            int res = Check();
            if (res == 1) {
                return RedirectToAction("Index", "Login");
            } else if (res == 2) {
                return RedirectToAction("Index", "Home");
            } else {
                CommonWork();

                ViewBag.curUserID = userDetails.ID.ToString();
                if (userDetails.isAdmin.ToString() == "True") {
                    ViewBag.isAdmin = "系主任";
                } else {
                    ViewBag.isAdmin = "教师";
                }
                ViewBag.userDepart = userDepart.Name.ToString();

                return View(userDetails);
            }
        }

        public ActionResult ChangePwd() {
            int res = Check();
            if (res == 1) {
                return RedirectToAction("Index", "Login");
            } else if (res == 2) {
                return RedirectToAction("Index", "Home");
            } else {
                CommonWork();
                return View();
            }
        }

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
                    return RedirectToAction("Index", "Admin");
                }
            }
        }

		[HttpGet]
		public ActionResult ShowAll(string ID = null, string Name = null, int SearchByDepart = 0, string sortBy = "none", bool ShowCheck = false) {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Home");
			} else {
				CommonWork();

				getDepartmentList();
				getSortList();
				getisSelectedList();

				TeacherRewardEntities db = new TeacherRewardEntities();
				var TeacherScores = db.TeacherScore.Select(x => x);
				if (db.TeacherScore.Where(x => x.FinalScore == 0 && x.Depart == userDetails.Department).Count() != 0) {
					ViewBag.ReadytoJudge = false;
				} else {
					ViewBag.ReadytoJudge = true;
				}

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

		public ActionResult ShowDetail(string ID = null) {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Home");
			} else {
				if (ID == null) {
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

		[HttpGet]
		public ActionResult PostInfo(string ID = null, string Name = null, bool isNotReady = false) {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Home");
			} else {
				CommonWork();

				TeacherRewardEntities db = new TeacherRewardEntities();

				if (db.TeacherScore.Where(x => x.Depart == userDetails.Department).Count() == 0) {
					ViewBag.NoInfos = true;
				} else {
					ViewBag.NoInfos = false;
				}

				getDepartmentList();
				getisSelectedList();

				var TeacherInfos = db.TeacherInfo.Select(x => x);
				TeacherInfos = TeacherInfos.Where(x => x.Depart == userDetails.Department);

				//筛选信息
				if (isNotReady == true) {
					TeacherInfos = TeacherInfos.Where(x => x.DutyTime == 0);
				}
				if (!String.IsNullOrEmpty(ID)) {
					TeacherInfos = TeacherInfos.Where(x => x.ID.Contains(ID));
				}
				if (!String.IsNullOrEmpty(Name)) {
					TeacherInfos = TeacherInfos.Where(x => x.Name.Contains(Name));
				}

				return View(TeacherInfos);
			}
		}

		public ActionResult UpdateInfo(string ID = null) {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Home");
			} else {
				if (ID == null) {
					return RedirectToAction("Error", "Admin");
				}
				CommonWork();

				getDepartmentList();
				getPerformList();

				TeacherRewardEntities db = new TeacherRewardEntities();
				var curTeacherInfo = db.TeacherInfo.SingleOrDefault(x => x.ID == ID);

				return View(curTeacherInfo);
			}
		}

		[HttpPost]
		public ActionResult UpdateInfo(string ID, int ShiDe = -1, int BossScore = -1, int SuperScore = -1, int StuScore = -1, int DutyTime = -1, int beLate = -1, int didnotShowup = -1, int DepartWork = -1) {
			if (ShiDe == -1) {
				return UpdateInfo(ID);
			}
			TeacherRewardEntities db = new TeacherRewardEntities();
			var thisTeacherInfo = db.TeacherInfo.SingleOrDefault(x => x.ID == ID);

			//计算分值
			thisTeacherInfo.ShiDe = Cal_PerformScore(ShiDe);
			thisTeacherInfo.BossScore = Cal_PerformScore(BossScore);
			thisTeacherInfo.SuperScore = Cal_PerformScore(SuperScore);
			thisTeacherInfo.StuScore = Cal_PerformScore(StuScore);
			thisTeacherInfo.DutyTime = Cal_DutyTimeScore(DutyTime);
			thisTeacherInfo.Showup = Cal_ShowupScore(beLate, didnotShowup);
			thisTeacherInfo.DepartWorks = Cal_DepartWorkScore(DepartWork);

			try {
				db.SaveChanges();
				return RedirectToAction("PostInfo", "Admin");
			} catch (Exception) {
				return RedirectToAction("Error", "Admin");
			} finally {
				db.Dispose();
			}
		}

		[HttpGet]
		public ActionResult CalFinalScore(int Affirm = -1) {
			int res = Check();
			if (res == 1) {
				return RedirectToAction("Index", "Login");
			} else if (res == 2) {
				return RedirectToAction("Index", "Home");
			} else {
				CommonWork();
				TeacherRewardEntities db = new TeacherRewardEntities();
				//检查
				if (db.TeacherScore.Where(x => x.Depart == userDetails.Department).Count() == 0) {
					ViewBag.ReadyStatus = 0;//没有教师填写信息
				} else if (db.TeacherInfo.Where(x => x.DutyTime == 0 && x.Depart == userDetails.Department).Count() != 0) {
					ViewBag.ReadyStatus = 1;//存在主任未评分的教师信息
				} else if (db.TeacherScore.Where(x => x.Depart == userDetails.Department).Count() != db.Users.Where(x => x.Department == userDetails.Department && x.isAdmin == false).Count()) {
					ViewBag.ReadyStatus = 2;//本系内部分教师未填写信息
				} else if(db.TeacherScore.Where(x => x.KPIScore == 0 && x.Depart == userDetails.Department).Count() == 0) { //已经评分完毕
					ViewBag.ReadyStatus = 3;
				} else {
					ViewBag.ReadyStatus = 4;
				}

				if (Affirm == -1) {
					return View();
				} else {
					string[] HighScores = getHighSocores();
					var TeacherInfos = db.TeacherInfo.Where(x => x.Depart == userDetails.Department);
					foreach (TeacherInfo each in TeacherInfos) {
						var thisTeacherScore = db.TeacherScore.Single(x => x.ID == each.ID);
						thisTeacherScore.KPIScore = Cal_KPIScore(each, HighScores);
						thisTeacherScore.CPIScore = Cal_CPIScore(each, HighScores);
						thisTeacherScore.FinalScore = thisTeacherScore.KPIScore + thisTeacherScore.CPIScore;
					}
					try {
						db.SaveChanges();
						return RedirectToAction("Success", "Admin");
					} catch (Exception) {
						return RedirectToAction("Error", "Admin");
					} finally {
						db.Dispose();
					}
				}
			}
		}

		[HttpPost]
		public ActionResult Select(string ID = null, int isSelect = -1) {
			if (ID == null || isSelect == -1) {
				return RedirectToAction("Error", "Admin");
			}
			TeacherRewardEntities db = new TeacherRewardEntities();
			var thisTeacherScore = db.TeacherScore.Single(x => x.ID == ID);

			thisTeacherScore.isSelected = isSelect == 1 ? true : false;
			try {
				db.SaveChanges();
				return RedirectToAction("ShowAll", "Admin");
			} catch (Exception) {
				return RedirectToAction("Error", "Admin");
			} finally {
				db.Dispose();
			}
		}
	}
}