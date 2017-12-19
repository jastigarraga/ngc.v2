using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NGC.UI.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string Title { get; set; }

        public int IdCustomer { get; set; }

        public String src { get; set; }

        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
    }
}
