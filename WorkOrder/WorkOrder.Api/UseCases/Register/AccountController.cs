using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WorkOrder.Api.Controllers;
using WorkOrder.Api.Security;
using WorkOrder.Application.AccountContext.Commands;
using WorkOrder.Application.AccountContext.Handlers;
using WorkOrder.Application.AccountContext.Repositories;
using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Domain.AccountContext.Enums;
using WorkOrder.Infra.Transactions;

namespace WorkOrder.Api.UseCases.Register
{
    public class AccountController : BaseController
    {
        private readonly AccountHandler _accountHandler;

        private readonly IUserRepository _repository;
        public TokenOptions TokenOptions { get; }
        private User _user;

        /// <summary>
        /// Construtor do controller
        /// </summary>
        /// <param name="uow"> IUnitOfWork - Injeção de Dependencia </param>
        /// <param name="accountHandler">AccountHandler - Injeção de Dependencia</param>
        /// <param name="repository">IUserRepository - Injeção de Dependencia</param>
        public AccountController(IUnitOfWork uow, AccountHandler accountHandler, IUserRepository repository) : base(uow)
        {
            _accountHandler = accountHandler;
            _repository = repository;
        }

        /// <summary>
        /// UC : Criar um novo usuário
        /// </summary>
        /// <param name="command">CreateUserCommand - Cria um novo usuário</param>
        /// <returns>ICommandResult - Resultado do Comando</returns>
        [HttpPost("v1/account/signup"), AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] CreateAccountCommand command)
        {
            var result = _accountHandler.Handle(command);
            return await Response(result);
            //var actionResult = await Response(result);
            //if (!result.Success)
            //return await Response(result);
            //return await SignIn(new AuthenticateUserCommand() { Email = command.Email, Password = command.Password, KeepLoggedIn = false });
        }

        /// <summary>
        /// UC : Recupera usuário logado
        /// </summary>
        /// <returns></returns>
        [HttpGet("v1/account")]
        public async Task<IActionResult> Profile()
        {
            return await Result(_repository.GetProfile(LoggedUser));
        }

        /// <summary>
        ///     Requisitar código para redefinição de senha
        /// </summary>
        /// <param name="command">CreatePasswordResetRequestCommand - realiza requisição de novo código para redefinição de senha</param>
        /// <returns>Token</returns>
        [HttpPost("v1/account/reset-password"), AllowAnonymous]
        public async Task<IActionResult> Remember([FromBody] CreatePasswordResetRequestCommand command)
        {
            return await Response(_accountHandler.Handle(command));
        }

        /// <summary>
        /// UC : Redefinir a senha do usuário
        /// </summary>
        /// <param name="command">CreatePasswordResetRequestCommand - realiza requisição de novo código para redefinição de senha</param>
        /// <returns>Token</returns>
        [HttpPut("v1/account/change-password"), AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ResetPasswordCommand command)
        {
            return await Response(_accountHandler.Handle(command));
        }



    }
}
