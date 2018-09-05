using InstagramApiSharp.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleAccountExample
{
    public class InstaApiList : List<IInstaApi>
    {
        public IInstaApi CurrentInstaApi { get; set; }
        public void SaveSessions()
        {
            Helper.CreateAccountDirectory();
            if (this == null)
                return;
            if(this.Any())
            {
                foreach(var instaApi in this)
                {
                    var state = instaApi.GetStateDataAsStream();
                    var path = Path.Combine(Helper.AccountPathDirectory, $"{instaApi.GetLoggedUser().UserName}.bin");
                    using (var fileStream = File.Create(path))
                    {
                        state.Seek(0, SeekOrigin.Begin);
                        state.CopyTo(fileStream);
                    }
                }
            }
        }
    }
    class MultipleHelper
    {
    }
}
