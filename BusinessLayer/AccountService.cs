using DataAccessLayer;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{

    public class AccountService 
    {
        
        private readonly IRepository<Account> repo;

        public AccountService(IRepository<Account> customer)
        {
            repo = customer;
        }


        /// <summary>
        /// Get account details
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public Account GetAccount(int accountNumber)
        {
            return repo.GetById(accountNumber);
        }

        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAccounts()
        {
            try
            {
                return repo.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update account
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool UpdateAccount(Account account)
        {
            try
            {
                var accountList = repo.GetAll().Where(x => x.AccountId == account.AccountId).ToList();
                foreach (var item in accountList)
                {
                    item.FirstName = account.FirstName;
                    item.LastName = account.LastName;
                    repo.Update(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// Delete Account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public bool DeleteAccount(int accountId)
        {

            try
            {
                var accountList = repo.GetAll().Where(x => x.AccountId == accountId).ToList();
                foreach (var item in accountList)
                {
                    repo.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }

    }
}
