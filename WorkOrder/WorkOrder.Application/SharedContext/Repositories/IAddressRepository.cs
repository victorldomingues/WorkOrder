using System;
using WorkOrder.Domain.SharedContext.Entities;

namespace WorkOrder.Application.SharedContext.Repositories
{
    public interface IAddressRepository
    {
        void Save(Address addres);
        Address Get(Guid contactAddressId);
        void Update(Address addres);
    }
}
