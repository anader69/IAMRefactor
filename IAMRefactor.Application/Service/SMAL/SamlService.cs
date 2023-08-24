using IAMRefactor.Application.Interface;
using IAMRefactor.Application.ViewModel;
using IAMRefactor.Common.Enums;
using IAMRefactor.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace IAMRefactor.Application.Service.SMAL
{
    public class SamlService:ISamlService
    {
        public IConfigurationProvider _configurationProvider { get; }
        public ISignatureProviderFactory _signatureProviderFactory { get; }

        public SamlService(IConfigurationProvider configurationProvider, ISignatureProviderFactory signatureProviderFactory)
        {
            _configurationProvider = configurationProvider;
            _signatureProviderFactory = signatureProviderFactory;
        }

        public string GetAuthnRequestUrl(Saml2Configuration config)
        {
            //var saml20AuthnRequest = CreateAuthnRequestURL(config);
            //var authnRequestUrl = BuildAuthnRequestUrl(config, saml20AuthnRequest);
            // return authnRequestUrl;
            return "";
        }

        //private Saml2AuthnRequest CreateAuthnRequestURL(Saml2Configuration configuration)
        //{
        //    var issueInstant = SamlHelper.GetIssueInstant();
        //    var identityProviderConfiguration = configuration.IdentityProviderConfiguration[0];
        //    var request = new Saml2AuthnRequest
        //    {
        //        ID = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())),
        //        Issuer = configuration.ServiceProviderConfiguration.EntityId,
        //        Destination = identityProviderConfiguration.SingleSignOnService,
        //        IssuerFormat = identityProviderConfiguration.IssuerFormat,
        //        IssueInstant = issueInstant,

        //    };
        //    //request.Request.AssertionConsumerServiceURL = identityProviderConfiguration.AssertionConsumerServiceUrl;

        //    if (identityProviderConfiguration.AllowCreate.HasValue &&
        //        identityProviderConfiguration.NameIdPolicyFormat.IsNotNullOrEmpty())
        //    {
        //        //request.Request.NameIDPolicy = new NameIDPolicy
        //        //{
        //        //    AllowCreate = identityProviderConfiguration.AllowCreate,
        //        //    Format = identityProviderConfiguration.NameIdPolicyFormat
        //        //};
        //    }

        //    if (identityProviderConfiguration.AuthnContextComparisonType.IsNotNullOrEmpty())
        //    {
        //        //request.Request.RequestedAuthnContext = new RequestedAuthnContext
        //        //{
        //        //    Comparison = Enum.Parse<AuthnContextComparisonType>(identityProviderConfiguration.AuthnContextComparisonType),
        //        //    ComparisonSpecified = true,
        //        //    Items = identityProviderConfiguration.AuthnContextComparisonItems,
        //        //    ItemsElementName = new ItemsChoiceType7[1]
        //        //};
        //    }

        //    return request;
        //}

        //private string BuildAuthnRequestUrl(Saml2Configuration providerName, Saml2AuthnRequest saml2AuthnRequest)
        //{
        //    var request = saml2AuthnRequest.GetXml().OuterXml;
        //    return BuildRequestUrl(providerName, request, saml2AuthnRequest.Destination);
        //}




        private string BuildRequestUrl(Saml2Configuration providerName, string message, string destination)
        {
            var result = new StringBuilder();
            result.AddMessageParameter(message, null);
            result.AddRelayState(message, null);
            AddSignature(providerName, result);
            return $"{destination}?{result}";
        }

        private void AddSignature(Saml2Configuration providerName, StringBuilder result)
        {
            var signingCertificate = _configurationProvider.ServiceProviderSigningCertificate(providerName);

            var signingKey = signingCertificate.PrivateKey;

            // Check if the key is of a supported type. [SAMLBind] sect. 3.4.4.1 specifies this.
            if (!(signingKey is RSA || signingKey is DSA || signingKey == null))
            {
                throw new ArgumentException("Signing key must be an instance of either RSA or DSA.");
            }

            var hashingAlgorithm = providerName.IdentityProviderConfiguration[0].HashingAlgorithm;
            if (signingKey == null)
            {
                return;
            }

            result.Append($"&{HttpRedirectBindingConstants.SigAlg}=");

            var shaHashingAlgorithm = _signatureProviderFactory.ValidateShaHashingAlgorithm(hashingAlgorithm);
            var signingProvider = _signatureProviderFactory.CreateFromAlgorithmName(signingKey.GetType(), shaHashingAlgorithm);

            var urlEncoded = signingProvider.SignatureUri.UrlEncode();
            result.Append(urlEncoded.UpperCaseUrlEncode());

            // Calculate the signature of the URL as described in [SAMLBind] section 3.4.4.1.
            var signature = signingProvider.SignData(signingKey, Encoding.UTF8.GetBytes(result.ToString()));

            result.AppendFormat("&{0}=", HttpRedirectBindingConstants.Signature);
            result.Append(HttpUtility.UrlEncode(Convert.ToBase64String(signature)));
        }






    }
}
