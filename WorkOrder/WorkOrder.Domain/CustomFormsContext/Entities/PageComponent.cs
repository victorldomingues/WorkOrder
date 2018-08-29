using Flunt.Validations;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.CustomFormsContext.Entities
{
    public class PageComponent : Entity
    {
 
        protected PageComponent()
        {
            
        }

        public PageComponent(string name)
        {
            Name = name;

            new Contract()
                .Requires()
                .IsNullOrEmpty(Name, "Name", "O Nome do componente não foi informado.")
                .HasMaxLen(Name, 255, "Name","O Nome deve ter no máximo 255 caracteres." )
                ;
        }


        public string Name { get; private set; }

        public void SetName(string name)
        {

            if (string.IsNullOrEmpty(name))
                AddNotification("Name", "O Nome deve ser informado.");

            Name = name;

        }
    }
}
