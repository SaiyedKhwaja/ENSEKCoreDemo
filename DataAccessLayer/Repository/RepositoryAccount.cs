using DataAccessLayer.Data;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class RepositoryAccount : IRepository<Account>
    {
        ApplicationDbContext _dbContext;
        public RepositoryAccount(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Account> Create(Account _object)
        {
            var obj = await _dbContext.Account.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Account _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Account> GetAll()
        {
            return _dbContext.Account.ToList();
        }

        public Account GetById(int Id)
        {
            return _dbContext.Account.Where(x => x.AccountId == Id).FirstOrDefault();
        }

        public void Update(Account _object)
        {
            _dbContext.Account.Update(_object);
            _dbContext.SaveChanges();
        }

        public Account GetAccount(int accountNumber)
        {
            return _dbContext
                .Account
                .FirstOrDefault(a => a.AccountId == accountNumber);
        }

       
    }
}
