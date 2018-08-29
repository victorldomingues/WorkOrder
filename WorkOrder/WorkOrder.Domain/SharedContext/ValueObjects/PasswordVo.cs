using System;
using Flunt.Notifications;
using Flunt.Validations;
using WorkOrder.Shared.Extensions;

namespace WorkOrder.Domain.SharedContext.ValueObjects
{
    public class PasswordVo : Notifiable, IValidatable
    {

        private string _password;
        private string _confirmPassword;

        protected PasswordVo()
        {

        }

        public PasswordVo(string password, string confirmPassword)
        {
            _password = password.Trim();
            _confirmPassword = confirmPassword.Trim();

            ConfirmPassword = _confirmPassword;
            Password = _password.Encrypt();

            Validate();
        }

        public void SetPassword(string password, string confirmPassword)
        {
            _password = password.Trim();
            _confirmPassword = confirmPassword.Trim();

            ConfirmPassword = _confirmPassword;
            Password = _password.Encrypt();
        }


        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }

        public void Validate()
        {
            AddNotifications(new Contract().Requires()
                .IsNotNullOrEmpty(_password, "Password", "A senha é obrigatório.")
                .AreEquals(_password, _confirmPassword, "Password", "As senhas não coincidem.")
                .HasMaxLen(_password, 30, "Password", "A senha teve conter no máximo 30 caracteres")
                .HasMinLen(_password, 8, "Password", "A senha deve conter no mínimo 8 caracteres")
                );

        }
    }
}
