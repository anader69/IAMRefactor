using IAMRefactor.Application.Service.SignatureBussiness;
using IAMRefactor.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IAMRefactor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly ISignatureService signatureService;

        public SignatureController(ISignatureService _signatureService)
        {
            signatureService = _signatureService;
        }

        [HttpPost]
        [Route("Sign")]
        public async Task<string> Sign([FromBody] SignModel signModel)
        {
          var result= await signatureService.Sign(signModel);
            return result;
        }

  

    }
}
