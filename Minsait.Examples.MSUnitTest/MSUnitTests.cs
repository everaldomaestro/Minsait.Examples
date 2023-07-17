using Minsait.Examples.Application.Services;
using Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories;
using Moq;

namespace Minsait.Examples.MSUnitTest
{
    [TestClass]
    public class MSUnitTests
    {
        private readonly Mock<IMinsaitTestRepository> _repository;
        private readonly MinsaitTestService _service;

        public MSUnitTests()
        {
            _repository = new Mock<IMinsaitTestRepository>();
            _service = new MinsaitTestService(_repository.Object);
        }

        [TestMethod]
        public void IsLeapYear_1996_IsTrue()
        {
            var year = 1996;
            var result = _service.IsLeapYear(year);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLeapYear_1997_IsFalse()
        {
            var year = 1997;
            var result = _service.IsLeapYear(year);

            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow(1996)]
        [DataRow(2000)]
        [DataRow(2004)]
        public void IsLeapYear_IsTrue(int year)
        {
            var result = _service.IsLeapYear(year);

            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow(1997)]
        [DataRow(2023)]
        [DataRow(2022)]
        [DataRow(2014)]
        public void IsLeapYear_IsFalse(int year)
        {
            var result = _service.IsLeapYear(year);

            Assert.IsFalse(result);
        }
    }
}