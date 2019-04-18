using System.Collections;
using System.Collections.Generic;
using LiveNation.Api.Options;
using Microsoft.Extensions.Options;

namespace LiveNation.Api.Helpers
{
    public interface IRulesHelper
    {
        bool TryApplyRules(int integer, out string convertedInteger);
    }

    public class RulesHelper : IRulesHelper
    {
        private readonly List<ConversionRule> _rules;

        public RulesHelper(IOptionsMonitor<List<ConversionRule>> rulesAccessor)
        {
            _rules = rulesAccessor.CurrentValue;
        }

        public bool TryApplyRules(int integer, out string convertedInteger)
        {
            string result = null;

            foreach (var rule in _rules)
            {
                var ruleShouldBeApplied = integer % rule.Divisor == 0;
                if (ruleShouldBeApplied)
                {
                    result += rule.OutputText;
                }
            }

            convertedInteger = result ?? integer.ToString();
            var rulesHaveBeenApplied = result != null;
            return rulesHaveBeenApplied;
        }
    }
}