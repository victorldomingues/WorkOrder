using WorkOrder.Shared.Commands.Interfaces;

namespace WorkOrder.Application.AccountContext.Commands
{
    public class ResetPasswordCommand : ICommand
    {
        public string Token { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Token))
                return false;

            if (string.IsNullOrEmpty(Code))
                return false;

            if (string.IsNullOrEmpty(Password))
                return false;

            if (string.IsNullOrEmpty(ConfirmPassword))
                return false;

            return true;
        }
    }
}
