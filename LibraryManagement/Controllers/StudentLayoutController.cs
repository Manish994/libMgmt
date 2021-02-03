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
    public class studentlayoutcontroller : Controller
    {
        private readonly ILibraryRepository _libraryRepository;
        public studentlayoutcontroller(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        [HttpGet]
        [Route("get-detailstudent")]
        public async Task<IActionResult> getdetailstudent()
        {
            try
            {
                IEnumerable<StudentDetail> studentdetails = await _libraryRepository.GetDetailStudent();
                return Ok(studentdetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
