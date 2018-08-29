using System;
using Flunt.Validations;
using WorkOrder.Domain.AccountContext.Enums;
using WorkOrder.Domain.SharedContext.ValueObjects;
using WorkOrder.Shared.Entities;
using WorkOrder.Shared.Extensions;

namespace WorkOrder.Domain.AccountContext.Entities
{
    public class User : TenantEntity, IValidatable
    {

        protected User()
        {
        }

        public User(NameVo name, EmailVo email, UserRole role, PasswordVo password, PhoneVo phone, Guid tenantId)
        {
            Name = name;
            Email = email;
            Role = role;
            Phone = phone;
            Password = password;
            SetTenantId(tenantId);
            Validate();
        }

        public NameVo Name { get; set; }
        public EmailVo Email { get; private set; }
        public UserRole Role { get; private set; }
        public PasswordVo Password { get; private set; }
        public PhoneVo Phone { get; private set; }

        public void Update(NameVo name, EmailVo email, PhoneVo phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
            SetUpdatedAt();
        }

        public void SetPassword(PasswordVo password)
        {
            Password = password;
            SetUpdatedAt();
        }

        public void SetRole(UserRole role)
        {
            Role = role;
            SetUpdatedAt();
        }

        public override string ToString()
        {
            return $"{Name.FirstName} {Name.LastName}";
        }

        public bool Authenticate(string email, string password)
        {
            var p = password.Trim().Encrypt();

            if (Email.Address == email && Password.Password.Trim() == p)
                return true;

            AddNotification("User", "Usuário ou senha inválidos");

            return false;
        }

        public void Validate()
        {
            AddNotifications(new Contract().Requires());
            AddNotifications(Name.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(Password.Notifications);
        }
    }
}
