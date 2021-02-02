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
    public class TeacherDetails : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public TeacherDetails(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpPost]
        [Route("insert-newteacher")]
        public async Task<IActionResult> Insert(TeacherDetail teacherdetail)
        {
            try
            {
                await _libraryRepository.Insert(teacherdetail);
                return Ok("New Teacher has been successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get-Teachers")]
        public async Task<IActionResult> GetAllTeacher()
        {
            try
            {
                IEnumerable<TeacherDetail> TeacherDetails = await _libraryRepository.GetAllTeacher();
                return Ok(TeacherDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("del-Teacher")]
        public async Task<IActionResult> DeleteTeacherDetails(TeacherDetail teacherdetail)
        {

            await _libraryRepository.DelById(teacherdetail);
            return Ok("Teacher has been successfully deleted");
        }

        [HttpGet]
        [Route("teachers/{id}")]
        public async Task<IActionResult> EditTeacherDetails(int id)
        {

            var teacherdetail = await _libraryRepository.GetById(id);
            return Ok(teacherdetail);
        }

        [HttpPost]
        [Route("saveUpdate-TeacherDetails")]
        public async Task<IActionResult> updateInsert(TeacherDetail teacherdetail)
        {
            try
            {
                await _libraryRepository.updateInsert(teacherdetail);
                return Ok("Successfully Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
