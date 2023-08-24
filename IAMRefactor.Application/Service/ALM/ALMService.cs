using Flurl;
using IAMRefactor.Application.ViewModel;
using IAMRefactor.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IAMRefactor.Application.Service.ALM
{
    public class ALMService : IALMService
    {

        public string GenerateBaseUrlForClient(IAMConfigrationModel model)
        {
          var result= model.IamAuthorizeUrl.SetQueryParams(new
                {
                    scope = "openid",
                    response_type = "id_token",
                    response_mode = "form_post",
                    client_id = model.ClientId
                })
                .SetQueryParam("redirect_uri", model.CallbackUrl, true)
                .SetQueryParams(new
                {
                    nonce = Guid.NewGuid(),
                    ui_locales =  IAMLanguages.English,
                    prompt = "login",
                    max_age = IAMHelper.CurrentTimeSeconds
                })
                .ToString();
          var data=   ToSignedUrl(result,Environment.CurrentDirectory + model.PrivatePfxPath, model.PrivatePfxPassword);
            return data.Url;

        }



        /// <summary>
        /// Generate url with signature using pfx file and password
        /// </summary>
        /// <param name="filePath">Path to the pfx file</param>
        /// <param name="password">Password for the pfx file</param>
        /// <returns>String url for login using IAM</returns>
        /// <exception cref="Exception">
        /// Specified file does not have a private key.
        /// </exception>
        private  (string Url, string State) ToSignedUrl( string baseUrl, string filePath, string password = null)
        {
            //baseUrl = "https://iambeta.elm.sa/authservice/authorize?scope=openid&response_type=id_token&response_mode=form_post&client_id=16371212&redirect_uri=https://stg.majles.tech/iam/auth/authorize/callback&nonce=dd6f78a2-6202-447a-8a91-9b2165cd404a&ui_locales=en&prompt=login&max_age=1631707140";
            //Create signature from base url, using pfx file and its password
            var signature = GetRsaPrivateKeyFromFile(filePath, password)
                                        .SignHash(baseUrl.GetUTF8SHA256Hash(), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1)
                                        .ToBase64String();
            //concat Url and signature as state
            return (Url: $"{baseUrl}&state={HttpUtility.UrlEncode(signature)}", State: signature);
        }



        /// <summary>
        /// Returns the RSA private key from the specified PFX file and password
        /// </summary>
        /// <exception cref="Exception">
        /// Specified file does not have a private key.
        /// </exception>
        private  readonly Func<string, string, RSA> GetRsaPrivateKeyFromFile = (filepath, password) =>
        {
            var certificate = new X509Certificate2(fileName: filepath, password: password);

            if (certificate.HasPrivateKey)
                return certificate.GetRSAPrivateKey();
            else
                throw new Exception("The file provided does not have a private key");
        };



  
    }
}
