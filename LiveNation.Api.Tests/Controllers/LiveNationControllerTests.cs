using System.Net.Http;
using System.Threading.Tasks;
using LiveNation.Api.DTOs.Response;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace LiveNation.Api.Tests.Controllers
{
    public class LiveNationControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public LiveNationControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(1,20, "1 2 Live 4 Nation Live 7 8 Live Nation 11 Live 13 14 LiveNation 16 17 Live 19 Nation")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(int rangeStart, int rangeEnd, string expectedResult)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/ConvertRange?start={rangeStart}&end={rangeEnd}";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var deserialisedResponseContent = await GetDeserialisedResponseContentAsync(response);
            Assert.Equal(expectedResult, deserialisedResponseContent.Result);
        }

        private static async Task<ConvertedRange> GetDeserialisedResponseContentAsync(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserialisedResponseContent = JsonConvert.DeserializeObject<ConvertedRange>(responseContent);
            return deserialisedResponseContent;
        }
    }
}
