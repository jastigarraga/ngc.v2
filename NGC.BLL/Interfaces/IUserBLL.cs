using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.BLL.Interfaces
{
    public interface IUserBLL : IBaseBLL<User>
    {
        User GetByLogin(string login);
        User GetById(int id);
    }
}
