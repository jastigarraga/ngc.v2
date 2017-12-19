using NGC.BLL.Interfaces;
using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;
using NGC.DAL.Base;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using NGC.Common.Classes.Filters;
using System.Linq.Expressions;

namespace NGC.BLL
{
    public class PhotoBLL : Base.V2.BaseBLL<Photo,PhotoFilter,IBaseRepository<Photo>>, IPhotoBLL
    {
        public PhotoBLL(IBaseRepository<Photo> repository) : base(repository)
        {
        }

        public string PhotosPath { get; private set; }

        public override IEnumerable<Photo> GetAll()
        {
            var photos = base.GetAll();
            foreach(Photo p in photos)
            {
                if (File.Exists(p.Path))
                {
                    try
                    {
                        p.bytes = File.ReadAllBytes(p.Path);
                    }
                    catch (Exception) { }
                }
            }
            return photos;
        }
        public override void Delete(Photo entity)
        {
            if (File.Exists(entity.Path))
            {
                File.Delete(entity.Path);
            }
            base.Delete(entity);
        }
        public override void Update(Photo entity)
        {
            File.WriteAllBytes(entity.Path, entity.bytes);
            base.Update(entity);
        }
        public override void Insert(Photo entity)
        {
            File.WriteAllBytes(entity.Path, entity.bytes);
            base.Insert(entity);
        }
        public Photo GetById(int id)
        {
            var photo = repository.All.Where(p => p.Id == id).FirstOrDefault();
            if(photo != null && File.Exists(photo.Path ?? ""))
            {
                photo.bytes = File.ReadAllBytes(photo.Path);
            }
            return photo;
        }
        public override IEnumerable<Photo> GetByFilters(PhotoFilter filter, Expression<Func<Photo, object>> order = null, params Expression<Func<Photo, object>>[] includes)
        {
            var photos = base.GetByFilters(filter, order, includes);
            foreach (Photo p in photos)
            {
                if (File.Exists(p.Path))
                {
                    try
                    {
                        p.bytes = File.ReadAllBytes(p.Path);
                    }
                    catch (Exception) { }
                }
            }
            return photos;
        }
    }
}
