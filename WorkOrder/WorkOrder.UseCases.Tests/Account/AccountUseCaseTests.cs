using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WorkOrder.Application.AccountContext.Commands;
using WorkOrder.Application.AccountContext.Handlers;
using WorkOrder.Application.AccountContext.Repositories;
using WorkOrder.Application.AccountContext.Services;
using WorkOrder.Domain.AccountContext.Entities;
using WorkOrder.Shared;
using WorkOrder.Shared.Entities;

namespace WorkOrder.UseCases.Tests.Account
{
    [TestClass]
    public class AccountUseCaseTests
    {

        private ITenantRepository _tenantRepository;

        private IUserRepository _userRepository;

        private ICreateAccountEmailService _createAccountEmailService;

        private Mock<ITenantRepository> _mockTenantRepository;

        private Mock<IUserRepository> _mockUserRepository;

        private Mock<ICreateAccountEmailService> _mockCreateAccountEmailService;

        private List<AppTenant> _tenants;

        private List<User> _users;

        [TestInitialize]
        public void SetUp()
        {

            Settings.SecurityKey = "cbffeba849124af8b7b89675c223fd3d";

            _tenants = new List<AppTenant>();
            _users = new List<User>();
            _mockTenantRepository = new Mock<ITenantRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockCreateAccountEmailService = new Mock<ICreateAccountEmailService>();

            _mockTenantRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns((string s) => _tenants.SingleOrDefault(x => x.AppName == s));

            _mockTenantRepository.Setup(mr => mr.Save(It.IsAny<AppTenant>())).Callback((AppTenant target) => { _tenants.Add(target); });

            _mockUserRepository.Setup(mr => mr.Get(It.IsAny<string>())).Returns((string s) => _users.SingleOrDefault(x => x.Email.Address == s));

            _mockUserRepository.Setup(mr => mr.Save(It.IsAny<User>())).Callback((User target) => { _users.Add(target); });

            _mockCreateAccountEmailService.Setup(mr => mr.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Callback(() => { });


            this._tenantRepository = _mockTenantRepository.Object;
            this._userRepository = _mockUserRepository.Object;
            this._createAccountEmailService = _mockCreateAccountEmailService.Object;

        }

        [TestMethod]
        public void Deve_Criar_Uma_Nova_Conta()
        {

            var command = new CreateAccountCommand
            {
                Hostname = "localhost:43500",
                CompanyName = "TreezeBit",
                Password = "12345678",
                ConfirmPassword = "12345678",
                Email = "victor.luiz@treezebit.com",
                FirstName = "Victor",
                LastName = "Domingues"
            };

            var accountHandler = new AccountHandler(_tenantRepository, _userRepository, _createAccountEmailService);

            var result = accountHandler.Handle(command);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(accountHandler.Notifications.Count, 0);
            Assert.AreEqual(_tenants.Count, 1);
            Assert.AreEqual(_users.Count, 1);

        }

    }
}
