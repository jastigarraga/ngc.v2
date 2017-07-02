using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGC.UI.Models
{
    public class EmailTemplateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }

        public string Template { get; set; }

        public ICollection<int> Customers { get; set; }
    }
}
