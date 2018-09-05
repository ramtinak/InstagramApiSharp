using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes
{
    public class InstaStoryShare
    {
        public InstaMedia Media { get; set; }
        public string ReelType { get; set; }
        public bool IsReelPersisted { get; set; }
        public string Text { get; set; }
        public bool IsLinked { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
