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
    internal class InstaProductConverter : IObjectConverter<InstaProductTag, InstaProductContainerResponse>
    {
        public InstaProductContainerResponse SourceObject { get; set; }

        public InstaProductTag Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var productTag = new InstaProductTag
            {
                Position = new InstaPosition(SourceObject.Position[0], SourceObject.Position[1])
            };
            var source = SourceObject.Product;
            var product = new InstaProduct
            {
                CheckoutStyle = source.CheckoutStyle,
                CurrentPrice = source.CurrentPrice,
                ExternalUrl = source.ExternalUrl,
                FullPrice = source.FullPrice,
                HasViewerSaved = source.HasViewerSaved,
                Merchant = ConvertersFabric.Instance.GetMerchantConverter(source.Merchant).Convert(),
                Name = source.Name,
                Price = source.Price,
                ProductId = source.ProductId,
                ReviewStatus = source.ReviewStatus
            };
            if (source.MainImage != null && source.MainImage.Images != null
                && source.MainImage.Images.Candidates.Any())
            {
                foreach (var image in source.MainImage.Images.Candidates)
                {
                    try
                    {
                        product.MainImage.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
                    }
                    catch { }
                }
            }
            if (source.ThumbnailImage != null && source.ThumbnailImage.Images != null
                && source.ThumbnailImage.Images.Candidates.Any())
            {
                foreach (var image in source.ThumbnailImage.Images.Candidates)
                {
                    try
                    {
                        product.ThumbnailImage.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
                    }
                    catch { }
                }
            }
            productTag.Product = product;
            return productTag;
        }
    }
}
