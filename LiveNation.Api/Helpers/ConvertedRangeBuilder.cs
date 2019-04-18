using System.Collections.Generic;
using System.Collections.Specialized;
using LiveNation.Api.DTOs.Response;

namespace LiveNation.Api.Helpers
{
    public interface IConvertedRangeBuilder
    {
        void AddElement(int integer);
        ConvertedRange Build();
    }

    public class ConvertedRangeBuilder : IConvertedRangeBuilder
    {
        private const string NoRuleAppliedKeyName = "integer";
        private readonly IRulesHelper _rulesHelper;

        public ConvertedRangeBuilder(IRulesHelper rulesHelper)
        {
            _rulesHelper = rulesHelper;
        }

        private List<string> Results { get; set; } = new List<string>();
        private int NoRuleWasAppliedCount { get; set; } = 0;
        private OrderedDictionary CountDictionary { get; set; } = new OrderedDictionary();

        public void AddElement(int integer)
        {
            var someRulesWereApplied = _rulesHelper.TryApplyRules(integer, out var convertedInteger);
            Results.Add(convertedInteger);
            if (someRulesWereApplied)
            {
                IncrementCountDictionary(convertedInteger);
            }
            else
            {
                NoRuleWasAppliedCount++;
            }
        }

        private void IncrementCountDictionary(string input)
        {
            if (CountDictionary.Contains(input))
            {
                CountDictionary[input] = (int)CountDictionary[input] + 1;
            }
            else
            {
                CountDictionary[input] = 1;
            }
        }

        public ConvertedRange Build()
        {
            CountDictionary[NoRuleAppliedKeyName] = NoRuleWasAppliedCount;

            var convertedRange = new ConvertedRange()
            {
                Result = string.Join(' ', Results),
                Summary = CountDictionary
            };

            return convertedRange;
        }
    }


}