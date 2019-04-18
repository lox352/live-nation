using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LiveNation.Api.DTOs.Request;
using LiveNation.Api.DTOs.Response;
using LiveNation.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace LiveNation.Api.Services
{
    public interface IConvertRangeService
    {
        ConvertedRange ConvertRange(RangeRequest range);
    }

    public class ConvertRangeService : IConvertRangeService
    {
        private readonly IConvertedRangeBuilder _convertedRangeBuilder;

        public ConvertRangeService(IConvertedRangeBuilder convertedRangeBuilder)
        {
            _convertedRangeBuilder = convertedRangeBuilder;
        }

        public ConvertedRange ConvertRange(RangeRequest range)
        {
            for (var i = range.Start; i <= range.End; i++)
            {
                _convertedRangeBuilder.AddElement(i);
            }

            return _convertedRangeBuilder.Build();
        }
    }
}