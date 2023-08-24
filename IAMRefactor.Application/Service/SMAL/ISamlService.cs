using IAMRefactor.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.Service.SMAL
{
    public interface ISamlService
    {
        string GetAuthnRequestUrl(Saml2Configuration config);
    }
}
