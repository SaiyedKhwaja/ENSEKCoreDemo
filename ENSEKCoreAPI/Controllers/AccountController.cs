using BusinessLayer;
using DataAccessLayer;
using DataAccessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ENSEKCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly MeterReadingService _meterReadingService;
        private readonly IRepository<Account> _account;

        public AccountController(IRepository<Account> Account, AccountService Accountservice, MeterReadingService MeterReadingservice)
        {
            _accountService = Accountservice;
            _meterReadingService = MeterReadingservice;
            _account = Account;

        }
        [HttpGet("GetAccounts")]
        public List<Account> GetAccounts()
        {

            return _accountService.GetAccounts().ToList();

        }
        [HttpGet("GetAccountById")]
        public Account GetAccountById(int accountId)
        {

            return _accountService.GetAccounts().FirstOrDefault(x => x.AccountId == accountId);

        }

        [HttpPut("UpdateAccount")]
        public bool UpdateAccount(Account Object)
        {
            try
            {
                _accountService.UpdateAccount(Object);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpDelete("DeleteAccount")]
        public bool DeleteAccount(int accountId)
        {
            try
            {
                _accountService.DeleteAccount(accountId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
