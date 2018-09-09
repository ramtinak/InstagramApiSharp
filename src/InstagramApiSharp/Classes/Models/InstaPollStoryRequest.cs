using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaPollStoryRequest
    {
        public double X { get; set; } = 0.5;
        public double Y { get; set; } = 0.5;
        //public double Z { get; set; } = 0;

        public double Width { get; set; } = 0.67407405;
        public double Height { get; set; } = 0.12417219;
        public double Rotation { get; set; } = 0.0;

        public string Question { get; set; }

        public string Answer1 { get; set; }
        public string Answer2 { get; set; }

    }
}
