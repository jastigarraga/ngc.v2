using NGC.BLL.Interfaces;
using NGC.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using NGC.DAL.Base;

namespace NGC.BLL
{
    public class MerakiTextImageBLL : Base.BaseBLL<MerakiTextImage>, Interfaces.IMerakiTextImageBLL
    {
        public MerakiTextImageBLL(MerakiContext context) : base(context)
        {
        }

        public MerakiTextImage GetById(int id)
        {
            return repository.QueryAll.Where(i => i.Id == id).FirstOrDefault();
        }
    }
}
