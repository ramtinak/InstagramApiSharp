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
    internal class InstaProductConverter : IObjectConverter<InstaProduct, InstaProductResponse>
    {
        public InstaProductResponse SourceObject { get; set; }

        public InstaProduct Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException($"Source object");
            var product = new InstaProduct
            {
                CheckoutStyle = SourceObject.CheckoutStyle,
                CurrentPrice = SourceObject.CurrentPrice,
                ExternalUrl = SourceObject.ExternalUrl,
                FullPrice = SourceObject.FullPrice,
                HasViewerSaved = SourceObject.HasViewerSaved,
                Merchant = ConvertersFabric.Instance.GetMerchantConverter(SourceObject.Merchant).Convert(),
                Name = SourceObject.Name,
                Price = SourceObject.Price,
                ProductId = SourceObject.ProductId,
                ReviewStatus = SourceObject.ReviewStatus,
                CurrentPriceStripped = SourceObject.CurrentPriceStripped,
                FullPriceStripped = SourceObject.FullPriceStripped,
                ProductAppealReviewStatus = SourceObject.ProductAppealReviewStatus
            };
            if (SourceObject.MainImage?.Images?.Candidates?.Count > 0)
            {
                foreach (var image in SourceObject.MainImage.Images.Candidates)
                {
                    try
                    {
                        product.MainImage.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
                    }
                    catch { }
                }
            }
            if (SourceObject.ThumbnailImage?.Images?.Candidates?.Count > 0)
            {
                foreach (var image in SourceObject.ThumbnailImage.Images.Candidates)
                {
                    try
                    {
                        product.ThumbnailImage.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
                    }
                    catch { }
                }
            }
            if (SourceObject.ProductImages?.Count > 0)
            {
                foreach (var productImage in SourceObject.ProductImages)
                {
                    if (productImage?.Images?.Candidates?.Count > 0)
                    {
                        foreach (var image in productImage.Images.Candidates)
                        {
                            try
                            {
                                product.ThumbnailImage.Add(new InstaImage(image.Url, int.Parse(image.Width), int.Parse(image.Height)));
                            }
                            catch { }
                        }
                    }
                }
            }
            return product;
        }
    }
}
