using EONET.BL.Abstraction;
using EONET.BL.Enums;
using EONET.BL.Models;
using EONET.Web.Controllers;
using EONET.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EONET.UnitTests
{
    public class EventControllerTests
    {
        private Mock<IEventService> _mockEventService;
        private EventController _mockEventController;

        [SetUp]
        public void Setup()
        {
            _mockEventService = new Mock<IEventService>();
            _mockEventController = new EventController(_mockEventService.Object);
        }

        [Test]
        public async Task GetEventsAsync_ReturnsActionResult_WithAListOfEvents()
        {
            // Arrange
            var paginationFilter = new PaginationFilter { PageNumber = 1, PageSize = 10 };
            var filter = new EventFilterModel { Status = Status.Open, SortField = "Test" };
            _mockEventService.Setup(x => x.GetEventsAsync(filter))
                .Returns(Task.FromResult(GetTestEvents()));

            // Act
            var actionResult = await _mockEventController.GetEventsAsync(paginationFilter, filter);
            var okResult = actionResult as OkObjectResult;
            var realResult = okResult?.Value as PagedResponse<List<EventModel>>;

            // Assert
            Assert.IsInstanceOf<ActionResult>(actionResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(2, realResult?.TotalRecords);
        }

        [Test]
        public async Task GetEventAsync_ReturnsActionResult_WithEventsById()
        {
            // Arrange
            var testId = "testId";
            _mockEventService.Setup(x => x.GetEventAsync(testId))
                .Returns(Task.FromResult(GetTestEvent()));

            // Act
            var actionResult = await _mockEventController.GetEventAsync(testId);
            var okResult = actionResult as OkObjectResult;
            var realResult = okResult?.Value as EventModel;

            // Assert
            Assert.IsInstanceOf<ActionResult>(actionResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(testId, realResult?.Id);
        }

        private EventModel GetTestEvent()
        {
            return new EventModel
            {
                Title = "title1",
                Id = "testId"
            };
        }

        private List<EventModel> GetTestEvents()
        {
            var eventModels = new List<EventModel>();

            eventModels.Add(new EventModel()
            {
                Closed = new DateTime(2020, 7, 2),
                Id = "test_id",
                Title = "Test One"
            });
            eventModels.Add(GetTestEvent());

            return eventModels;
        }
    }
}