using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NGC.UI.Controllers
{
    public class TemplateController : Base.MerakiController
    {
        public TemplateController(IUserBLL userBll) : base(userBll)
        {
        }

        public PartialViewResult EditorBase(string model,string label = "",string type="text",bool required = false)
        {
            ViewBag.Required = required?"required":"";
            ViewBag.Label = label;
            ViewBag.Model = model;
            return PartialView();
        }
        public PartialViewResult ViewBase(string model)
        {
            ViewBag.Model = model;
            return PartialView();
        }
        public PartialViewResult ViewDate(string model)
        {
            ViewBag.Model = model;
            return PartialView();
        }
        public PartialViewResult EditorDate(string model)
        {
            ViewBag.Model = model;
            return PartialView();
        }
        public PartialViewResult MerakiGrid()
        {
            return PartialView();
        }
        public PartialViewResult MerakiDeleteConfirm()
        {
            return PartialView();
        }
    }
}
