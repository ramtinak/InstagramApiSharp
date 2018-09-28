using System;
using System.Collections.Generic;
using System.Text;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Classes.Models.Business;
using InstagramApiSharp.Classes.ResponseWrappers;
using InstagramApiSharp.Classes.ResponseWrappers.Business;
using InstagramApiSharp.Helpers;
namespace InstagramApiSharp.Converters.Business
{
    internal class InstaStatisticsDataPointConverter : IObjectConverter<InstaStatisticsDataPointItem, InstaStatisticsDataPointItemResponse>
    {
        public InstaStatisticsDataPointItemResponse SourceObject { get; set; }

        public InstaStatisticsDataPointItem Convert()
        {
            var dataPoint = new InstaStatisticsDataPointItem
            {
                Label = SourceObject.Label,
                Value = SourceObject.Value ?? 0
            };
            return dataPoint;
        }
    }
}
