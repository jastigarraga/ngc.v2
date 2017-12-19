using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.Model
{
    public class PhotoCollection
    {
        public int IdUser { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
