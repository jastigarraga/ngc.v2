using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NGC.BLL.Interfaces;
using NGC.Model;
using NGC.UI.Mapper;

namespace NGC.UI.Controllers.Base
{
    public abstract class MerakiController : Controller
    {
        private User merakiUser;
        protected IUserBLL _userBLL;
        protected IMapper Mapper { get; private set; }
        public MerakiController(IUserBLL userBll)
        {
            Mapper = MapperFactory.GetMapper();
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
                if (value == null)
                {
                    HttpContext.Authentication.SignInAsync("Loged", Request.HttpContext.User);
                }
                else
                {
                    Request.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(value);
                    HttpContext.Authentication.SignInAsync("Loged", Request.HttpContext.User).Wait();
                }
                merakiUser = value;
            }
        }
    }
}
