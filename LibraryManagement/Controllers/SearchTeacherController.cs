
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
    public class SearchTeacherController : Controller
    {
        private readonly ITeacherSearchRepository _teacherSearchRepository;
        
        public SearchTeacherController(ITeacherSearchRepository teacherSearchRepository)
        {
            _teacherSearchRepository = teacherSearchRepository;
        }


        [HttpGet]
        [Route("fetch-teachersId")]
        public async Task<IActionResult> AllTeachersId()
        {
            try
            {
                IEnumerable<TeacherDetail> teacherDetail = await _teacherSearchRepository.AllTeachersId();
                return Ok(teacherDetail);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Search-TeacherWithId/{teacherId}")]
        public async Task<IActionResult> SearchTeacherWithId(string teacherId)
        {
            try
            {
                var teacherDetail = await _teacherSearchRepository.SearchTeacherWithId(teacherId);
                return Ok(teacherDetail);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
