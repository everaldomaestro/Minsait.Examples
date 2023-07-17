#nullable disable

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Minsait.Examples.Api.Mapping;
using Minsait.Examples.Api.ViewModels;
using Minsait.Examples.Domain.Interfaces.Application.Services;
using System.Data.Common;

namespace Minsait.Examples.PresentationTests
{
    public class MinsaitControllerTest
    {
        private readonly Mock<ILogger<MinsaitController>> _loggerMock;
        private readonly Mock<IMinsaitTestService> _testService;
        private readonly Mock<IMinsaitTestRepository> _testRepository;
        private readonly IMapper _mapper;

        public MinsaitControllerTest()
        {
            _loggerMock = new Mock<ILogger<MinsaitController>>();
            _testService = new Mock<IMinsaitTestService>();
            _testRepository = new Mock<IMinsaitTestRepository>();

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task GetById_WithFakeId_Success()
        {
            //Arrange
            long fakeId = 1;
            var fakeEntity = GetByIdFakeModel(fakeId);

            _testService.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(fakeEntity);

            //_testRepository.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            //    .Returns(Task.FromResult(fakeEntity));

            //Act
            var controller = new MinsaitController(_loggerMock.Object, _testService.Object, _mapper);
            var actionResult = await controller.Get(fakeId);

            //Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.Equal(fakeId, (((ObjectResult)actionResult.Result).Value as MinsaitTestViewModel).Id);
        }

        [Fact]
        public async Task GetById_WithNullEntity_Success()
        {
            //Arrange
            long fakeId = 1;
            MinsaitTest fakeEntity = null;

            _testService.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(fakeEntity));

            _testRepository.Setup(x => x.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(fakeEntity));

            //Act
            var controller = new MinsaitController(_loggerMock.Object, _testService.Object, _mapper);
            var actionResult = await controller.Get(fakeId);

            //Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.Null(((ObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void GetAll_Count5_Success()
        {
            //Arrange
            var count = 5;
            var fakeEntities = GetAllFakeModel();

            _testService.Setup(x => x.GetQueryable())
                .Returns(fakeEntities);

            _testRepository.Setup(x => x.GetQueryable())
                .Returns(fakeEntities);

            //Act
            var controller = new MinsaitController(_loggerMock.Object, _testService.Object, _mapper);
            var actionResult = controller.GetAll();

            //Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.Equal(count, (((ObjectResult)actionResult.Result).Value as IEnumerable<MinsaitTestViewModel>).Count());
        }

        [Fact]
        public async Task Post_ValidModel_Success()
        {
            //Arrange
            var fakeModel = new MinsaitTestViewModel
            {
                Name = "test",
                Active = true,
                Value = 1
            };

            //Act
            var controller = new MinsaitController(_loggerMock.Object, _testService.Object, _mapper);
            var actionResult = await controller.Post(fakeModel);

            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.OK, (actionResult as OkResult).StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task Delete_FakeId_Success(long fakeId)
        {
            //Arrange

            //Act
            var controller = new MinsaitController(_loggerMock.Object, _testService.Object, _mapper);

            var actionResult = await controller.Delete(fakeId);

            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.OK, (actionResult as OkResult).StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task Delete_FakeId_BadRequest(long fakeId)
        {
            //Arrange
            _testService.Setup(x => x.DeleteAsync(fakeId))
                .Throws<OutOfMemoryException>();

            //Act
            var controller = new MinsaitController(_loggerMock.Object, _testService.Object, _mapper);

            var actionResult = await controller.Delete(fakeId);

            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, (actionResult as BadRequestObjectResult).StatusCode);
        }


        private MinsaitTestViewModel GetByIdFakeViewModel(long fakeId)
        {
            return new MinsaitTestViewModel()
            {
                Id = fakeId
            };
        }

        private MinsaitTest GetByIdFakeModel(long fakeId)
        {
            return new MinsaitTest()
            {
                Id = fakeId
            };
        }

        private IQueryable<MinsaitTestViewModel> GetAllFakeViewModel()
        {
            var list = new List<MinsaitTestViewModel>
            {
                GetByIdFakeViewModel(1),
                GetByIdFakeViewModel(2),
                GetByIdFakeViewModel(3),
                GetByIdFakeViewModel(4),
                GetByIdFakeViewModel(5)
            };

            return list.AsQueryable();
        }

        private IQueryable<MinsaitTest> GetAllFakeModel()
        {
            var list = new List<MinsaitTest>
            {
                GetByIdFakeModel(1),
                GetByIdFakeModel(2),
                GetByIdFakeModel(3),
                GetByIdFakeModel(4),
                GetByIdFakeModel(5)
            };

            return list.AsQueryable();
        }
    }
}