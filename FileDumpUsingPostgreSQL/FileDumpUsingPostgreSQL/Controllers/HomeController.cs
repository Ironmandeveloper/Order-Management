using FileDumpUsingPostgreSQL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Xml;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using Newtonsoft.Json;
using DocumentFormat.OpenXml;

namespace FileDumpUsingPostgreSQL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult LoadFile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoadFile(string fileSource, DateTime? startDate, DateTime? endDate, string searchText)
        {
            List<FileUploadModel> fileContent = null;
            if(fileSource == "json")
            {
                string jsonPath = "JsonFile.json";
                string jsonFileData = System.IO.File.ReadAllText(jsonPath);
                fileContent = JsonConvert.DeserializeObject<List<FileUploadModel>>(jsonFileData);
            }
            else
            {
                var fileFromDb = _dbContext.FileUpload.AsQueryable();
                if (startDate.HasValue)
                    fileFromDb = fileFromDb.Where(x => x.OrderDate.DateTime >= startDate.Value);
                if (endDate.HasValue)
                    fileFromDb = fileFromDb.Where(x => x.OrderDate.DateTime <= endDate.Value);
                if (!string.IsNullOrEmpty(searchText))
                    fileFromDb = fileFromDb.Where(x => EF.Functions.ILike(x.Name, "%" + searchText + "%"));

                fileContent = fileFromDb.ToList();
            }

            return View(fileContent);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(bool saveToFile)
        {
            string excelPath = "vodus-test-excel.xlsx";
            FileParseToJson(excelPath, "JsonFile.json", saveToFile);
            return View();
        }

        private void FileParseToJson(string excelPath, string resultFile, bool saveToFile)
        {
            try
            {
                using (SpreadsheetDocument excelDocument = SpreadsheetDocument.Open(excelPath, false))
                {
                    WorkbookPart workbook = excelDocument.WorkbookPart;
                    var book = workbook.Workbook;
                    Sheet excelSheet = book.Descendants<Sheet>().First();

                    if (excelSheet != null)
                    {
                        WorksheetPart workSheet = (WorksheetPart)workbook.GetPartById(excelSheet.Id);
                        SheetData sheet = workSheet.Worksheet.Elements<SheetData>().First();

                        // Create a list to hold C# objects
                        List<FileUploadModel> fileUpload = new List<FileUploadModel>();

                        foreach (Row row in sheet.Elements<Row>().Skip(1))
                        {
                            FileUploadModel fileModel = new FileUploadModel();

                            // Assuming the order of columns is Id, Name, Value
                            fileModel.Id = Convert.ToInt32(GetValue(row.Elements<Cell>().ElementAt(0), workbook));
                            fileModel.Image = GetValue(row.Elements<Cell>().ElementAt(1), workbook);
                            fileModel.Name = GetValue(row.Elements<Cell>().ElementAt(2), workbook);
                            double excelDateValue = Convert.ToDouble(GetValue(row.Elements<Cell>().ElementAt(3), workbook));
                            DateTime dateTimeValue = DateTime.FromOADate(excelDateValue).Date;
                            fileModel.OrderDate = new DateTimeOffset(dateTimeValue, TimeSpan.Zero); // Assuming no time zone offset information is available
                            string stringValueDiscount = GetValue(row.Elements<Cell>().ElementAt(4), workbook);
                            string decimalValueDiscount = stringValueDiscount.Replace("RM", "");
                            if (decimal.TryParse(decimalValueDiscount, out decimal fileDiscountedPrice))
                            {
                                fileModel.DiscountedPrice = fileDiscountedPrice;
                            }
                            string stringValuePrice = GetValue(row.Elements<Cell>().ElementAt(5), workbook);
                            string decimalValuePrice = stringValuePrice.Replace("RM", "");
                            if (decimal.TryParse(decimalValuePrice, out decimal filePrice))
                            {
                                fileModel.Price = filePrice;
                            }

                            fileUpload.Add(fileModel);
                        }

                        // Serialize the C# objects to JSON
                        if (saveToFile)
                        {
                            string jsonData = JsonConvert.SerializeObject(fileUpload, Newtonsoft.Json.Formatting.Indented);

                            // Save JSON data to a file
                            System.IO.File.WriteAllText(resultFile, jsonData);
                        }
                        else
                        {
                            if (_dbContext.FileUpload.ToList().Count == 0)
                            {
                                _dbContext.FileUpload.AddRange(fileUpload);
                                _dbContext.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        static string GetValue(Cell cell, WorkbookPart workbook)
        {
            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                SharedStringTablePart sharedTable = workbook.SharedStringTablePart;
                if (sharedTable != null)
                {
                    return sharedTable.SharedStringTable.Elements<SharedStringItem>().ElementAt(int.Parse(cell.InnerText)).InnerText;
                }
            }
            return cell.InnerText;
        }
    }
}