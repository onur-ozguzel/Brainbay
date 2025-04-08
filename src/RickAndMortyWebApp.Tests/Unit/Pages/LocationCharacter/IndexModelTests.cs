using Moq;
using RickAndMortyWebApp.Models;
using RickAndMortyWebApp.Pages.LocationCharacter;
using RickAndMortyWebApp.Services;

namespace RickAndMortyWebApp.Tests.Unit.Pages.LocationCharacter
{
    [Trait("Category", "Unit")]
    public class IndexModelTests
    {
        [Fact]
        public async Task OnGet_ShouldSetPaginatedResults_WhenServiceReturnsData()
        {
            // Arrange
            var mockService = new Mock<ICharacterService>();
            var testLocation = "Citadel of Ricks";
            var page = 2;
            var size = 5;
            var data = new List<LocationCharacterListModel> { new()
            {
                Id = 1, Name = "Rick Sanchez", Gender = "Male", Species = "Human", OriginName = "Earth (C-137)", Image = "rick.png"
            } };

            var expectedResult = new PaginationModel<LocationCharacterListModel>(page, size, 3, data);

            mockService.Setup(s => s.GetCharactersByLocation(page, size, testLocation))
                           .ReturnsAsync(expectedResult);

            var pageModel = new IndexModel(mockService.Object);

            // Act
            await pageModel.OnGet(testLocation, page, size);

            // Assert
            Assert.Equal(testLocation, pageModel.LocationName);
            Assert.NotNull(pageModel.PaginatedResults);
            Assert.Equal(page, pageModel.PaginatedResults!.PageIndex);
            Assert.Single(pageModel.PaginatedResults!.Data);
            mockService.Verify(s => s.GetCharactersByLocation(page, size, testLocation), Times.Once);
        }

        [Fact]
        public async Task OnGet_ShouldSetEmptyPaginatedResults_WhenNoCharactersReturned()
        {
            // Arrange
            var mockService = new Mock<ICharacterService>();
            var location = "Nowhere";
            var emptyResult = new PaginationModel<LocationCharacterListModel>(
                1, 10, 0, new List<LocationCharacterListModel>());

            mockService.Setup(s => s.GetCharactersByLocation(1, 10, location))
                       .ReturnsAsync(emptyResult);

            var pageModel = new IndexModel(mockService.Object);

            // Act
            await pageModel.OnGet(location);

            // Assert
            Assert.Equal(location, pageModel.LocationName);
            Assert.NotNull(pageModel.PaginatedResults);
            Assert.Empty(pageModel.PaginatedResults.Data);
            Assert.Equal(0, pageModel.PaginatedResults.TotalPages);
        }

        [Fact]
        public async Task OnGet_ShouldThrow_WhenServiceThrowsException()
        {
            // Arrange
            var mockService = new Mock<ICharacterService>();
            mockService.Setup(s => s.GetCharactersByLocation(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                       .ThrowsAsync(new Exception("DB failure"));

            var pageModel = new IndexModel(mockService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => pageModel.OnGet("AnyLocation"));
        }
    }
}
