using AutoMapper;
using EONET.Api.Client;
using EONET.BL.Abstraction;
using EONET.BL.Models;
using EONET.BL.Services;
using EONET.DAL.Entities;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EONET.UnitTests
{
    public class EventServiceTests
    {
        private Mock<IHttpEonetClient> _mockEonetClient;
        private Mock<IMapper> _mockMapper;
        private IEventService _eventService;

        [SetUp]
        public void Setup()
        {
            _mockEonetClient = new Mock<IHttpEonetClient>();
            _mockMapper = new Mock<IMapper>();
            _eventService = new EventService(_mockMapper.Object, _mockEonetClient.Object);
        }

        [Test]
        public async Task GetEventAsync_ReturnsEventById()
        {
            // Arrange
            var testId = "testId";
            _mockEonetClient.Setup(x => x.GetEventAsync(testId))
                .Returns(Task.FromResult(new Event()));
            _mockMapper.Setup(x => x.Map<EventModel>(It.IsAny<Event>()))
                .Returns(GetTestEvent());

            // Act
            var actualResult = await _eventService.GetEventAsync(testId);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(actualResult.IsOpen);
        }

        private EventModel GetTestEvent()
        {
            return new EventModel
            {
                Title = "title1",
                Id = "testId",
                IsOpen = true
            };
        }
    }
}