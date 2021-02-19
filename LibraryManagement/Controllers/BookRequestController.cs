
using LibraryManagement.Repositories;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    }
}
