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
    public class MeterReadingController : ControllerBase
    {

        private readonly AccountService _accountService;
        private readonly MeterReadingService _meterReadingService;
        private readonly IRepository<Account> _account;


        public MeterReadingController(IRepository<Account> Account, AccountService Accountservice, MeterReadingService MeterReadingservice)
        {
            _accountService = Accountservice;
            _meterReadingService = MeterReadingservice;
            _account = Account;

        }
       
        [HttpPost("meter-reading-uploads")]
        public async Task<IActionResult> MeterReadingUploads([FromBody] List<MeterReading> readings)
        {
            List<MeterReading> meterReadings = new List<MeterReading>();
            foreach (var item in readings)
            {
               
                var reading = await _meterReadingService.MeterReadingUpload(item);

                reading.MeterReadingStringValue = item.MeterReadingStringValue;
                reading.MeterReadingValue = item.MeterReadingValue;
                meterReadings.Add(reading);
            }


            return Ok(meterReadings);

        }

        [HttpGet("GetMeterReadings")]
        public List<MeterReading> GetMeterReadings()
        {

            return _meterReadingService.GetMeterReadings().ToList();

        }

        [HttpGet("GetMeterReadingById")]
        public MeterReading GetMeterReadingById(int meterReadingId)
        {

            return _meterReadingService.GetMeterReadings().FirstOrDefault(x => x.Id == meterReadingId);

        }

        [HttpPut("UpdateMeterReading")]
        public bool UpdateMeterReading(MeterReading Object)
        {
            try
            {
                _meterReadingService.UpdateMeterReading(Object);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpDelete("DeleteMeterReading")]
        public bool DeleteMeterReading(int Id)
        {
            try
            {
                _meterReadingService.DeleteMeterReading(Id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
