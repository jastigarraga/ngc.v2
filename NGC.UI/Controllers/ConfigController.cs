using NGC.UI.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGC.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGC.Common.Classes;
using NGC.Common.Helpers;

namespace NGC.UI.Controllers
{
    [Authorize]
    public class ConfigController : MerakiController
    {
        protected IConfigurationBLL _configBLL { get; private set; }
        public ConfigController(IUserBLL userBll, IConfigurationBLL configBLL) : base(userBll)
        {
            _configBLL = configBLL;
        }

        [HttpGet("api/emailconfig")]
        public EmailConfiguration GetEmailConfiguration()
        {
            return _configBLL.GetEmailConfiguration();
        }
        public ActionResult Index()
        {
            return View("Config");
        }
        [HttpPost("api/emailconfig")]
        public async Task<ActionResult> TestConfiguration([FromBody]EmailConfiguration config)
        {
            try
            {
                await EmailHelper.SendEmailAsync(config, config.To, config.To, "Email de prueba", $"<p>Enhorabuena!</p><p>La siguiente configuración ha funcionado:</p> {config.ToString()}");
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("api/emailconfig")]
        public EmailConfiguration SaveEmailConfig([FromBody]EmailConfiguration config)
        {
            _configBLL.Update(config);
            _configBLL.Save();
            return _configBLL.GetEmailConfiguration();
        }
    }
}
