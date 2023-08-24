using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.ViewModel
{
    public class ServiceProviderConfiguration
    {
        public string EntityId { get; set; }

        public bool OmitAssertionSignatureCheck { get; set; }

        public string AssertionConsumerServiceUrl { get; set; } = "Saml2/AssertionConsumerService";

        public string SingleLogoutServiceUrl { get; set; } = "Saml2/SingleLogoutService";

        public string SingleLogoutResponseServiceUrl { get; set; } = "Saml2/SingleLogoutService";

        public Certificate Certificate { get; set; }
    }
}
