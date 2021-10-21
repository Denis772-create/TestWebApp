using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TestWebApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "some value";
        }
    }
}
