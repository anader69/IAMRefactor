using IAMRefactor.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.Service.ALM
{
    public interface IALMService
    {
        string GenerateBaseUrlForClient(IAMConfigrationModel model);
    }
}
