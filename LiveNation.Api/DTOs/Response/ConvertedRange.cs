using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveNation.Api.DTOs.Response
{
    public class ConvertedRange
    {
        public string Result { get; set; }
        public Dictionary<string, int> ResultSummary { get; set; }
    }
}
