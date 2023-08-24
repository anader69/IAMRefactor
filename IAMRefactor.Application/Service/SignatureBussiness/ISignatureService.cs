using IAMRefactor.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.Service.SignatureBussiness
{
    public interface ISignatureService
    {
        Task<string> Sign(SignModel signModel);
    }
}
