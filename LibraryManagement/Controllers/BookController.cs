using LibraryManagement.Repositories;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [ApiController]
    public class BookController : Controller
    {

        private readonly ILibraryRepository _libraryRepository;

        public BookController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        [HttpGet]
        [Route("Response-All")]
        public async Task<IActionResult> ResponseAll()
        {
            try
            {
                IEnumerable<BookDetail> departments = await _libraryRepository.ResponseAll();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("books/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {

            var teacherdetail = await _libraryRepository.GetBookById(id);
            return Ok(teacherdetail);
        }

        [HttpPost]
        [Route("saveUpdate-BookDetails")]
        public async Task<IActionResult> updateInsertBook(BookDetail bookDetail)
        {

            try
            {
                await _libraryRepository.updateInsertBook(bookDetail);
                return Ok("Successfully Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("del-Book")]
        public async Task<IActionResult> DelBookById(BookDetail bookDetail)
        {

            await _libraryRepository.DelBookById(bookDetail);
            return Ok("Book has been successfully deleted");
        }

        [HttpPost]
        [Route("create-newBooks")]
        public async Task<IActionResult> Insert(BookDetail bookDetail)
        {
            try
            {
                await _libraryRepository.Insert(bookDetail);
                return Ok("New Book has been inserted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("book/BookById")]
        public async Task<IActionResult> BookById(int id)
        {
            try
            {
                return Ok(_libraryRepository.BookById(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
