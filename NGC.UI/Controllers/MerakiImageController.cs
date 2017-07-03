using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGC.BLL.Interfaces;
using NGC.Model;
using NGC.UI.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace NGC.UI.Controllers
{
    [Authorize]
    public class MerakiImageController : Base.MerakiController
    {
        protected IMerakiTextImageBLL ImageBLL { get; private set; }
        public MerakiImageController(IUserBLL userBll, IMerakiTextImageBLL imageBLL) : base(userBll)
        {
            this.ImageBLL = imageBLL;
        }
        public ActionResult Index()
        {
            
            return View("Editor");
        }
        [HttpGet("api/merakiimage")]
        public IEnumerable<MerakiTextImageModel> GetAll()
        {
            return ImageBLL.GetAll().Select(i => Mapper.Map<MerakiTextImageModel>(i));
        }
        [HttpPut("api/merakiimage")]
        public MerakiTextImageModel Insert([FromBody]MerakiTextImageModel model)
        {
            if (ModelState.IsValid)
            {
                var image = Mapper.Map<MerakiTextImage>(model);
                ImageBLL.Insert(image);
                ImageBLL.Save();
                return Mapper.Map<MerakiTextImageModel>(image);
            }
            return null;
        }
        [HttpPost("api/merakiimage")]
        public MerakiTextImageModel Update([FromBody]MerakiTextImageModel model)
        {
            if (ModelState.IsValid)
            {
                var image = ImageBLL.GetById(model.Id);
                if (image != null)
                {
                    Mapper.Map<MerakiTextImageModel, MerakiTextImage>(model, image);
                    ImageBLL.Save();
                    return Mapper.Map<MerakiTextImageModel>(image);
                }
            }
            return null;
        }
        [HttpDelete("api/merakiimage")]
        public ActionResult Delete(int id) {
            var image = ImageBLL.GetById(id);
            if(image != null)
            {
                try
                {
                    ImageBLL.Delete(image);
                    ImageBLL.Save();
                    return Ok();
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("La imagen no existe");
        }

    }
}
