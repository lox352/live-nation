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
        private readonly IRulesHelper _rulesHelper;

        public ConvertRangeService(IRulesHelper rulesHelper)
        {
            _rulesHelper = rulesHelper;
        }

        public ConvertedRange ConvertRange(RangeRequest range)
        {
            var countDictionary = new Dictionary<string, int>();

            var integers = Enumerable.Range(range.Start, range.Length);
            var convertedRange = integers.Select(integer => _rulesHelper.ApplyRule(integer, countDictionary));

            return new ConvertedRange()
            {
                Result = string.Join(' ', convertedRange),
                ResultSummary = countDictionary
            };
        }
    }
}