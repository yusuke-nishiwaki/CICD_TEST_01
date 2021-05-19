using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Controllers.ViewModel;
using SampleAPI.Models;

namespace SampleAPI.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IGenerateRandomId _generateRandomId;
        private readonly IAuthentication _authentication;

        public TestController(IGenerateRandomId generateRandomId,
                                IAuthentication authentication)
        {
            _authentication = authentication;
            _generateRandomId = generateRandomId;
        }

        [Route("api/test")]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] TestRequestModel model)
        {
            try
            {
                await _authentication.ExcuteAsync(model.AccessID, model.Password);
                return Ok(_generateRandomId.Generate(model.AccessID, model.Length));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}