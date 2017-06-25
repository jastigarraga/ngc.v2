using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NGC.DAL.Base;
using Microsoft.AspNetCore.Mvc;
using NGC.UI.Models;
using NGC.Model;
using NGC.BLL.Interfaces;

namespace NGC.UI.Controllers
{
    public class LoginController : Base.MerakiController
    {
        public LoginController(IUserBLL userBll) : base(userBll)
        {
        }

        [HttpPost]
        public  ActionResult DoLogin(UserModel model)
        {
            if(model != null)
            {
                User user = _userBLL.GetByLogin(model.Login);
                if(user != null && user.Password == model.Password)
                {
                    MerakiUser = user;
                    RedirectToAction("Index", "Home");
                }
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
