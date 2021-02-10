using LibraryManagement.Model;
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
    public class RegisterController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;
        public RegisterController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Signup(Login testRegister)
        {

            if (testRegister == null || string.IsNullOrEmpty(testRegister.Username) || string.IsNullOrEmpty(testRegister.Password))
            {
                return BadRequest("Missing register details");
            }
            else
            {

                await _libraryRepository.Signup(testRegister);
                return Ok("Successfully Register.");
            }
            //return Ok("Successfully Register");

        }
    }
}
