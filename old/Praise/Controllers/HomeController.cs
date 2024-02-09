using Microsoft.AspNetCore.Mvc;

namespace Praise.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        public ActionResult Get() => Ok("Praise Beta v0.1.1");
    }        
}
