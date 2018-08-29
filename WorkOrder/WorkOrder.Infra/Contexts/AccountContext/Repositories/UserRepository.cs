using System;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using WorkOrder.Application.AccountContext.Queries;
using WorkOrder.Application.AccountContext.Repositories;
using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Infra.Contexts.SharedContext;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.AccountContext.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(SaasContext context, AppTenant tenant) : base(context, tenant)
        {
        }

        public User Get(string commandEmail)
        {
            return Context.Users.FirstOrDefault(x => x.Email.Address == commandEmail);
        }

        public User Get(string commandEmail, Guid tenantId)
        {
            return Context.Users.FirstOrDefault(x => x.Email.Address == commandEmail);
        }

        public ProfileQuery GetProfile(Guid userId)
        {
            return Connection.QueryFirst<ProfileQuery>(
                "select Id, FirstName, LastName, Email, Role, Phone from Users where Id  = @Id and TenantId  = @TenantId",
                new
                {
                    Id = userId,
                    TenantId = Tenant.Id
                });
        }


        public void Save(User entity)
        {
            Context.Users.Add(entity);
        }

        public void Update(User entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        //public void Delete(Guid? id = null, User entity = null)
        //{
        //    if(id != null)
        //    {
        //        entity = Context.Users.FirstOrDefault(x => x.Id == id);
        //    }
        //    Context.Entry<User>(entity).State = EntityState.Modified;
        //}
    }
}
