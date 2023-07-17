namespace Minsait.Examples.Application.Commands.Base
{
    public interface ICommandHandler<in T, TDataResult> where T : CommandBase where TDataResult : CommandDataResult
    {
        Task<CommandResult<TDataResult>> Handle(T command);
    }
}
