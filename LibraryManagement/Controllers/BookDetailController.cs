using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Repositories;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [ApiController]
    public class BookDetailController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;
        public BookDetailController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        [Route("getallbooks")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<BookDetail> BookDetails = await _libraryRepository.GetAll();
                return Ok(BookDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("create-newBooks")]
        public async Task<IActionResult> Insert(BookDetail bookdetail)
        {
            try
            {
                await _libraryRepository.Insert(bookdetail);
                return Ok("New book has been inserted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("del-Book")]
        public async Task<IActionResult> DeleteBookDetails(BookDetail bookdetail)
        {

            await _libraryRepository.DelBookById(bookdetail);
            return Ok("Book has been successfully deleted");
        }

        [HttpGet]
        [Route("books/{id}")]
        public async Task<IActionResult> EditBooksDetails(int id)
        {

            var bookdetail = await _libraryRepository.GetBookById(id);
            return Ok(bookdetail);
        }

        [HttpPost]
        [Route("saveUpdate-BookDetails")]
        public async Task<IActionResult> updateInsertBook(BookDetail bookdetail)
        {
            try
            {
                await _libraryRepository.updateInsertBook(bookdetail);
                return Ok("Successfully Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
