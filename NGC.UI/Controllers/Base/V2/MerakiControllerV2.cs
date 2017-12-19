using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NGC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGC.UI.Controllers.Base.V2
{
    public abstract class MerakiControllerV2<TBllContainer> : Controller where TBllContainer : BllContainers.IMerakiControllerBLL
    {
        protected readonly TBllContainer _bll;
        private User merakiUser;
        protected IMapper Mapper { get; private set; }
        protected User MerakiUser
        {
            get
            {
                if (merakiUser == null)
                {
                    string login = Request.HttpContext.User.Identity.Name;
                    merakiUser = _bll.Users.GetByLogin(login);
                }
                return merakiUser;
            }
            set
            {
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
        public MerakiControllerV2(TBllContainer bll)
        {
            this._bll = bll;
        }
       
    }
}
