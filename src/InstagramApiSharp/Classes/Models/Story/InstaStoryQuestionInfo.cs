/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaStoryQuestionInfo
    {
        public long QuestionId { get; set; }

        public string Question { get; set; }

        public string QuestionType { get; set; }

        public string BackgroundColor { get; set; }

        public string TextColor { get; set; }

        public List<InstaStoryQuestionResponder> Responders { get; set; } = new List<InstaStoryQuestionResponder>();

        public string MaxId { get; set; }

        public bool MoreAvailable { get; set; }

        public int QuestionResponseCount { get; set; }

        public DateTime LatestQuestionResponseTime { get; set; }
    }
}
