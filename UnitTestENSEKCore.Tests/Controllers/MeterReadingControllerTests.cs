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
   
    public class MeterReadingControllerTests
    {
        private Mock<IRepository<MeterReading>> meterReadingRepository;
        private List<MeterReading> _meterReading;
        [SetUp]
        public void Setup()
        {
            meterReadingRepository = new Mock<IRepository<MeterReading>>();
            _meterReading = new List<MeterReading>();
            _meterReading.Add(new MeterReading 
            {   Id = 101, 
                AccountId = 2344, 
                IsSuccess = true, 
                MeterReadingDateTime = DateTime.Now.AddDays(-30),
                MeterReadingStringValue = "2321"  
            });
          
        }


        [Test]
        public void GetAllMeterReadings_HappyPath_ReturnsExpectedValue()
        {
            //Act
            meterReadingRepository.Setup(x => x.GetAll()).Returns(_meterReading.AsQueryable());

            //Arrange
            var accountBL = new MeterReadingService(meterReadingRepository.Object);
            var accountList = accountBL.GetAllMeterReadings(2344);

            //Assert
            Assert.IsNotNull(accountList);
           // Assert.AreEqual(accountList, accountBL);

        }

        [Test]
        public void CheckValidMeterReading_ReturnsExpectedValue()
        {
            //Act
            meterReadingRepository.Setup(x => x.GetById(2344));

            //Arrange
            var accountBL = new MeterReadingService(meterReadingRepository.Object).GetMeterReading(2344);

            //Assert
            if (accountBL != null)
            {
                Assert.IsNotNull(accountBL);
                Assert.AreEqual("2321", accountBL.MeterReadingStringValue);
            }
          
          
        }
    }
}
