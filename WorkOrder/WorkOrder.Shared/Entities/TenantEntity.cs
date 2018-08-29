using System;

namespace WorkOrder.Shared.Entities
{
    public abstract class TenantEntity : Entity
    {
        /// <summary>
        /// TenantId Privado para setar, colocar no construtor da classe
        /// </summary>
        public Guid TenantId { get; private set; }

        public AppTenant Tenant { get; private set; }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void SetTenantId(Guid tenantId)
        {
            TenantId = tenantId;
        }
    }
}
