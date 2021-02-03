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
    public class StudentDetailController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;
        public StudentDetailController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet]
        [Route("get-AllStudents")]
        public async Task<IActionResult> GetAllStudent()
        {
            try
            {
                IEnumerable<StudentDetail> studentDetails = await _libraryRepository.GetAllStudent();
                return Ok(studentDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("All-Department")]
        public async Task<IActionResult> All_Department()
        {
            try
            {
                IEnumerable<Department> objDepartment= await _libraryRepository.All_Department();
                return Ok(objDepartment);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("insert-newStudent")]
        public async Task<IActionResult> InsertNewStudent(StudentDetail studentdetail)
        {
            try
            {
                await _libraryRepository.InsertNewStudent(studentdetail);
                return Ok("New Student has been inserted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getStudentsById/{id}")]
        public async Task<IActionResult> getStudentsById(int id)
        {

            var studentdetail = await _libraryRepository.getStudentsById(id);
            return Ok(studentdetail);
        }

        [HttpPost]
        [Route("saveUpdate-StudentDetails")]
        public async Task<IActionResult> updateInsertStudent(StudentDetail studentdetail)
        {
            try
            {
                await _libraryRepository.updateInsertStudent(studentdetail);
                return Ok("Successfully Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("del-Student")]
        public async Task<IActionResult> DeleteStudentDetails(StudentDetail studentdetail)
        {

            await _libraryRepository.DelStudentsById(studentdetail);
            return Ok("Student has been successfully deleted");
        }

    }
}
