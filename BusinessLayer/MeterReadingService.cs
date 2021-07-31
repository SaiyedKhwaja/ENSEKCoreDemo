using DataAccessLayer;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer
{

    public class MeterReadingService 
    {

        private readonly IRepository<MeterReading> repoMeterReading;
        private readonly IRepository<Account> repoAccount;

        public MeterReadingService(IRepository<MeterReading> meterReading)
        {
            repoMeterReading = meterReading;
        }
        public MeterReadingService(IRepository<Account> account,IRepository<MeterReading> meterReading)
        {
            repoMeterReading = meterReading;
            repoAccount = account;
        }
        /// <summary>
        /// Get all meter reading
        /// </summary>
        /// <returns></returns>
        public List<MeterReading> GetMeterReadings()
        {
            return repoMeterReading.GetAll().ToList();
        }

        public List<MeterReading> GetAllMeterReadings(int AccountId)
        {
            return repoMeterReading.GetAll().Where(x => x.AccountId == AccountId).ToList();
        }
        public bool UpdateMeterReading(MeterReading meterReading)
        {
            try
            {
               
                var accountList = repoMeterReading.GetAll().Where(x => x.Id == meterReading.Id).ToList();
                foreach (var item in accountList)
                {
                    item.MeterReadingDateTime = meterReading.MeterReadingDateTime;
                    item.MeterReadingValue = meterReading.MeterReadingValue;
                    repoMeterReading.Update(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
        public bool DeleteMeterReading(int meterReadingId)
        {

            try
            {
                var accountList = repoMeterReading.GetAll().Where(x => x.Id == meterReadingId).ToList();
                foreach (var item in accountList)
                {
                    repoMeterReading.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }


        public MeterReading GetMeterReading(int accountNumber)
        {
            return repoMeterReading.GetById(accountNumber);
        }

        /// <summary>
        /// Upload meter readings
        /// </summary>
        /// <param name="meterReading"></param>
        /// <returns></returns>
        public async Task<MeterReading> MeterReadingUpload(MeterReading meterReading)
        {
            var meterReadingValue =meterReading;
            List<string> _messages = new List<string>();
            if (IsValid(meterReading))
            {
                
                _messages.Add("Meter reading has been uploaded successfully");
                meterReading.MeterReadingValue = int.Parse(meterReading.MeterReadingStringValue);
                meterReadingValue = await repoMeterReading.Create(meterReading);
                meterReadingValue.IsSuccess = true;
                meterReadingValue.Messages = _messages;
            }
            else 
            {
                _messages = GetFailureMessages(meterReading);
                meterReadingValue .IsSuccess = false;
                meterReadingValue.Messages = _messages;
            }
            return meterReadingValue;
        }
    
        /// <summary>
        /// Get failure messages
        /// </summary>
        /// <param name="readingValue"></param>
        /// <returns></returns>
        private List<string> GetFailureMessages(MeterReading readingValue)
        {
            List<string> _messages = new List<string>();
            //You should not be able to load the same entry twice
            if (!IsDuplicateEntry(readingValue))
            {
                _messages.Add("There is already meter reading entry exists.");
            }
            //A meter reading must be associated with an Account ID to be deemed valid
            if (!IsValidAccountId(readingValue))
            {
                _messages.Add("Invalid Account Id.");
            }
            //Reading values should be in the format NNNNN
            if (!IsValidMeterReading(readingValue))
            {
                _messages.Add("Invalid meter reading entry.");
            }
            return _messages;
        }

        /// <summary>
        /// Check meter reading
        /// </summary>
        /// <param name="reading"></param>
        /// <returns></returns>
        private bool IsValidMeterReading(MeterReading reading)
        {
            if (reading.MeterReadingStringValue == null)
            {
                return false;
            }

            if (reading.MeterReadingStringValue.Trim() == string.Empty)
            {
                return false;
            }


            Regex regex = new Regex("^[0-9]*$");
            return regex.IsMatch(reading.MeterReadingStringValue.ToString());
        }

        /// <summary>
        /// Check duplicate entry
        /// </summary>
        /// <param name="reading"></param>
        /// <returns></returns>
        private bool IsDuplicateEntry(MeterReading reading)
        {
            //var repoMeterReading.GetAll().Where(x => x.CustomerId == Id).ToList();
            bool isValid = true;
            var data= repoMeterReading.GetAll().Where(x => x.AccountId == reading.AccountId && x.MeterReadingDateTime.ToShortDateString() == reading.MeterReadingDateTime.ToShortDateString());
            if(data != null)
            {
                if (data.Count() > 1)
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        /// <summary>
        /// Check valid account id
        /// </summary>
        /// <param name="reading"></param>
        /// <returns></returns>
        private bool IsValidAccountId(MeterReading reading)
        {

            if (reading.AccountId > 0)
            {
                var result = repoAccount.GetAll().Where(x => x.AccountId == reading.AccountId).FirstOrDefault();
                if (result != null)
                {
                    return (result.AccountId > 0);
                }
                return false;
            }
            return (reading.AccountId > 0);
            
        }

        /// <summary>
        /// Check meter readings are fine
        /// </summary>
        /// <param name="reading"></param>
        /// <returns></returns>
        private bool IsValid(MeterReading reading)
        {
           
            if (IsValidMeterReading(reading) == false ||
               IsDuplicateEntry(reading) == false ||
               IsValidAccountId(reading) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        


    }
}
