using System;
using System.Collections.Generic;
using System.Linq;
using NGC.BLL.Interfaces;
using NGC.Common.Classes;
using NGC.DAL.Base;
using NGC.Model;

namespace NGC.BLL
{
    public class ConfigurationBLL : Base.BaseBLL<Configuration>, IConfigurationBLL
    {
        public ConfigurationBLL(MerakiContext context) : base(context)
        {
        }
        public EmailConfiguration GetEmailConfiguration()
        {
            var config = repository.QueryAll.Where(c => c.Key.Contains("Email"));
            return new EmailConfiguration(config);
        }
        public void Update(EmailConfiguration config)
        {
            var entries = config.ToConfigCollection();
            var configEntries = repository.QueryAll.Where(c => entries.Select(e => e.Key).Contains(c.Key)).AsEnumerable();
            foreach(var entry in entries)
            {
                if (configEntries.Any(c=>c.Key == entry.Key)){
                    var c = configEntries.Where(conf => conf.Key == entry.Key).FirstOrDefault();
                    c.Value = entry.Value;
                    Update(c);
                }
                else
                {
                    Insert(entry);
                }
            }
        }
    }
}
