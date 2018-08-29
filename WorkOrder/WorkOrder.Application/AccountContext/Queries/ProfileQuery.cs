using System;
using WorkOrder.Domain.AccountContext.Enums;

namespace WorkOrder.Application.AccountContext.Queries
{
    public class ProfileQuery
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => FirstName + " " + LastName;
        public UserRole Role { get; set; }
        public string Phone { get; set; }
    }
}
