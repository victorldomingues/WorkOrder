using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.EntityFrameworkCore;
using WorkOrder.Application.CustomFormsContext.Queries;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Infra.Contexts.SharedContext;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Infra.Contexts.CustomFormsContext.Repositories
{
    public class CustomFormRepository : Repository, ICustomFormRepository
    {
        public CustomFormRepository(SaasContext context, AppTenant tenant = null) : base(context, tenant)
        {
        }

        public void Save(CustomForm entity)
        {
            Context.CustomForms.Add(entity);
        }

        public void Update(CustomForm entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<CustomFormQuery> Query()
        {
            return Connection.Query<CustomFormQuery>(
                @"select 	[Id], [Name] from [CustomForms] where TenantId = @TenantId and EntityStatus = 1;", new
                {
                    TenantId = Tenant.Id
                });
        }

        public CustomFormQuery Query(Guid id)
        {
            var query = Connection.QueryMultiple(@"
                select 	[Id], [Name] from [CustomForms] where TenantId = @TenantId and EntityStatus = 1 and Id = @Id;
                select 	[Id], [Name], [Description], [Type], [Mandatory] from [CustomFields] where TenantId = @TenantId and EntityStatus = 1 and CustomFormId = @Id;
            ", new
            {
                Id = id,
                TenantId = Tenant.Id
            });
            var result = query.ReadFirst<CustomFormQuery>();
            result.Fields = query.Read<CustomFieldQuery>();
            foreach (var field in result.Fields)
            {
                if (CustomField.HasOptions(field.Type))
                    field.Options = Connection.Query<CustomFieldOptionQuery>(
                        @"select 	[Id], [Name] from [CustomFieldOptions] where  CustomFieldId = @Id and TenantId = @TenantId and EntityStatus = 1;"
                        , new
                        {
                            Id = field.Id,
                            TenantId = Tenant.Id
                        });
            }
            return result;
        }

    }
}
