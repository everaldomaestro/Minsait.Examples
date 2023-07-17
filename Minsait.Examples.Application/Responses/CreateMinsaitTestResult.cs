using Minsait.Examples.Application.Commands.Base;

namespace Minsait.Examples.Application.Responses
{
    public class CreateMinsaitTestResult : CommandDataResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Creation { get; set; }
        public decimal Value { get; set; }
        public bool Active { get; set; }
    }
}
