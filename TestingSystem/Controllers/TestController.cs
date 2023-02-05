using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace TestingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet("avaliableTests")]
        public async Task<ActionResult<IEnumerable<TestNames>>> GetAvaliableTests()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value;

            return Content(JsonConvert.SerializeObject(await _testService.GetAvaliableTestNames(userId)));
        }

        [HttpGet("testDescription/{testId}")]
        public async Task<ActionResult<TestDescriptionModel>> GetTestDescription(string testId)
        {
            //var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value;

            return Content(JsonConvert.SerializeObject(await _testService.GetTestDescription(testId)));
        }

        [HttpGet("startTest/{testId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetTest(string testId)
        {
            //var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value;

            return Content(JsonConvert.SerializeObject(await _testService.GetByIdWithDetailsAsync(testId)));
        }

        [HttpPost("checkResults")]
        public async Task<ActionResult> CheckResults([FromBody] TestingAnswer value)
        {
            var result = await _testService.CheckTestAndGetResults(value);

            return Content(JsonConvert.SerializeObject(result));
        }

    }
}
