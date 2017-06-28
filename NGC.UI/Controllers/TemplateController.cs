using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NGC.Model;

namespace NGC.UI.Controllers
{
    public class TemplateController : Base.MerakiController
    {
        protected IEmailTemplateBLL _emailTemplateBLL;
        public TemplateController(IUserBLL userBll, IEmailTemplateBLL emailTemplateBLL) : base(userBll)
        {
            this._emailTemplateBLL = emailTemplateBLL;
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
        public PartialViewResult MerakiEditor()
        {
            return PartialView();
        }
        public ActionResult Index()
        {
            return View("EmailTemplateEditor");
        }
       
        [HttpGet("api/templates")]
        public IEnumerable<EmailTemplate> GetTemplates()
        {
            return _emailTemplateBLL.GetAll();
        }
        [HttpPost("api/templates")]
        public EmailTemplate UpdateEmailTemplate([FromBody] EmailTemplate model)
        {
            EmailTemplate template = _emailTemplateBLL.GetById(model.Id);
            template.Name = model.Name;
            template.Subject = model.Subject;
            template.Template = model.Template;
            _emailTemplateBLL.Update(template);
            _emailTemplateBLL.Save();
            return template;
        }
        [HttpPut("api/templates")]
        public EmailTemplate InsertEmailTemplate([FromBody]EmailTemplate model)
        {
            _emailTemplateBLL.Insert(model);
            _emailTemplateBLL.Save();
            return model;
        }
        [HttpDelete("api/templates")]
        public ActionResult DeleteEmailTemplate(int id)
        {
            try
            {
                EmailTemplate template = _emailTemplateBLL.GetById(id);
                _emailTemplateBLL.Delete(template);
                _emailTemplateBLL.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
