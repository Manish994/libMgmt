using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [ApiController]
    public class DateController : Controller
    {
        [HttpGet]
        [Route("Current-Date")]
        public IActionResult CurrentDate()
        {
            string currentDate =  DateTime.Now.ToString("yyyy/MM/dd");
             return Ok(currentDate);
        }
    }
}
