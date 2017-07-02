using NGC.Model;
using System.Linq;
using NGC.DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace NGC.BLL
{
    public class EmailTemplateBLL : Base.BaseBLL<EmailTemplate>, Interfaces.IEmailTemplateBLL
    {
        public EmailTemplateBLL(MerakiContext context) : base(context)
        {
        }

        public EmailTemplate GetById(int id)
        {
            return repository.QueryAll.Where(e => e.Id == id).Include(e=>e.Customers).FirstOrDefault();
        }
    }
}
