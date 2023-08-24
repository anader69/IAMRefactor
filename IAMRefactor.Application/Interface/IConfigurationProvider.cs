using IAMRefactor.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.Interface
{
    public interface IConfigurationProvider
    {
        ServiceProviderConfiguration ServiceProviderConfiguration { get; }

        IdentityProviderConfiguration GetIdentityProviderConfiguration(string providerName);

        X509Certificate2 GetIdentityProviderSigningCertificate(string providerName);

        X509Certificate2 ServiceProviderSigningCertificate();
        X509Certificate2 ServiceProviderSigningCertificate(Saml2Configuration config);
    }
}
