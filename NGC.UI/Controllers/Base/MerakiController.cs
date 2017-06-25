using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NGC.BLL;
using NGC.BLL.Interfaces;
using NGC.DAL.Base;
using NGC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGC.UI.Controllers.Base
{
    public abstract class MerakiController : Controller
    {
        private User merakiUser;
        protected IUserBLL _userBLL;
        public MerakiController(IUserBLL userBll)
        {
            _userBLL = userBll;
        }
        protected User MerakiUser {
            get {
                if (merakiUser == null)
                {
                    string login = Request.HttpContext.User.Identity.Name;
                    merakiUser = _userBLL.GetByLogin(login);
                }
                return merakiUser;
            }
            set {
                Request.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(value);
                HttpContext.Authentication.SignInAsync("Loged", Request.HttpContext.User).Wait();
                merakiUser = value;
            }
        }
    }
}
