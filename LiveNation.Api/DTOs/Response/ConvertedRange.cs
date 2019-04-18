using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace LiveNation.Api.DTOs.Response
{
    public class ConvertedRange
    {
        public string Result { get; set; }
        public OrderedDictionary Summary { get; set; }
    }
}
