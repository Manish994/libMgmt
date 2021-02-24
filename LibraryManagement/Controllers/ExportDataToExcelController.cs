using LibraryManagement.Repositories;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [ApiController]
    public class ExportDataToExcelController : ControllerBase
    {
        private readonly IExportDataToExcel _exportDataToExcel;

        private readonly ILibraryRepository _libraryRepository;

        public ExportDataToExcelController(IExportDataToExcel exportDataToExcel, ILibraryRepository libraryRepository)
        {
            _exportDataToExcel = exportDataToExcel;
            _libraryRepository = libraryRepository;
        }


        [HttpGet]
        [Route("exportDataToExcel/GetAll")]
        public async Task<IActionResult> AllStudents()
        {
            try
            {
                IEnumerable<StudentDetail> studentDetails = await _exportDataToExcel.AllStudents();
                // memory stream reads and writes to the memory. Stream is representation of bytes.
                // File stream deals with the files on disk.
                // use memory stream when you don't want anything to hit the disk.
                // use file stream when writing a file to disk.
                var stream = new MemoryStream();
                using var pck = new ExcelPackage(stream);
                var ws = pck.Workbook.Worksheets.Add("List Of Students");
                var staticHeaders = new string[]
               {
                   "Roll Number", "First Name", "Last Name", "Department", "Email", "Phone Number", "Address"
               };

                for (var i = 0; i < staticHeaders.Length; i++)
                {
                    var currentValue = (i + 1);
                    ws.Cells[1, currentValue].Value = staticHeaders[i];
                    ws.Cells[1, currentValue].Style.Font.Bold = true;
                    ws.Cells[1, currentValue].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                var dataRow = 2;
                foreach (var item in studentDetails.Select((x, i) => new { x, i }))
                {
                    ws.Cells[dataRow, 1].Value = item.x.CollegeRollNo;
                    ws.Cells[dataRow, 2].Value = item.x.FirstName;
                    ws.Cells[dataRow, 3].Value = item.x.LastName;
                    ws.Cells[dataRow, 4].Value = item.x.Department.Name;
                    ws.Cells[dataRow, 5].Value = item.x.Email;
                    ws.Cells[dataRow, 6].Value = item.x.ContactNumber;
                    ws.Cells[dataRow, 7].Value = item.x.Address;
                    dataRow += 1;
                }
                ws.Cells.AutoFitColumns();
                pck.Save();
                const string fileName = @"List Of Students.xlsx";
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
