using IAMRefactor.Application.Service.ALM;
using IAMRefactor.Application.ViewModel;
using IAMRefactor.Common.Helper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IAMRefactor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public IALMService aLMService { get; }

        public AlmController(IConfiguration configuration, IALMService _aLMService)
        {
            _configuration = configuration;
            aLMService = _aLMService;
        }

        [HttpGet]
        [Route("IamLogin")]
        public string IamLogin()
        {
            var formatSettings = _configuration.GetSection("IAM").Get<IAMConfigrationModel>();
           var result= aLMService.GenerateBaseUrlForClient(formatSettings);
            return result;
        }

    }
}
