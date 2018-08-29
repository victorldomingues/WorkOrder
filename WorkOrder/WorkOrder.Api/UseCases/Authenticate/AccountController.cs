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

namespace WorkOrder.Api.UseCases.Authenticate
{
    public class AccountController : BaseController
    {

        private readonly AccountHandler _accountHandler;
        private readonly IUserRepository _repository;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly TokenOptions _tokenOptions;
        private User _user;

        public AccountController(IUnitOfWork uow, AccountHandler accountHandler, IUserRepository repository, IOptions<TokenOptions> jwtOptions) : base(uow)
        {
            _accountHandler = accountHandler;
            _repository = repository;
            _tokenOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_tokenOptions);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        /// <summary>
        /// UC : Autenticar um usuário
        /// </summary>
        /// <param name="command">AuthenticateUserCommand - Faz a autenticação do usuário</param>
        /// <returns>Token</returns>
        [HttpPost("v1/account/signin"), AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AuthenticateUserCommand command)
        {
            if (command == null)
                return await Response(null,
                    new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var identity = await GetClaims(command);
            if (identity == null)
                return await Response(null,
                    new List<Notification> { new Notification("User", "Usuário ou senha inválidos") });

            var seconds = ToUnixEpochDate(_tokenOptions.IssuedAt);

            if (command.KeepLoggedIn.GetValueOrDefault())
                seconds *= 256;

            var identityE = identity.FindFirst("Saas");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, _user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, _user.Name.FirstName),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email.Address),
                new Claim(JwtRegisteredClaimNames.Sub, _user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, seconds.ToString(), ClaimValueTypes.Integer64),
                identityE
            };

            var jwt = new JwtSecurityToken(
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                claims.AsEnumerable(),
                _tokenOptions.NotBefore,
                _tokenOptions.Expiration,
                _tokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                token = encodedJwt,
                expires = (int)_tokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = _user.Id,
                    name = _user.ToString(),
                    email = _user.Email.Address
                }
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }


        /// <summary>
        /// UC : Deslogar usuário logado
        /// </summary>
        /// <returns></returns>
        [HttpDelete("v1/account/logout"), AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            return Ok(new
            {
                Success = true,
                Message = "Deslogado"
            });
        }



        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("O período deve ser maior que zero", nameof(TokenOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(TokenOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        }

        private Task<ClaimsIdentity> GetClaims(AuthenticateUserCommand command)
        {
            var user = _repository.Get(command.Email);

            if (user == null)
                return Task.FromResult<ClaimsIdentity>(null);

            if (!user.Authenticate(command.Email, command.Password))
                return Task.FromResult<ClaimsIdentity>(null);

            _user = user;

            string policy;

            switch (_user.Role)
            {
                case UserRole.Owner:
                    policy = "Owner";
                    break;
                case UserRole.Manager:
                    policy = "Manager";
                    break;
                case UserRole.User:
                    policy = "User";
                    break;
                default:
                    policy = "User";
                    break;
            }

            var claims = new[]
            {
                new Claim("Saas", policy)
            };


            return Task.FromResult(new ClaimsIdentity(
                    new GenericIdentity(user.Email.Address, "Token"),
                    claims))
                ;
        }
    }
}
