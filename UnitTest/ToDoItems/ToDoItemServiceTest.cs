using Application.Commons;
using Application.ToDoItems;
using Application.ToDoItems.DTOs;
using Domain.Commons;
using Domain.ToDoItems;
using FluentAssertions;
using Infrastructure.Exceptions;
using Moq;
using UnitTest.Configurations;
using UnitTest.ToDoItems.Testables;

namespace UnitTest.ToDoItems
{
    public class ToDoItemServiceTest
    {
        private readonly Mock<IToDoItemRepository> _mockToDoItemRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly ToDoItemService _service;
        public ToDoItemServiceTest()
        {
            TestMapsterConfig.Configure();  // init mapster
            _mockToDoItemRepo = new Mock<IToDoItemRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new ToDoItemService(
                _mockToDoItemRepo.Object,
                _mockUnitOfWork.Object
                );
        }

        /*--------------------------------------------------------List-------------------------------------------------------*/
        [Fact]
        public async Task ListToDoItems_ReturnsOrderedUndoneItems()
        {
            var item1 = new TestableToDoItem("name1", "description1", ToDoItemPriority.Low, new DateTime(2025, 03, 22), false);
            var item2 = new TestableToDoItem("name2", "description2", ToDoItemPriority.Medium, new DateTime(2025, 03, 25), true);
            var item3 = new TestableToDoItem("name3", "description3", ToDoItemPriority.High, new DateTime(2025, 03, 24), false);
            var item4 = new TestableToDoItem("name4", "description4", ToDoItemPriority.Medium, new DateTime(2025, 03, 24), true);

            var items = new List<ToDoItem>() { item1, item2, item3, item4 };

            var mockSpec = new Mock<ISpecification<ToDoItem>>();
            mockSpec.Setup(spec => spec.ToExpressAll()).Returns(i => !i.IsDone);

            _mockToDoItemRepo.Setup(repo => repo.ListToDoItemsAsync(It.IsAny<Specification<ToDoItem>>()))
                .ReturnsAsync(items
                    .Where(mockSpec.Object.ToExpressAll().Compile())
                    .OrderByDescending(i => i.Priority)
                    .ThenByDescending(i => i.CreatedAt)
                    .ToList()
                 );

            var result = await _service.ListToDoItemsAsync(false);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            var expectedDTOList = new List<ToDoItemListDTO>
            {
                new ToDoItemListDTO { name = item3.Name, description = item3.Description, priority = item3.Priority, createdAt = item3.CreatedAt, isDone = item3.IsDone },
                new ToDoItemListDTO { name = item1.Name, description = item1.Description, priority = item1.Priority, createdAt = item1.CreatedAt, isDone = item1.IsDone }
            };

            result.Should().BeEquivalentTo(expectedDTOList, options => options.WithStrictOrdering()); // check order

            result[0].name.Should().Be(item3.Name);
            result[0].description.Should().Be(item3.Description);
            result[0].priority.Should().Be(item3.Priority);
            result[0].isDone.Should().BeFalse();

            result[1].name.Should().Be(item1.Name);
            result[1].description.Should().Be(item1.Description);
            result[1].priority.Should().Be(item1.Priority);
            result[1].isDone.Should().BeFalse();
        }

        [Fact]
        public async Task ListToDoItems_ReturnsEmptyList()
        {
            var emptyItems = new List<ToDoItem>();
            var mockSpec = new Mock<ISpecification<ToDoItem>>();
            mockSpec.Setup(spec => spec.ToExpressAll()).Returns(i => !i.IsDone);

            _mockToDoItemRepo.Setup(repo => repo.ListToDoItemsAsync(It.IsAny<Specification<ToDoItem>>())).ReturnsAsync(emptyItems);

            var result = await _service.ListToDoItemsAsync(false);

            result.Should().BeEmpty();
        }

        /*--------------------------------------------------------Create-------------------------------------------------------*/
        [Fact]
        public async Task CreateToDoItem_ReturnsOk()
        {
            var name = "name";
            var priority = ToDoItemPriority.Low;
            var dto = new ToDoItemCreateDTO
            {
                name = name,
                priority = priority
            };

            ToDoItem? capturedItem = null;

            _mockToDoItemRepo.Setup(repo => repo.CreateToDoItemAsync(It.IsAny<ToDoItem>()))
                .Callback<ToDoItem>(item => capturedItem = item);

            await _service.CreateToDoItemAsync(dto);

            _mockToDoItemRepo.Verify(repo => repo.CreateToDoItemAsync(It.IsAny<ToDoItem>()), Times.Once);

            capturedItem.Should().NotBeNull();
            capturedItem!.Name.Should().Be(name);
            capturedItem.Priority.Should().Be(priority);
        }

        [Fact]
        public async Task CreateToDoItem_Returns400RequiredName()
        {
            var priority = ToDoItemPriority.Low;
            var dto = new ToDoItemCreateDTO
            {
                priority = priority
            };

            await FluentActions
                .Invoking(() => _service.CreateToDoItemAsync(dto))
                .Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage(ErrorMessages.REQUIRED_TO_DO_ITEM_NAME);
        }

        [Fact]
        public async Task CreateToDoItem_Returns400InvalidPriority()
        {
            var name = "name";
            var dto = new ToDoItemCreateDTO
            {
                name = name,
                priority = 0
            };

            await FluentActions
                .Invoking(() => _service.CreateToDoItemAsync(dto))
                .Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage(ErrorMessages.INVALID_TO_DO_ITEM_PRIORITY);
        }

        /*--------------------------------------------------------Update-------------------------------------------------------*/
        [Fact]
        public async Task UpdateUndoneToDoItem_ReturnsOk()
        {
            var item = new TestableToDoItem("name", "desc", ToDoItemPriority.Low, new DateTime(2025, 03, 25), false);

            _mockToDoItemRepo.Setup(repo => repo.GetToDoItemAsync(It.IsAny<int>())).ReturnsAsync(item);

            await _service.UpdateToDoItemAsync(item.Id);

            item.IsDone.Should().BeTrue();

            _mockUnitOfWork.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDoneToDoItem_Returns400AlreadyDoneItem()
        {
            var item = new TestableToDoItem("name", "desc", ToDoItemPriority.Low, new DateTime(2025, 03, 25), true);

            _mockToDoItemRepo.Setup(repo => repo.GetToDoItemAsync(It.IsAny<int>())).ReturnsAsync(item);

            await FluentActions
                .Invoking(() => _service.UpdateToDoItemAsync(item.Id))
                .Should()
                .ThrowAsync<BadRequestException>()
                .WithMessage(ErrorMessages.ALREADY_DONE_TO_DO_ITEM);
        }

        [Fact]
        public async Task UpdateNotExistingToDoItem_Returns404NotFoundItem()
        {
            var notExistingId = 0;

            _mockToDoItemRepo.Setup(repo => repo.GetToDoItemAsync(It.IsAny<int>())).ReturnsAsync((ToDoItem?)null);

            await FluentActions
                .Invoking(() => _service.UpdateToDoItemAsync(notExistingId))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(ErrorMessages.NOT_FOUND_TO_DO_ITEM);
        }
    }
}
