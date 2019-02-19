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
using System.Linq;
using System.Text;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.ResponseWrappers;

namespace InstagramApiSharp.Converters
{
    internal class InstaProductContainerConverter : IObjectConverter<InstaProductTag, InstaProductContainerResponse>
    {
        public InstaProductContainerResponse SourceObject { get; set; }

        public InstaProductTag Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var productTag = new InstaProductTag
            {
                Product = ConvertersFabric.Instance.GetProductConverter(SourceObject.Product).Convert()
            };

            if (SourceObject.Position != null)
                productTag.Position = new InstaPosition(SourceObject.Position[0], SourceObject.Position[1]);

            return productTag;
        }
    }
}
