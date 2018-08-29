using Flunt.Validations;

namespace WorkOrder.Shared.ValidationContracts
{
    public partial class ValidationContract
    {
        public static Contract Site(string site)
        {
            return new Contract()
                .Requires()
                .IsNotNullOrEmpty(site, "Site", "o site não pode ser vazio!")
                .HasMinLen(site, 3, "Site", "O site deve conter no mínimo 3 caracteres!")
                .HasMaxLen(site, 255, "Site", "O site deve conter no máximo 255 caracteres!")
                ;
        }
    }
}
