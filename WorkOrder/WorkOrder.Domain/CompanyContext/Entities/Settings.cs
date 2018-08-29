using System;
using Flunt.Validations;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.CompanyContext.Entities
{
    public class Settings : TenantEntity, IValidatable
    {

        protected Settings()
        {

        }

        public Settings(Guid tenantId, Guid companyId)
        {

            SetTenantId(tenantId);
            CompanyId = companyId;
            Logo = "";
            WarrantyTerm = "";
            EnableWarrantyTerm = false;
            DisplayTotals = false;
        }

        
        public string Logo { get; private set; }
        public string WarrantyTerm { get; private set; }
        public bool EnableWarrantyTerm { get; private set; }
        public bool DisplayTotals { get; private set; }
        public Guid CompanyId { get; private set; }
        public Company Company { get; private set; }

        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}
