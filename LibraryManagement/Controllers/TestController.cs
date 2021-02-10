using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public class TheTest
        {
            public string Test { get; set; }
        }

        [Route("")]
        [HttpPost]
        public IActionResult Index()
        {
            //var theTest = HttpContext.Request.Form.FirstOrDefault(x => x.Key == "test");
            //var req = HttpContext.Request.Form.Files;
            if(Request.Form.Files.Count() > 0)
            {
                IFormFile file = Request.Form.Files[0];
            }
            return Ok();
        }
    }
}
