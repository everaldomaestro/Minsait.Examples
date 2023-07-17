using Minsait.Examples.Application.Services;

namespace Minsait.Examples.ApplicationTests
{
    public class IsLeapYearTest
    {
        private readonly Mock<IMinsaitTestRepository> _repository;
        private readonly MinsaitTestService _service;

        public IsLeapYearTest()
        {
            _repository = new Mock<IMinsaitTestRepository>();
            _service = new MinsaitTestService(_repository.Object);
        }

        [Fact]
        public void IsLeapYear_1996_IsTrue()
        {
            var year = 1996;
            var result = _service.IsLeapYear(year);

            Assert.True(result);
        }

        [Fact]
        public void IsLeapYear_1997_IsFalse()
        {
            var year = 1997;
            var result = _service.IsLeapYear(year);

            Assert.False(result);
        }

        [Theory]
        [InlineData(1996)]
        [InlineData(2000)]
        [InlineData(2004)]
        public void IsLeapYear_IsTrue(int year)
        {
            var result = _service.IsLeapYear(year);

            Assert.True(result);
        }

        [Theory]
        [InlineData(1997)]
        [InlineData(2023)]
        [InlineData(2022)]
        [InlineData(2014)]
        public void IsLeapYear_IsFalse(int year)
        {
            var result = _service.IsLeapYear(year);

            Assert.False(result);
        }
    }
}
