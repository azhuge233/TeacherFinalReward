using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherReward.Models;

namespace TeacherReward.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Users user) {
            using (TeacherRewardEntities db = new TeacherRewardEntities()) {
                var userDetails = db.Users.Where(x => x.ID == user.ID && x.Password == user.Password).FirstOrDefault();
                if (userDetails == null) {
                    user.ErrMsg = "用户名或密码错误";
                    return View("Index", user);
                } else {
                    ///
                    ///使用userDetails而不是user，因为user对象中的isAdmin和Department没有赋值（用户只输入了ID和密码）
                    ///
                    Session["userID"] = userDetails.ID;
                    Session["isAdmin"] = userDetails.isAdmin;
                    Session["Depart"] = userDetails.Department;
                    if (userDetails.isAdmin) {
                        return RedirectToAction("Index", "Admin");
                    } else {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }

        public ActionResult Logout() {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}