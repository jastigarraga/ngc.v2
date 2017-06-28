using NGC.Common.Helpers;
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
                if (user != null && user.PasswordVerify(model.Password))
                {
                    MerakiUser = user;

                    return Redirect("~/");
                }
            }
            ViewBag.Error = "Nombre de usuario y/o contraseña incorrectos";
            return View("Index");
        }
        [HttpGet]
        public ActionResult DoLogOff()
        {
            MerakiUser = null;
            return Redirect(Url.Action("", "Login"));
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
