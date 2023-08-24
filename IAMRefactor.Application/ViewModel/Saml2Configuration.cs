using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.ViewModel
{
    public class Saml2Configuration
    {
        public ServiceProviderConfiguration ServiceProviderConfiguration { get; set; }

        public IList<IdentityProviderConfiguration> IdentityProviderConfiguration { get; set; }
    }
}
