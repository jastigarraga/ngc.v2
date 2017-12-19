using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGC.BLL.Interfaces;
using NGC.Common.Classes.Filters;
using NGC.Model;
using NGC.UI.BllContainers;
using NGC.UI.Extensions;
using NGC.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGC.UI.Controllers
{
    [Authorize]
    public class PhotoController : Base.V2.MerakiControllerV2<IPhotoControllerBLL>
    {
        public PhotoController(IPhotoControllerBLL bll) : base(bll)
        {
        }
        public ActionResult Customer(int id = 0)
        {
            return View(_bll.Customers.GetById(id));
        }
        [HttpPost("api/photo")]
        public ActionResult NewPhoto(IEnumerable<PhotoModel> photos)
        {
            var phs = photos.Select(p => Mapper.Map<Photo>(p));
            foreach(Photo photo in phs)
            {
                _bll.Photos.Insert(photo);
            }
            _bll.Photos.Save();
            return Ok(phs.Map<Photo,PhotoModel>());
        }
        [HttpGet("api/photo")]
        public IEnumerable<PhotoModel> GetPhotos(int idCustomer)
        {
            return _bll.Photos.GetByFilters(new PhotoFilter()
            {
                IdCustomer = idCustomer
            }).Map<Photo,PhotoModel>();
        }
    }
}
