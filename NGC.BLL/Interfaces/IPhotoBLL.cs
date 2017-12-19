using NGC.Common.Classes.Filters;
using NGC.DAL.Base;
using NGC.Model;

namespace NGC.BLL.Interfaces
{
    public interface IPhotoBLL : Base.V2.IBaseBLL<Photo,PhotoFilter,IBaseRepository<Photo>>
    {
        
    }
}
