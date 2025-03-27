using Application.Commons;
using Application.ToDoItems;
using Application.ToDoItems.DTOs;
using Domain.Commons;
using Domain.ToDoItems;
using FluentAssertions;
using Moq;
using System.Linq;
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
            var item1 = new TestableToDoItem("name1", "description1", ToDoItemPriority.low, new DateTime(2025, 03, 22), false);
            var item2 = new TestableToDoItem("name2", "description2", ToDoItemPriority.medium, new DateTime(2025, 03, 25), true);
            var item3 = new TestableToDoItem("name3", "description3", ToDoItemPriority.high, new DateTime(2025, 03, 24), false);
            var item4 = new TestableToDoItem("name4", "description4", ToDoItemPriority.medium, new DateTime(2025, 03, 24), true);

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
    }
}
