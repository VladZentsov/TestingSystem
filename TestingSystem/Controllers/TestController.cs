using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace TestingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("avaliableTests")]
        public async Task<ActionResult<IEnumerable<string>>> GetAvaliableTests()
        {
            var id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value;

            return Content(JsonConvert.SerializeObject(await _testService.GetAvaliableTestNames(id)));
        }
    }
}
