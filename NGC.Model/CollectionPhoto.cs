using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.Model
{
    public class CollectionPhoto
    {
        public int IdCollection { get; set; }
        public int IdPhoto { get; set; }
        public int Order { get; set; }
        public bool Favourite { get; set; }

        public virtual PhotoCollection Collecion { get; set; }
        public virtual Photo Photo { get; set;}
    }
}
