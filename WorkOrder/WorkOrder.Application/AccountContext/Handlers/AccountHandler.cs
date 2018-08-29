using System.Linq;
using Flunt.Notifications;
using WorkOrder.Application.AccountContext.Commands;
using WorkOrder.Application.AccountContext.Repositories;
using WorkOrder.Application.AccountContext.Services;
using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Domain.AccountContext.Enums;
using WorkOrder.Domain.SharedContext.ValueObjects;
using WorkOrder.Shared.Commands;
using WorkOrder.Shared.Commands.Interfaces;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Application.AccountContext.Handlers
{
    public class AccountHandler : Notifiable
        , ICommandHandler<CreateAccountCommand>
        , ICommandHandler<ResetPasswordCommand>
        , ICommandHandler<CreatePasswordResetRequestCommand>
    {
        private readonly ICreateAccountEmailService _createAccountEmailService;
        private readonly IUserRepository _repository;

        private readonly ITenantRepository _tenantRepository;

        public AccountHandler(ITenantRepository tenantRepository, IUserRepository repository,
            ICreateAccountEmailService createAccountEmailService)
        {
            _tenantRepository = tenantRepository;
            _repository = repository;
            _createAccountEmailService = createAccountEmailService;
        }

        public ICommandResult Handle(CreateAccountCommand command)
        {
            if (!command.IsValid())
            {
                return new CommandResult(false, "Erro ao criar conta, Comando invalido", command);
            }

            var hostname = command.Hostname.ToLower().Replace(" ", "");

#if DEBUG
            hostname = hostname.Split(":").Last();
#endif
            var tenant = _tenantRepository.Get(hostname);

            if (tenant == null)
                tenant = new AppTenant(command.CompanyName, hostname, null, null);
            else
                AddNotification("Hostname",
                    $"{command.FirstName}, Já existe uma empresa cadastrada com o nome: {command.Hostname}");

            AddNotifications(tenant.Notifications);

            if (Valid)
                _tenantRepository.Save(tenant);


            var user = _repository.Get(command.Email);


            if (user == null)
                user = new User(new NameVo(command.FirstName, command.LastName), new EmailVo(command.Email), UserRole.Owner, new PasswordVo(command.Password,
                    command.ConfirmPassword), new PhoneVo(command.Phone), tenant.Id);
            else
                AddNotification("Email",
                    $"{command.FirstName}, Já existe um usuário cadastrado com o e-mail {command.Email}");

            AddNotifications(user.Notifications);

            if (Valid) _repository.Save(user);


            if (Valid)
            {
                _createAccountEmailService.Send(user.Name.FirstName, user.Name.LastName, user.Email.Address, tenant.AppName,
                    tenant.Hostname);


                return new CommandResult(true,
                    $"{command.FirstName}, Sua conta criada com sucesso, enviamos um e-mail para {user.Email.Address}",
                    new
                    {
                        Name = $"{user.Name.FirstName} {user.Name.LastName}",
                        Email = user.Email.Address
                    });
            }

            return new CommandResult(false, "Erro ao criar conta", Notifications);
        }

        public ICommandResult Handle(CreatePasswordResetRequestCommand command)
        {
            //if (command == null)
            //    return new CommandResult()
            //    {
            //        Success = false,
            //        Data = Notifications,
            //        Message = "Erro ao requisitar redefinição de senha. Comando inválido!"
            //    };

            //var user = _repository.Get(command.Email);

            //PasswordResetRequest request = null;

            //if (user == null)
            //    AddNotification("Email", $"O Usuário com o e-mail: {command.Email} não foi econtrado!");
            //else
            //{
            //    request = new PasswordResetRequest(user.Id);

            //    if (Valid)
            //        _repository.Save(request);
            //}
            //if (Valid)
            //{

            //    _resetPasswordEmailService.Send(user.Name, user.Email, request.Code, request.Token);

            //    return new CommandResult(true,
            //        $"Um e-mail foi enviado para {command.Email}! com um código de confirmação para resetar a sua senha"
            //        , new { Email = command.Email });
            //}
            //else
            //    return new CommandResult(false, "Erro ao gerar sua requisição", Notifications);

            //todo: implementar
            return null;
        }

        public ICommandResult Handle(ResetPasswordCommand command)
        {
            //if (command == null || !command.IsValid())
            //    return new CommandResult()
            //    {
            //        Success = false,
            //        Data = Notifications,
            //        Message = "Erro ao cadastrar usuário. Comando inválido!"
            //    };

            //var request = _repository.GetPasswordResetRequest(command.Token, command.Code);

            //if (request == null || !request.Token.ToLower().Equals(command.Token.Trim().ToLower()) || !request.Code.ToLower().Equals(command.Code.Trim().ToLower()))
            //    AddNotification("Token", "O seu  Código expirou ou é invalido");
            //else
            //{

            //    var user = _repository.Get(request.UserId);

            //    if (user == null)
            //        AddNotification("Email", $"O Usuári não foi econtrado!");
            //    else
            //    {
            //        if (!command.Password.Equals(command.ConfirmPassword))
            //            AddNotification("Password", "Erro ao redefinir senha as senhas não coincidem");
            //        else
            //        {
            //            user.SetPassword(command.Password, command.ConfirmPassword);
            //            _repository.Update(user);
            //        }
            //        AddNotifications(user.Notifications);
            //    }


            //    if (Valid)
            //        request.Use();
            //}

            //if (Valid)
            //    return new CommandResult(true, "Senha redefinida com sucesso!", new { });
            //else
            //    return new CommandResult(false, "Erro ao redefinir senha", Notifications);

            //todo: implementar
            return null;
        }
    }
}
