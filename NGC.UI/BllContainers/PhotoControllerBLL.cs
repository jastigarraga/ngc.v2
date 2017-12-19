using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGC.BLL.Interfaces;

namespace NGC.UI.BllContainers
{
    public class PhotoControllerBLL : MerakiControllerBLL, IPhotoControllerBLL
    {
        private readonly IPhotoBLL photoBLL;
        private readonly ICustomerBLL customerBLL;
        public IPhotoBLL Photos => photoBLL;

        public ICustomerBLL Customers => customerBLL;

        public PhotoControllerBLL(IUserBLL userBLL,ICustomerBLL customerBLL, IPhotoBLL photoBLL) : base(userBLL)
        {
            this.photoBLL = photoBLL;
            this.customerBLL = customerBLL;
        }
    }
}
