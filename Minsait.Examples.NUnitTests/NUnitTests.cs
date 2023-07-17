using Minsait.Examples.Application.Services;
using Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories;
using Moq;

namespace Minsait.Examples.NUnitTests
{
    public class NUnitTests
    {
        private Mock<IMinsaitTestRepository> _repository;
        private MinsaitTestService _service;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IMinsaitTestRepository>();
            _service = new MinsaitTestService(_repository.Object);
        }

        [Test]
        public void IsLeapYear_1996_IsTrue()
        {
            var year = 1996;
            var result = _service.IsLeapYear(year);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsLeapYear_1997_IsFalse()
        {
            var year = 1997;
            var result = _service.IsLeapYear(year);

            Assert.False(result);
        }
    }
}