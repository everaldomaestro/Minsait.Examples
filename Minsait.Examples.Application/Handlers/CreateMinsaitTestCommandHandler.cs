using FluentValidation;
using Minsait.Examples.Application.Commands;
using Minsait.Examples.Application.Commands.Base;
using Minsait.Examples.Application.Responses;
using Minsait.Examples.Domain.Entities;
using Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories;

namespace Minsait.Examples.Application.Handlers
{
    public class CreateMinsaitTestCommandHandler : CommandHandlerBase<CreateMinsaitTestResult>, ICommandHandler<CreateMinsaitTestCommand, CreateMinsaitTestResult>
    {
        private readonly IValidator<CreateMinsaitTestCommand> _createCommandValidator;
        private readonly IMinsaitTestRepository _repository;

        public CreateMinsaitTestCommandHandler(IValidator<CreateMinsaitTestCommand> validator, IMinsaitTestRepository repository)
        {
            _createCommandValidator = validator;
            _repository = repository;
        }

        public async Task<CommandResult<CreateMinsaitTestResult>> Handle(CreateMinsaitTestCommand command)
        {
            var validationResult = Validate(command, _createCommandValidator);

            if (validationResult.IsValid)
            {
                var entry = new MinsaitTest
                {
                    Name = command.Name,
                    Creation = command.Creation,
                    Value = command.Value,
                    Active = command.Active
                };

                await _repository.AddAsync(entry);
                await _repository.SaveChangesAsync();

                var result = new CreateMinsaitTestResult
                {
                    Id = entry.Id,
                    Name = command.Name,
                    Creation = entry.Creation,
                    Value = entry.Value,
                    Active = entry.Active
                };

                return Return(result);
            }

            return Return();
        }
    }
}
