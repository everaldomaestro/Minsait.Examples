using AutoFixture;

namespace Minsait.Examples.ApplicationTests
{
    public class CreateMinsaitTestCommandHandlerTest
    {
        private readonly CreateMinsaitTestCommandHandler _createCommandHandler;
        private readonly Mock<IMinsaitTestRepository> _repository;

        public CreateMinsaitTestCommandHandlerTest()
        {
            _repository = new Mock<IMinsaitTestRepository>();

            var createCommandValidator = new CreateMinsaitTestCommandValidator();

            _createCommandHandler = new CreateMinsaitTestCommandHandler(createCommandValidator, _repository.Object);
        }

        [Fact]
        public async Task CreateMinsaitTest_IsValid_Success()
        {
            //Arrange
            var fixture = new Fixture();

            var command = fixture.Create<CreateMinsaitTestCommand>();

            //var command = new CreateMinsaitTestCommand
            //{
            //    Name = "Name",
            //    Active = true,
            //    Creation = DateTime.Now,
            //    Value = 1
            //};

            //Act
            var result = await _createCommandHandler.Handle(command);

            //Assert
            _repository.Verify(r => r.AddAsync(It.IsAny<MinsaitTest>()), Times.Once);
        }

        [Fact]
        public async Task CreateMinsaitTest_ValueInvalid_Failure()
        {
            //Arrange
            var command = new CreateMinsaitTestCommand
            {
                Name = "Name",
                Active = true,
                Creation = DateTime.Now,
                Value = 0
            };

            //Act
            await _createCommandHandler.Handle(command);

            //Assert
            _repository.Verify(r => r.AddAsync(It.IsAny<MinsaitTest>()), Times.Never);
        }
    }
}
