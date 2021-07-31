using ENSEKCoreWebApp.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ENSEKCoreWebApp.Helpers
{
    public class CommonRoutines
    {

        /// <summary>
        /// Get data from CSV
        /// </summary>
        /// <param name="csv_file_path"></param>
        /// <returns></returns>
        public static List<MeterReading> ReadCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            List<MeterReading> _meterReading = new List<MeterReading>();
            string jsonString = string.Empty;
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields;
                    bool tableCreated = false;
                    int cnt = 0;
                    while (tableCreated == false)
                    {
                        colFields = csvReader.ReadFields();
                        foreach (string column in colFields)
                        {
                            if (cnt < 3)
                            {
                                DataColumn datecolumn = new DataColumn(column);
                                datecolumn.AllowDBNull = true;
                                csvData.Columns.Add(datecolumn);
                            }

                            cnt++;
                        }
                        tableCreated = true;
                    }

                    while (!csvReader.EndOfData)
                    {
                        csvData.Rows.Add(csvReader.ReadFields());
                    }
                    for (int i = 0; i < csvData.Rows.Count; i++)
                    {
                        Account _customer = new Account();
                        MeterReading _readingValue = new MeterReading();
                        if (csvData.Rows[i]["AccountId"] != null)
                        {
                            if (csvData.Rows[i]["AccountId"].ToString() != string.Empty)
                            {
                                try
                                {
                                    _customer.AccountId = int.Parse(csvData.Rows[i]["AccountId"].ToString());
                                    _readingValue.Account = _customer;
                                }
                                catch (Exception)
                                {


                                }
                            }
                            
                        }
                        if (csvData.Rows[i]["MeterReadingDateTime"] != null)
                        {
                            if (csvData.Rows[i]["MeterReadingDateTime"].ToString() != string.Empty)
                            {
                                try
                                {
                                    _readingValue.MeterReadingDateTime = DateTime.Parse(csvData.Rows[i]["MeterReadingDateTime"].ToString());
                                }
                                catch (Exception)
                                {


                                }
                            }
                            
                            
                        }
                        if (csvData.Rows[i]["MeterReadValue"] != null)
                        {

                            _readingValue.MeterReadingStringValue = csvData.Rows[i]["MeterReadValue"].ToString();


                        }



                        if (_readingValue.Account != null)
                        {
                            if (_readingValue.Account.AccountId > 0)
                            {
                                _readingValue.AccountId = _readingValue.Account.AccountId;
                                _meterReading.Add(_readingValue);
                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // return "Error:Parsing CSV";
            }
            //if everything goes well, serialize csv to json 
            //  jsonString = JsonConvert.SerializeObject(csvData);
            return _meterReading;
            // return jsonString;
        }

        /// <summary>
        /// Re-arranged data
        /// </summary>
        /// <param name="customerData"></param>
        /// <returns></returns>
        public static List<Account> GetAccounts(List<Account> customerData)
        {
            var groupedResult = (from cust in customerData
                                 group cust by cust.AccountId).ToList();

            List<Account> _accountList = new List<Account>();
            foreach (var custIdGroup in groupedResult)
            {
                var result = _accountList.Any(x => x.AccountId == custIdGroup.Key);
                if (!result)
                {
                    string Forename = string.Empty;
                    string Surname = string.Empty;
                    int? AccountId;
                    List<MeterReading> _meterReadings = new List<MeterReading>();
                    foreach (Account item in custIdGroup)
                    {
                        foreach (var vehicle in item.MeterReadings)
                        {
                            _meterReadings.Add(new MeterReading
                            {
                                Id  = vehicle.Id,
                                MeterReadingDateTime = vehicle.MeterReadingDateTime,
                                MeterReadingValue = vehicle.MeterReadingValue,
                                Account = vehicle.Account,
                             
                            });
                        }



                        Forename = item.FirstName;
                        Surname = item.LastName;
                        AccountId = item.AccountId;

                    }
                    _accountList.Add(new Account
                    {
                        AccountId = custIdGroup.Key,
                       
                        FirstName = Forename,
                        LastName = Surname,
                        MeterReadings = _meterReadings
                    });
                }

            }

            return _accountList;
        }
    }

   
}
