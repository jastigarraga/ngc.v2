using System;
using System.Collections.Generic;
using System.Text;

namespace NGC.Model
{
    public class MerakiTextImage : Entity
    {
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Name { get; set; }
        public double X { get; set; }

        public double Y { get;set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public string Text { get; set; }

        public string FontName { get; set; }
    }
}
