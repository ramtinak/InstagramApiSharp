using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaSuggestionItem
    {
        public InstaUserShort User { get; set; }

        public string SocialContext { get; set; }
        
        public string Caption { get; set; }

        public bool IsNewSuggestion { get; set; }

    }
}
