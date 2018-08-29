using WorkOrder.Shared.Commands.Interfaces;

namespace WorkOrder.Application.AccountContext.Commands
{
    public class CreateAccountCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string Hostname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool IsValid()
        {
            //if (string.IsNullOrEmpty(FirstName))
            //    return false;
            //if (string.IsNullOrEmpty(LastName))
            //    return false;
            //if (string.IsNullOrEmpty(Email))
            //    return false;
            //if (string.IsNullOrEmpty(CompanyName))
            //    return false;
            //if (string.IsNullOrEmpty(Password))
            //    return false;
            //if (string.IsNullOrEmpty(ConfirmPassword))
            //    return false;

            return true;
        }
    }
}
