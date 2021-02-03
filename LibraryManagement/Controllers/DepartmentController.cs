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


    public class DepartmentController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public DepartmentController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        [Route("department")]
        public async Task<IActionResult> Department()
        {
            try
            {
                IEnumerable<Department> departments = await _libraryRepository.Department();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
