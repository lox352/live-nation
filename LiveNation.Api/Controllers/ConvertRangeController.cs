using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiveNation.Api.DTOs.Request;
using LiveNation.Api.DTOs.Response;
using LiveNation.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveNation.Api.Controllers
{
    [Route("convertrange")]
    [ApiController]
    public class ConvertRangeController : ControllerBase
    {
        private readonly IConvertRangeService _convertRangeService;

        public ConvertRangeController(IConvertRangeService convertRangeService)
        {
            _convertRangeService = convertRangeService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<ConvertedRange> Get(RangeRequest range)
        {
            var convertedRange = _convertRangeService.ConvertRange(range);
            return convertedRange;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
