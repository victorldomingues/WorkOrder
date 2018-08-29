using Flunt.Validations;

namespace WorkOrder.Shared.ValidationContracts
{
    public partial class ValidationContract
    {

        public static Contract Phone(string number)
        {
            return new Contract().Requires().IsNotNullOrEmpty(number, "Phone", "O Numéro de telefone é obrigatório!");
        }

    }
}
