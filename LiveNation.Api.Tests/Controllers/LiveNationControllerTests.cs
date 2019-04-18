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
        [InlineData(-5,0, "Nation -4 Live -2 -1 0")]
        public async Task Get_EndpointsReturnSuccessAndCorrectResult(int rangeStart, int rangeEnd, string expectedResult)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/ConvertRange?start={rangeStart}&end={rangeEnd}";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var deserialisedResponseContent = await GetDeserialisedResponseContentAsync(response);
            Assert.Equal(expectedResult, deserialisedResponseContent.Result);
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectSummary()
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = "/ConvertRange?start=1&end=20";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var deserialisedResponseContent = await GetDeserialisedResponseContentAsync(response);
            Assert.Equal(5, deserialisedResponseContent.ResultSummary["Live"]);
            Assert.Equal(3, deserialisedResponseContent.ResultSummary["Nation"]);
            Assert.Equal(1, deserialisedResponseContent.ResultSummary["LiveNation"]);
            Assert.Equal(11, deserialisedResponseContent.ResultSummary["integer"]);
        }

        private static async Task<ConvertedRange> GetDeserialisedResponseContentAsync(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserialisedResponseContent = JsonConvert.DeserializeObject<ConvertedRange>(responseContent);
            return deserialisedResponseContent;
        }
    }
}
