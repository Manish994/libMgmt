using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Interfaces;
using LibraryManagement.Repositories;
using LibraryManagement.Request;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [ApiController]

    [Route("api")]
    public class LoginController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public LoginController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Signin(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Missing login details");
            }
            var loginResponse = await customerService.Signin(loginRequest);

            if (loginResponse == null)
            {
                return BadRequest($"Invalid credentials");
            }

            return Ok(loginResponse);
        }
    }
}
