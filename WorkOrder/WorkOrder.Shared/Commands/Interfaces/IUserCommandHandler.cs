using System;

namespace WorkOrder.Shared.Commands.Interfaces
{
    public interface IUserCommandHandler<in T> where T : ICommand
    {
        /// <summary>
        ///     Manipulador de comando para usuário logado
        /// </summary>
        /// <param name="userId">Id do usuário logado</param>
        /// <param name="command">Comando : ICommand </param>
        /// <returns></returns>
        ICommandResult Handle(Guid userId, T command);
    }
}
