using DataAccessLayer.Data;
using DataAccessLayer.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class RepositoryMeterReading : IRepository<MeterReading>
    {
        ApplicationDbContext _dbContext;
        public RepositoryMeterReading(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<MeterReading> Create(MeterReading _object)
        {
            var obj = await _dbContext.MeterReading.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(MeterReading _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

       
        public IEnumerable<MeterReading> GetAll()
        {
            return _dbContext.MeterReading.ToList();
        }

        public MeterReading GetById(int Id)
        {
            return _dbContext.MeterReading.Where(x => x.Id == Id).FirstOrDefault();
        }

        public void Update(MeterReading _object)
        {
            _dbContext.MeterReading.Update(_object);
            _dbContext.SaveChanges();
        }
       
    }
}
