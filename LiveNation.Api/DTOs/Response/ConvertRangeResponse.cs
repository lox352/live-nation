using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveNation.Api.DTOs.Response
{
    public class ConvertRangeResponse
    {
        public string Result { get; set; }
        public ResultSummary ResultSummary { get; set; }
    }

    public class ResultSummary
    {
        public int Live { get; set; }
        public int Nation { get; set; }
        public int LiveNation { get; set; }
        public int Integer { get; set; }
    }
}
