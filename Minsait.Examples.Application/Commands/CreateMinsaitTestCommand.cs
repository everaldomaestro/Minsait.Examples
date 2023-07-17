using Minsait.Examples.Application.Commands.Base;

namespace Minsait.Examples.Application.Commands
{
    public class CreateMinsaitTestCommand : CommandBase
    {
        public string Name { get; set; }
        public DateTime Creation { get; set; }
        public decimal Value { get; set; }
        public bool Active { get; set; }
    }
}
