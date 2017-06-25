using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NGC.BLL;
using NGC.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using NGC.DAL.Base;
using NGC.BLL.Interfaces;

namespace NGC.UI.Controllers
{

    [Authorize(Policy = "Loged")]
    public class HomeController : Base.MerakiController
    {
        public HomeController(IUserBLL userBll) : base(userBll)
        {
        }

        public IActionResult Index()
        {
          
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page. " + _userBLL.GetAll().Count().ToString();

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
