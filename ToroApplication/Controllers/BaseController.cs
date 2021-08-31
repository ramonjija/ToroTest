using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ToroApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected int GetUserId()
        {
            return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
        }

        protected string GetUserCpf()
        {
            return this.User.Claims.First(i => i.Type == "Cpf").Value;
        }
    }
}
