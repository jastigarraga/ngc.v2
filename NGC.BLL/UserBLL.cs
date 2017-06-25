using NGC.DAL.Base;
using NGC.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace NGC.BLL
{
    public class UserBLL : Base.BaseBLL<User>, Interfaces.IUserBLL
    {
        public UserBLL(MerakiContext context) : base(context)
        {
        }
        public User GetByLogin(string login)
        {
            return repository.QueryAll.Where(u => u.Login == login).FirstOrDefault();
        }
        public User GetById(int id)
        {
            return repository.QueryAll.Where(u => u.Id == id).FirstOrDefault();
        }
    }
}
