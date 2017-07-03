

using NGC.Model;
using System.Collections.Generic;

namespace NGC.BLL.Interfaces
{
    public interface IMerakiTextImageBLL : IBaseBLL<MerakiTextImage> {
        MerakiTextImage GetById(int id);
    }
}
