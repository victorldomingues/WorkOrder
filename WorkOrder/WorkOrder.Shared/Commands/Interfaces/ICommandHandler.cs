namespace WorkOrder.Shared.Commands.Interfaces
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
