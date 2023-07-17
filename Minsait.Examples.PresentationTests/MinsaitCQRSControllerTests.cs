#nullable disable

namespace Minsait.Examples.PresentationTests
{
    public class MinsaitCQRSControllerTests
    {
        private readonly Mock<ILogger<MinsaitCQRSController>> _logger;
        private readonly Mock<IMinsaitTestQueries> _minsaitTestQueries;
        private readonly CreateMinsaitTestCommandHandler _createCommandHandler;
        private readonly Mock<IMinsaitTestRepository> _repository;
        private readonly MinsaitCQRSController _controller;

        public MinsaitCQRSControllerTests()
        {
            _logger = new Mock<ILogger<MinsaitCQRSController>>();
            _minsaitTestQueries = new Mock<IMinsaitTestQueries>();
            _repository = new Mock<IMinsaitTestRepository>();

            var createCommandValidator = new CreateMinsaitTestCommandValidator();
            _createCommandHandler = new CreateMinsaitTestCommandHandler(createCommandValidator, _repository.Object);

            _controller = new MinsaitCQRSController(_logger.Object, _createCommandHandler, _minsaitTestQueries.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public async Task GetByIdAsync_Success(long fakeId)
        {
            //Arrange
            var fakeEntity = GetByIdFake(fakeId);

            _minsaitTestQueries.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(fakeEntity));

            _repository.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(fakeEntity));

            //Act
            var actionResult = await _controller.GetByIdAsync(fakeId);

            //Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.Equal(fakeId, (((ObjectResult)actionResult.Result).Value as MinsaitTest).Id);
        }

        [Fact]
        public async Task GetByIdAsync_Failure()
        {
            //Arrange
            var fakeId = 1;
            var fakeEntity = GetByIdFake(fakeId);

            _minsaitTestQueries.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .Throws<OutOfMemoryException>();

            //Act
            var actionResult = await _controller.GetByIdAsync(fakeId);

            //Assert
            Assert.Equal((actionResult.Result as BadRequestObjectResult).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public void GetAll_Count5_Success()
        {
            //Arrange
            var count = 5;
            var fakeEntities = GetAllFake();

            _minsaitTestQueries.Setup(x => x.GetQueryable())
                .Returns(fakeEntities);

            _repository.Setup(x => x.GetQueryable())
                .Returns(fakeEntities);

            //Act
            var actionResult = _controller.GetAsync();
            var result = (actionResult as OkObjectResult).Value as List<MinsaitTest>;

            //Assert
            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.True(result.Count == 5);
        }

        [Fact]
        public void GetAll_Count5_Failure()
        {
            //Arrange
            var fakeEntities = GetAllFake();

            _minsaitTestQueries.Setup(x => x.GetQueryable())
                .Throws<OutOfMemoryException>();

            //Act
            var actionResult = _controller.GetAsync();

            //Assert
            Assert.Equal((actionResult as BadRequestObjectResult).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_ValidCreateCommand_Success()
        {
            //Arrange
            var fakeCommand = CreateMinsaitTestFakeCommand();

            //Act
            var actionResult = await _controller.Post(fakeCommand);
            var result = (actionResult as OkObjectResult).Value as CommandResult<CreateMinsaitTestResult>;

            //Assert
            Assert.Equal((actionResult as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Post_InvalidCreateCommand_Failure()
        {
            //Arrange
            var fakeCommand = CreateMinsaitTestFakeCommand();
            fakeCommand.Value = -1;

            //Act
            var actionResult = await _controller.Post(fakeCommand);
            var result = (actionResult as BadRequestObjectResult).Value as CommandResult<CreateMinsaitTestResult>;

            //Assert
            Assert.Equal((actionResult as BadRequestObjectResult).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.False(result.Success);
            Assert.True(result.Errors.Count() > 0);
        }

        [Fact]
        public async Task Post_ValidCreateCommand_BadRequest()
        {
            //Arrange
            var fakeCommand = CreateMinsaitTestFakeCommand();

            _repository.Setup(x => x.SaveChangesAsync())
                .Throws<OutOfMemoryException>();

            //Act
            var actionResult = await _controller.Post(fakeCommand);

            //Assert
            Assert.Equal((actionResult as BadRequestObjectResult).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            _repository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        private MinsaitTest GetByIdFake(long fakeId)
        {
            return new MinsaitTest()
            {
                Id = fakeId
            };
        }

        private CreateMinsaitTestCommand CreateMinsaitTestFakeCommand()
        {
            return new CreateMinsaitTestCommand
            {
                Name = "Name",
                Active = true,
                Creation = DateTime.Now,
                Value = 1
            };
        }

        private IQueryable<MinsaitTest> GetAllFake()
        {
            var list = new List<MinsaitTest>();
            list.Add(new MinsaitTest { Id = 1 });
            list.Add(new MinsaitTest { Id = 2 });
            list.Add(new MinsaitTest { Id = 3 });
            list.Add(new MinsaitTest { Id = 4 });
            list.Add(new MinsaitTest { Id = 5 });

            return list.AsQueryable();
        }
    }
}
