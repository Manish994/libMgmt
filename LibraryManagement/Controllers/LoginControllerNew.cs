//using LibraryManagement.Repositories;
//using LibraryManagement.ViewModel;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LibraryManagement.Controllers
//{
//    [ApiController]
//    public class LoginControllerNew : Controller
//    {
//        private readonly ILibraryRepository _libraryRepository;
//        public LoginControllerNew(ILibraryRepository libraryRepository)
//        {
//            _libraryRepository = libraryRepository;
//        }
//        [HttpPost]
//        [Route("signin")]
//        public async Task<IActionResult> Signup(Login login)
//        {

//            if (login == null || string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
//            {
//                return BadRequest("Missing register details");
//            }
//            else
//            {

//                await _libraryRepository.Signin(login);
//                return Ok("Successfully Login.");
//            }
//            //return Ok("Successfully Register");

//        }
//    }
//}
