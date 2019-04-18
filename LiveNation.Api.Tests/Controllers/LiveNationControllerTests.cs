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
        [InlineData(-5,0, "Nation -4 Live -2 -1 LiveNation")]
        public async Task Get_EndpointsReturnSuccessAndCorrectResult(int rangeStart, int rangeEnd, string expectedResult)
        {
            // Arrange
            var client = _factory.CreateClient();
            var url = $"/ConvertRange?start={rangeStart}&end={rangeEnd}";

            // Act
            var response = await client.GetAsync(url);
            var deserialisedResponseContent = await GetDeserialisedResponseContentAsync(response);

            // Assert
            response.EnsureSuccessStatusCode();

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
            var deserialisedResponseContent = await GetDeserialisedResponseContentAsync(response);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal((long)5, deserialisedResponseContent.Summary["Live"]);
            Assert.Equal((long)3, deserialisedResponseContent.Summary["Nation"]);
            Assert.Equal((long)1, deserialisedResponseContent.Summary["LiveNation"]);
            Assert.Equal((long)11, deserialisedResponseContent.Summary["integer"]);
        }

        private static async Task<ConvertedRange> GetDeserialisedResponseContentAsync(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserialisedResponseContent = JsonConvert.DeserializeObject<ConvertedRange>(responseContent);
            return deserialisedResponseContent;
        }
    }
}
