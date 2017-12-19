using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NGC.Model
{
    public class Photo : Entity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }

        public int IdCustomer { get; set; }
        public virtual Customer Customer { get; set; }

        public byte[] bytes { get; set; }
    }
}
