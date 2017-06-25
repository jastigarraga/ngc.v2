

using NGC.Common.Classes;
using NGC.Model;

namespace NGC.BLL.Interfaces
{
    public interface IConfigurationBLL : IBaseBLL<Configuration>
    {
         EmailConfiguration GetEmailConfiguration();
        void Update(EmailConfiguration config);


    }
}
