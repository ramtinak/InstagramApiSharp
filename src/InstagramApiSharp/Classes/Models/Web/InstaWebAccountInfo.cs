using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaWebAccountInfo
    {
        public DateTime? JoinedDate { get; set; } = DateTime.MinValue;

        public DateTime? SwitchedToBusinessDate { get; set; } = DateTime.MinValue;
    }
}
