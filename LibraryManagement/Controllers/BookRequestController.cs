
using LibraryManagement.Repositories;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test.Authentication;

namespace LibraryManagement.Controllers
{
    [ApiController]
    public class BookRequestController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;
        public BookRequestController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }


        [HttpPost]
        [Route("Insert-Request")]
        public async Task<IActionResult> InsertRequest()
        {
            try
            {
                RequestBook objRequestBook = new RequestBook();
                objRequestBook.CollegeRollNo = HttpContext.Request.Form["collegeRollNo"];
                objRequestBook.FirstName= HttpContext.Request.Form["FirstName"];
                objRequestBook.LastName = HttpContext.Request.Form["LastName"];
                objRequestBook.DepartmentName = HttpContext.Request.Form["Department"];
                objRequestBook.Email = HttpContext.Request.Form["Email"];
                objRequestBook.BookName = HttpContext.Request.Form["BookName"];
                objRequestBook.BookId = HttpContext.Request.Form["BookId"];
                objRequestBook.BookAuthor = HttpContext.Request.Form["AuthorName"];
                objRequestBook.IssueDate = Convert.ToDateTime(HttpContext.Request.Form["IssueDate"]);
                objRequestBook.DueDate = Convert.ToDateTime(objRequestBook.IssueDate.Value.AddDays(7).ToString("yyyy/MM/dd"));
                await _libraryRepository.InsertRequest(objRequestBook);

                return Ok("Successfully Requested");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Response-Request")]
        public async Task<IActionResult> ResponseRequest()
        {
            try
            {

                IEnumerable<RequestBook> objRequestBook = await _libraryRepository.ResponseRequest();
               
                return Ok(objRequestBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("Approve-Request")]
        public async Task<IActionResult> ResponseRequest(RequestBook objRequestBook)
        {

            if (objRequestBook.Action == null)
            {
                objRequestBook.Action = true;
                await _libraryRepository.ResponseRequest(objRequestBook);
            }
            else
            {
                return BadRequest("You Already Approved Book Request !!!");
            }
            return Ok("Successfully Approved");
            
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("Cancel-Request")]
        public async Task<IActionResult> CancelRequest(RequestBook objRequestBook)
        {

            if (objRequestBook.Action == null)
            {
                objRequestBook.Action = false;
                await _libraryRepository.CancelRequest(objRequestBook);
            }
            else
            {
                return BadRequest("You Already Cancelled Book Request !!!");
            }
            return Ok("Successfully Cancelled.");

        }

        [HttpGet]
        [Route("bookRequest/pullAllExcel")]
        public async Task<IActionResult> PullAll()
        {
            try
            {
                IEnumerable<RequestBook> objRequestBooks = await _libraryRepository.PullAll();

                //create memorystream (constructor)object that will hold into memory(RAM).
                var stream = new MemoryStream();

                // create excelpackage (constructor)object that takes memorystream object as parameter.
                // and excelpackage object is disposable.
                using var pak = new ExcelPackage(stream);

                // create a new worksheet.
                var ws = pak.Workbook.Worksheets.Add("Request Book");

                // create headers for worksheet.
                var staticHeaders = new string[]
                {
                    "Student ID", "First Name", "Last Name", "Branch", "Email", "Book Id", "Book Name",
                    "Author Name", "Book Issue Date", "Book Due Date", "Action"
                };

                // Implement headers in new worksheet(ws) and styling new worksheet.
                for(var i = 0; i < staticHeaders.Length; i++)
                {
                    // starts from first row and first column
                    var currentValue = (i + 1);
                    ws.Cells[1, currentValue].Value = staticHeaders[i];
                    ws.Cells[1, currentValue].Style.Font.Bold = true;
                    ws.Cells[1, currentValue].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Implement new worksheet(ws) body from second row
                var dataRow = 2;

                // Select projection is used to select data from a data source and creating a new form of data.
                // If i need my data in original form then i can do that, but if i need to apply some operations on that data then projection will help in that part.
                // select query will execute by foreach loop.
                foreach(var item in objRequestBooks.Select((x, i) => new { x, i }))
                {
                    ws.Cells[dataRow, 1].Value = item.x.CollegeRollNo;
                    ws.Cells[dataRow, 2].Value = item.x.FirstName;
                    ws.Cells[dataRow, 3].Value = item.x.LastName;
                    ws.Cells[dataRow, 4].Value = item.x.DepartmentName;
                    ws.Cells[dataRow, 5].Value = item.x.Email;
                    ws.Cells[dataRow, 6].Value = item.x.BookId;
                    ws.Cells[dataRow, 7].Value = item.x.BookName;
                    ws.Cells[dataRow, 8].Value = item.x.BookAuthor;
                    ws.Cells[dataRow, 9].Value = item.x.IssueDate.Value.ToString("yyyy-MM-dd");
                    ws.Cells[dataRow, 10].Value = item.x.DueDate.Value.ToString("yyyy-MM-dd");
                    dataRow += 1;
                }

                ws.Cells.AutoFitColumns();
                pak.Save();
                const string fileName = @"Requested_Book.xlxs";
                const string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                stream.Position = 0;
                return File(stream, fileType, fileName);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
