using LiveNation.Api.DTOs.Request;
using LiveNation.Api.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace LiveNation.Api.Services
{
    public interface IConvertRangeService
    {
        ConvertedRange ConvertRange(RangeRequest range);
    }
}