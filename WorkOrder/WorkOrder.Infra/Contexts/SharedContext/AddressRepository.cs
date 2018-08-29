using System;
using System.Linq;
using WorkOrder.Application.SharedContext.Repositories;
using WorkOrder.Domain.SharedContext.Entities;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.SharedContext
{
    public class AddressRepository : Repository, IAddressRepository
    {
        public AddressRepository(SaasContext context, AppTenant tenant = null) : base(context, tenant)
        {
        }


        public void Save(Address addres)
        {
            addres.SetTenantId(Tenant.Id);
            Context.Address.Add(addres);
        }

        public Address Get(Guid contactAddressId)
        {
            return Context.Address.FirstOrDefault(x => x.Id == contactAddressId);
        }

        public void Update(Address address)
        {
            var databaseAddress = Context.Address.Single(x => x.Id == address.Id);
            databaseAddress.SetUpdatedAt();
        }

    }
}
