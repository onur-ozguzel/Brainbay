using Microsoft.AspNetCore.Mvc.Testing;
using RickAndMortyWebApp.Tests.TestUtilities;
using System.Net;

namespace RickAndMortyWebApp.Tests.Integration
{
    /// <summary>
    /// NOTE: Other pages should have integrations tests but I didn't add them for the simplicity
    /// </summary>
    [Trait("Category", "Integration")]
    public class LocationCharacterPageTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public LocationCharacterPageTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(); // In-memory test server
        }

        [Fact]
        public async Task Get_LocationCharacterPage_ReturnsSuccessAndContainsHeading()
        {
            // Arrange
            var locationName = "Citadel of Ricks";
            var page = 1;
            var pageSize = 5;
            var url = $"/LocationCharacter?locationName={locationName}&currentPage={page}&pageSize={pageSize}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();

            // Optional: Check expected heading or partial content
            Assert.Contains($"Alive Characters from {locationName}", content);
        }

        [Fact]
        public async Task Get_LocationCharacterPage_ShouldRenderPagination_WhenMultiplePages()
        {
            // Arrange
            var url = "/LocationCharacter?locationName=Citadel%20of%20Ricks&pageSize=5&currentPage=1";

            // Act
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Previous", content);
            Assert.Contains("Next", content);
            Assert.Contains("page-link", content);
        }

        [Fact]
        public async Task Get_LocationCharacterPage_ShouldShowNoCharactersMessage_WhenEmptyResult()
        {
            // Arrange
            var url = "/LocationCharacter?locationName=UnknownPlanet";

            // Act
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("No characters found for this location.", content);
        }

        [Theory]
        [InlineData("/LocationCharacter?locationName=Citadel&pageSize=not-a-number")]
        [InlineData("/LocationCharacter")]
        public async Task Get_LocationCharacterPage_ShouldNotCrash_OnInvalidOrMissingQueryParams(string url)
        {
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode); 
        }

        [Fact]
        public async Task Get_LocationCharacterPage_ShouldDisplayCharacterTable_WhenDataExists()
        {
            var url = "/LocationCharacter?locationName=Citadel%20of%20Ricks&pageSize=5";

            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            Assert.Contains("<table", content);
            Assert.Contains("Rick Sanchez", content);
        }

        [Fact]
        public async Task LocationCharacterPage_ShouldReturnDatabaseHeaderOnlyOnFirstCall()
        {
            // Arrange
            var url = "/LocationCharacter?locationName=Citadel&pageSize=5&currentPage=1";

            // Act - First Call
            var firstResponse = await _client.GetAsync(url);
            firstResponse.EnsureSuccessStatusCode();
            var firstHasDbHeader = firstResponse.Headers.TryGetValues("from-database", out var firstHeaderValues);

            // Assert First Call
            Assert.True(firstHasDbHeader);

            // Act - Second Call (should be from OutputCache)
            var secondResponse = await _client.GetAsync(url);
            secondResponse.EnsureSuccessStatusCode();
            var secondHasDbHeader = secondResponse.Headers.TryGetValues("from-database", out _);

            // Assert Second Call
            Assert.False(secondHasDbHeader); // No header on cached response
        }

        [Fact]
        public async Task LocationCharacterPage_ShouldRenderExpectedNumberOfRows()
        {
            var url = "/LocationCharacter?locationName=Citadel%20of%20Ricks&pageSize=5";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            HtmlAssert.TableRowCount(content, 5);
        }

        [Fact]
        public async Task LocationCharacterPage_ShouldRenderNoRows_WhenNoCharactersFound()
        {
            var url = "/LocationCharacter?locationName=NotExist&pageSize=5";

            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            HtmlAssert.TableRowCount(content, 0);
        }
    }
}
