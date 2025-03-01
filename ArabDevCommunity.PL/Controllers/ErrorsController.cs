using ArabDevCommunity.PL.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArabDevCommunity.PL.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {

        public ActionResult Error(int Code)
        {
            return NotFound(new ApiResponse(Code));
        }
    }
}
