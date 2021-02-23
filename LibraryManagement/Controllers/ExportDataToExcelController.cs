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
    public class ExportDataToExcelController : Controller
    {
        private readonly IExportDataToExcel _exportDataToExcel;

        public ExportDataToExcelController(IExportDataToExcel exportDataToExcel)
        {
            _exportDataToExcel = exportDataToExcel;
        }


        [HttpGet]
        [Route("ExportDataToExcel/GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                IEnumerable<StudentDetail> studentDetails = await _exportDataToExcel.GetAllStudents();
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
                    ws.Cells[dataRow, 2].Value = item.x.LastName;
                    ws.Cells[dataRow, 2].Value = item.x.Department.Name;
                    ws.Cells[dataRow, 2].Value = item.x.Email;
                    ws.Cells[dataRow, 2].Value = item.x.ContactNumber;
                    ws.Cells[dataRow, 2].Value = item.x.Address;
                    dataRow += 1;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
