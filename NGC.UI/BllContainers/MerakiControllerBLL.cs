using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NGC.BLL.Interfaces;

namespace NGC.UI.BllContainers
{
    public class MerakiControllerBLL : IMerakiControllerBLL
    {
        private readonly IUserBLL userBLL;

        public IUserBLL Users => userBLL;

        public MerakiControllerBLL(IUserBLL userBLL)
        {
            this.userBLL = userBLL;
        }
    }
}
