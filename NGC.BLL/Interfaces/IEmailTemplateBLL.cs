using NGC.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.BLL.Interfaces
{
    public interface IEmailTemplateBLL : IBaseBLL<EmailTemplate>
    {
        EmailTemplate GetById(int id);
    }
}
