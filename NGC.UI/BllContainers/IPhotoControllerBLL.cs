using NGC.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGC.UI.BllContainers
{
    public interface IPhotoControllerBLL : IMerakiControllerBLL
    {
        IPhotoBLL Photos { get; }
        ICustomerBLL Customers { get; }
    }
}
