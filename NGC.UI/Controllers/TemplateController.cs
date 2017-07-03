using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NGC.Model;
using NGC.UI.Models;
using Microsoft.AspNetCore.Authorization;

namespace NGC.UI.Controllers
{
    [Authorize]
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
        [HttpGet("api/templates/{name}")]
        public PartialViewResult Editor(string name)
        {
            return PartialView(name);
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
        public PartialViewResult MerakiPopupGrid()
        {
            return PartialView();
        }

        [HttpGet("api/templates")]
        public IEnumerable<EmailTemplateModel> GetTemplates()
        {
            return _emailTemplateBLL.GetAll().Select(m=>Mapper.Map<EmailTemplateModel>(m));
        }
        [HttpPost("api/templates")]
        public EmailTemplateModel UpdateEmailTemplate([FromBody] EmailTemplateModel model)
        {
            EmailTemplate template = _emailTemplateBLL.GetById(model.Id);
            Mapper.Map<EmailTemplateModel, EmailTemplate>(model, template);
            _emailTemplateBLL.Save();
            return Mapper.Map<EmailTemplateModel>(template);
        }
        [HttpPut("api/templates")]
        public EmailTemplateModel InsertEmailTemplate([FromBody]EmailTemplateModel model)
        {
            var m = Mapper.Map<EmailTemplate>(model);
            _emailTemplateBLL.Insert(m);
            _emailTemplateBLL.Save();
            return Mapper.Map<EmailTemplateModel>(m);
        }
        
        [HttpDelete("api/templates")]
        public ActionResult DeleteEmailTemplate(int id)
        {
            try
            {
                EmailTemplate template = _emailTemplateBLL.GetById(id);
                if (template.Customers.Any())
                {
                    return BadRequest("No se puede eliminar una plantilla que tenga usuarios");
                }
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
