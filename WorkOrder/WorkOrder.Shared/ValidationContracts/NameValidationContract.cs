using Flunt.Validations;
using WorkOrder.Shared.ValueObjects;

namespace WorkOrder.Shared.ValidationContracts
{
    public partial class ValidationContract
    {
        public static Contract Name(string name)
        {
            return new Contract()
                .Requires()
                .IsNotNullOrEmpty(name, "Name", ErrorMessagesVo.NameMandatory)
                .HasMinLen(name, 3, "Name", ErrorMessagesVo.NameMinLen)
                .HasMaxLen(name, 255, "Name", ErrorMessagesVo.NameMaxLen)
                ;
        }
        public static Contract FirstNameLastName(string firstName, string lastName)
        {
            return new Contract()
                .Requires()
                .IsNotNullOrEmpty(firstName, "FirstName", ErrorMessagesVo.FirstNameMandatory)
                .HasMinLen(firstName, 3, "FirstName", ErrorMessagesVo.FirstNameMinLen)
                .HasMaxLen(firstName, 125, "FirstName", ErrorMessagesVo.FirstNameMaxLen).HasMinLen(firstName, 3, "FirstName", ErrorMessagesVo.FirstNameMinLen)
                .IsNotNullOrEmpty(lastName, "LastName", ErrorMessagesVo.LastNameMandatory)
                .HasMaxLen(lastName, 125, "LastName", ErrorMessagesVo.LastNameMaxLen).HasMinLen(firstName, 3, "FirstName", ErrorMessagesVo.FirstNameMinLen)
            ;
        }
    }
}
