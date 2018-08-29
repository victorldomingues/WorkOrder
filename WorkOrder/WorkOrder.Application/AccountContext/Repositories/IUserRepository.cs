using System;
using WorkOrder.Application.AccountContext.Queries;
using WorkOrder.Application.SharedContext.Repositories;
using WorkOrder.Domain.AccountContext.Entities;

namespace WorkOrder.Application.AccountContext.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string email);
        User Get(string email, Guid tenantId);
        ProfileQuery GetProfile(Guid userId);
    }
}
