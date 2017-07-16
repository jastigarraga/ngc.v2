

using NGC.Model;
using System.Collections.Generic;
using System.Linq;

namespace NGC.BLL.Interfaces
{
    public interface IMerakiTextImageBLL : IBaseBLL<MerakiTextImage> {
        MerakiTextImage GetById(int id);

        IQueryable<MerakiTextImage> QueryAll();
    }
}
