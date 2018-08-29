namespace WorkOrder.Shared.ValueObjects
{

    public class MessagesVo
    {
        public static string OrderCreated = "Ordem de serviço criada com sucesso";
    }

    public static class ErrorMessagesVo
    {
        public static string NameMandatory = "O nome não pode ser vazio!";

        public static string FirstNameMandatory = "O Primeiro nome é obrigatório.";
        public static string FirstNameMinLen = "O Primeiro nome deve conter no mínimo 3 caractere";
        public static string FirstNameMaxLen = "O Primeiro nome deve conter no máximo 125 caractere";

        public static string LastNameMandatory = "O Ultimo nome é obrigatório.";
        public static string LastNameMaxLen = "O Ultimo nome deve conter no máximo 125 caractere";

        public static string NameMinLen = "O nome deve conter no mínimo 3 caracteres!";
        public static string NameMaxLen = "O nome deve conter no máximo 255 caracteres!";
    }

}

