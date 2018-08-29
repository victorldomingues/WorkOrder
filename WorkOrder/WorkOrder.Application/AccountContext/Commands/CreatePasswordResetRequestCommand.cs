using WorkOrder.Shared.Commands.Interfaces;

namespace WorkOrder.Application.AccountContext.Commands
{
    public class CreatePasswordResetRequestCommand : ICommand
    {
        public string Email { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Email);
        }
    }
}
