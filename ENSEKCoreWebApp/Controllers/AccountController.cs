using ENSEKCoreWebApp.Helpers;
using ENSEKCoreWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ENSEKCoreWebApp.Controllers
{
    public class AccountController : Controller
    {
        public const string URI = "http://localhost:5165/api/account/";
        public async Task<IActionResult> Index()
        {
            List<Account> customerList = new List<Account>();
            AccountViewModel results = new AccountViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(URI + "GetAccounts"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerList = JsonConvert.DeserializeObject<List<Account>>(apiResponse);

                    foreach (var accountItem in customerList)
                    {
                        List<MeterReading> meterReading = new List<MeterReading>();
                        if (accountItem.MeterReadings != null) 
                        {
                            foreach (var meterItem in accountItem.MeterReadings)
                            {
                                meterReading.Add(new MeterReading
                                {
                                    Id = meterItem.Id,
                                    MeterReadingDateTime = meterItem.MeterReadingDateTime,
                                    MeterReadingValue = meterItem.MeterReadingValue,
                                    Account = meterItem.Account
                                });
                            }
                        }
                        
                        results.accounts.Add(new Account
                        {
                            AccountId = accountItem.AccountId,
                            FirstName = accountItem.FirstName,
                            LastName = accountItem.LastName,
                            MeterReadings = meterReading
                        });
                    }
                }
            }
            return View("Index", results);


        }
             
        [HttpGet]
        public async Task<IActionResult> Details(int accountId)
        {

            FormEditAccountViewModel viewModel = new FormEditAccountViewModel { AccountId = accountId };

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(URI + "GetAccountById/?accountId=" + accountId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var _account = JsonConvert.DeserializeObject<Account>(apiResponse);

                    if (_account != null)
                    {
                        viewModel.FirstName = _account.FirstName;
                        viewModel.LastName = _account.LastName;
                        viewModel.AccountId = _account.AccountId;
                    }
                }
            }



            return View("FormEditAccount", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details(FormEditAccountViewModel account)
        {

            using (var httpClient = new HttpClient())
            {
                Account _account = new Account { AccountId = (int)account.AccountId, FirstName = account.FirstName, LastName = account.LastName };
                httpClient.BaseAddress = new Uri(URI + "UpdateAccount");
                StringContent content = new StringContent(JsonConvert.SerializeObject(_account), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(httpClient.BaseAddress, content))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    //var resultData = JsonConvert.DeserializeObject<Account>(apiResponse);
                }
            }

            return RedirectToAction("Index");

        }

             
    }
}
