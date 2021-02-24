
using LibraryManagement.Repositories;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        public static IWebHostEnvironment _webHostEnvironment;
        public SearchTeacherController(ITeacherSearchRepository teacherSearchRepository, IWebHostEnvironment webHostEnvironment)
        {
            _teacherSearchRepository = teacherSearchRepository;
            _webHostEnvironment = webHostEnvironment;
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
        [Route("Search-TeacherWithId")]
        public async Task<IActionResult> SearchTeacherWithId(string teacherId)
            {
            try
            {
                TeacherDetail teacherDetail = await _teacherSearchRepository.SearchTeacherWithId(teacherId);
                //var response = teacherDetail.ImageName;
                //var img = _webHostEnvironment.WebRootPath + "\\Teacher\\" + response;
                //teacherDetail.ImageName = img;
                //teacherDetail.ImagePath= $"{Request.Scheme}://{Request.Host}/Teacher/" + teacherDetail.ImageName;

                return Ok(teacherDetail);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
