using BusinessLayer;
using DataAccessLayer;
using DataAccessLayer.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestENSEKCore.Tests.Controllers
{
    public class AccountControllerTests
    {
        private Mock<IRepository<Account>> accountRepository;
        private List<Account> _accounts;
        [SetUp]
        public void Setup()
        {
            accountRepository = new Mock<IRepository<Account>>();
            _accounts = new List<Account>();
            _accounts.Add(new Account { AccountId = 1234, FirstName = "Khwaja", LastName = "Saiyed" });
            _accounts.Add(new Account { AccountId = 2345, FirstName = "Firdosali", LastName = "Saiyed" });
            _accounts.Add(new Account { AccountId = 3456, FirstName = "Babar", LastName = "Saiyed" });
            _accounts.Add(new Account { AccountId = 2344, FirstName = "Ali", LastName = "Saiyed" });
        }
        

        [Test]
        public void GetAllAccounts_HappyPath_GetAllAccounts()
        {
            //Act
            accountRepository.Setup(x => x.GetAll()).Returns(_accounts.AsQueryable());

            //Arrange
            var accountBL = new AccountService(accountRepository.Object);
            var accountList = accountBL.GetAccounts();

            //Assert
            Assert.IsNotNull(accountList);
            Assert.IsTrue(accountList.Count == 4);

        }

        [Test]
        public void GetAccount_ReturnsAccount()
        {
            //Act
            accountRepository.Setup(x => x.GetById(2344));

            //Arrange
            var accountBL = new AccountService(accountRepository.Object);
            var accountList = accountBL.GetAccount(2344);

            //Assert
            Assert.IsNotNull(accountList);
            Assert.AreSame(accountList, accountBL);


        }
    }
}
