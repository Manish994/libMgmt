using LibraryManagement.Repositories;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [ApiController]
    public class UploadExcelDataController : Controller
    {
        private readonly IUploadDownloadExcel _uploadDownloadExcel;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadExcelDataController(IUploadDownloadExcel uploadDownloadExcel, IWebHostEnvironment webHostEnvironment)
        {
            _uploadDownloadExcel = uploadDownloadExcel;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("UploadExcelData/All")]
        public async Task <IActionResult>InsertExcel()
        {
            try
            {
                var file = HttpContext.Request.Form.Files[0];
                var uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files", "Request Book");
                var ext = Path.GetExtension(file.FileName)?.ToLower();
                var extensions = new string[] { ".xls", ".xlsx" };
                var flag = false;
                foreach (var item in extensions)
                {
                    if (item != ext) continue;
                    flag = true;
                    break;
                }
                if (!flag) throw new Exception("Not a valid extension !");
                var fileName = DateTime.UtcNow.AddMinutes(345).ToString("yyyyMMddhhmmssfff") + ext;
                await using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                var package = new ExcelPackage(ms);
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.FirstOrDefault(x => x.Name == "STUDENT_INFO");
                if (worksheet is null)
                    throw new Exception("A worksheet with name Request Book cannot be found!");
                var rows = worksheet.Dimension.Rows;
                var columns = worksheet.Dimension.Columns;
                if (rows <= 1 || columns <= 1)
                    throw new Exception("Unable to find data!");
                var dt = new DataTable();
                for (var row = 1; row <= 1; row++)
                {
                    for (var col = 1; col <= columns; col++)
                    {
                        dt.Columns.Add(worksheet.Cells[row, col].Value.ToString()?.Trim());
                    }
                }
                for (var row = 2; row <= rows; row++)
                {
                    var dr = dt.NewRow();
                    for (var col = 1; col <= columns; col++)
                    {
                        dr[col - 1] = worksheet.Cells[row, col].Value.ToString()?.Trim();
                    }
                    dt.Rows.Add(dr);
                }
                var objUploadExcel = new UploadExcel();
                objUploadExcel.DetailsDT = dt;
                await _uploadDownloadExcel.InsertExcel(objUploadExcel);
                var filePath = Path.Combine(uploadPath, fileName);
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);
                return Ok("Save Changes Successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("DownloadExcelData/ExcelFormat")]
        public async Task<IActionResult> DownloadExcelFormat()
        {
            try
            {
               
                //create memorystream (constructor)object that will hold into memory(RAM).
                 var stream = new MemoryStream();

                // create excelpackage (constructor)object that takes memorystream object as parameter.
                // and excelpackage object is disposable.
                using var pak = new ExcelPackage(stream);

                // create a new worksheet.
                var ws = pak.Workbook.Worksheets.Add("STUDENT_INFO");

                // create headers for worksheet.
                var staticHeaders = new string[]
                {
                    "FirstName", "LastName", "Email", "Department"
                };

                // Implement headers in new worksheet(ws) and styling new worksheet.
                for (var i = 0; i < staticHeaders.Length; i++)
                {
                    // starts from first row and first column
                    var currentValue = (i + 1);
                    ws.Cells[1, currentValue].Value = staticHeaders[i];
                    ws.Cells[1, currentValue].Style.Font.Bold = true;
                    ws.Cells[1, currentValue].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                ws.Cells.AutoFitColumns();
                pak.Save();
                const string fileName = @"STUDENT_INFO.xlsx";
                const string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, fileType, fileName);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
