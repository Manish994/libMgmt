using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Repositories;
using LibraryManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Authentication;

namespace LibraryManagement.Controllers
{
    [ApiController]
    public class TeacherDetails : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public static IWebHostEnvironment _webHostEnvironment;
        public TeacherDetails(ILibraryRepository libraryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _libraryRepository = libraryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("insert-newteacher")]
        public async Task<IActionResult> Insert()
        {

            try
            {
                IFormFile files = HttpContext.Request.Form.Files[0];
                string ext = Path.GetExtension(files.FileName).ToLowerInvariant();
                string fileName = DateTime.UtcNow.AddMinutes(345).ToString("yyyyMMddHHmmssffff") + ext;
                string path = _webHostEnvironment.WebRootPath + "\\Teacher\\" + fileName;
                using (FileStream fileStream = System.IO.File.Create(path + files.FileName))
                {
                    await files.CopyToAsync(fileStream);
                }

                TeacherDetail teacherDetail = new TeacherDetail();
                teacherDetail.TeacherId = HttpContext.Request.Form["TeacherId"];
                teacherDetail.FirstName = HttpContext.Request.Form["FirstName"];
                teacherDetail.LastName = HttpContext.Request.Form["LastName"];
                teacherDetail.DepartmentId = int.Parse(HttpContext.Request.Form["Id"]);
                teacherDetail.Email = HttpContext.Request.Form["Email"];
                teacherDetail.ContactNumber = HttpContext.Request.Form["Contact"];
                teacherDetail.ImageName = fileName;
                teacherDetail.CitizenshipNo = HttpContext.Request.Form["CitizenshipNo"];
                await _libraryRepository.Insert(teacherDetail);



                return Ok("New Teacher has been successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.User)]
        [HttpGet]
        [Route("get-Teachers")]
        public async Task<IActionResult> GetAllTeacher()
        {
            try
            {
               
                IEnumerable<TeacherDetail> teacherDetails = await _libraryRepository.GetAllTeacher();

                return Ok(teacherDetails);
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
            //var imgNmae = Guid.NewGuid().ToString();

            try
            {
                teacherdetail.DepartmentId = teacherdetail.Department.Id;
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
