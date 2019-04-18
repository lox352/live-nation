using System.Collections.Generic;

namespace LiveNation.Api.Helpers
{
    public interface IRulesHelper
    {
        string ApplyRules(int integer, Dictionary<string, int> resultsSummary);
    }

    public class RulesHelper : IRulesHelper
    {
        public string ApplyRules(int integer, Dictionary<string, int> resultsSummary)
        {
            throw new System.NotImplementedException();
        }
    }
}