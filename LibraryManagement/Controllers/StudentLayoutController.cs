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
    public class StudentLayoutController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;
        public StudentLayoutController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        [HttpGet]
        [Route("get-DetailStudent")]
        public async Task<IActionResult> GetDetailStudent()
        {
            try
            {
                IEnumerable<StudentDetail> studentDetails = await _libraryRepository.GetDetailStudent();
                return Ok(studentDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
