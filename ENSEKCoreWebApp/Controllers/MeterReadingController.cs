using ENSEKCoreWebApp.Helpers;
using ENSEKCoreWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ENSEKCoreWebApp.Controllers
{
    public class MeterReadingController : Controller
    {
        public const string URI = "http://localhost:5165/api/meterreading/";
        public async Task<IActionResult> Index()
        {
            MeterReadingViewModel results = new MeterReadingViewModel();
            List<MeterReading> _meterReadings = new List<MeterReading>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(URI + "GetMeterReadings"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _meterReadings = JsonConvert.DeserializeObject<List<MeterReading>>(apiResponse);

                    foreach (var reading in _meterReadings)
                    {
                        

                        results.meterReadings.Add(new MeterReading
                        {
                            AccountId = reading.AccountId,
                             Id = reading.Id,
                            MeterReadingDateTime = reading.MeterReadingDateTime,
                            MeterReadingStringValue = reading.MeterReadingStringValue,
                            MeterReadingValue = reading.MeterReadingValue
                        });
                    }
                }
            }
            return View(results);
        }

        public IActionResult Import()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            ImportMeterReadingViewModel viewModel = new ImportMeterReadingViewModel();
            //Get data from csv file
            List<MeterReading> meterReadings = CommonRoutines.ReadCSVFile(path);
            List<MeterReading> distinctMeterReadings = new List<MeterReading>();
            var data = meterReadings.Distinct(new DistinctItemComparer());

            foreach (var meterReading in data)
            {
                distinctMeterReadings.Add(new MeterReading {
                     AccountId = meterReading.AccountId,
                     MeterReadingDateTime = meterReading.MeterReadingDateTime,
                    MeterReadingStringValue = meterReading.MeterReadingStringValue 
                });
            }
           
            List<MeterReading> resultData = new List<MeterReading>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(URI + "meter-reading-uploads");
                StringContent content = new StringContent(JsonConvert.SerializeObject(distinctMeterReadings), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(httpClient.BaseAddress, content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                   
                    resultData = JsonConvert.DeserializeObject<List<MeterReading>>(apiResponse);
                }
            }

            var orderByResult = (from s in resultData
                                orderby s.IsSuccess descending, s.MeterReadingDateTime,s.MeterReadingValue
                                select s).ToList();

            viewModel.MeterReadings = orderByResult;
            return View(viewModel);
         
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details(FormEditMeterReadingViewModel meterReading)
        {

            using (var httpClient = new HttpClient())
            {
                MeterReading _meterReading = new MeterReading
                {    Id = meterReading.Id,
                     AccountId = (int)meterReading.AccountId, 
                     MeterReadingDateTime  = meterReading.MeterReadingDateTime , 
                     MeterReadingValue  = meterReading.MeterReadingValue , 
                     MeterReadingStringValue = meterReading.MeterReadingValue.ToString()
                };
                httpClient.BaseAddress = new Uri(URI + "UpdateMeterReading");
                StringContent content = new StringContent(JsonConvert.SerializeObject(_meterReading), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(httpClient.BaseAddress, content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    //var resultData = JsonConvert.DeserializeObject<Account>(apiResponse);
                }
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Details(int meterReadingId)
        {

            FormEditMeterReadingViewModel viewModel = new FormEditMeterReadingViewModel { Id = (int)meterReadingId };

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(URI + "GetMeterReadingById/?meterReadingId=" + meterReadingId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var _account = JsonConvert.DeserializeObject<MeterReading>(apiResponse);

                    if (_account != null)
                    {
                        viewModel.Id = _account.Id;
                        viewModel.AccountId = _account.AccountId;
                        viewModel.MeterReadingDateTime = _account.MeterReadingDateTime;
                        viewModel.MeterReadingValue = _account.MeterReadingValue;

                    }
                }
            }

            return View("FormEditMeterReading", viewModel);
        }
    }

}
