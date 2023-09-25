using HashMechanism;
using IAMRefactor.Application.Interface;
using IAMRefactor.Application.ViewModel;
using IAMRefactor.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.Service.SignatureBussiness
{
    public class SignatureService : ISignatureService
    {
        private readonly IGenericRepository<SignHashRequest> signHashRequest;

        public SignatureService(IGenericRepository<SignHashRequest> _signHashRequest)
        {
            signHashRequest = _signHashRequest;
        }
        public async Task<string> Sign(SignModel signModel)
        {

            if (signModel == null && string.IsNullOrEmpty(signModel.PdfFile))
            {
                throw new ArgumentNullException(nameof(signModel));

            }
            var URL = signModel.SignHashUrl;
            Signature sig = null;
            var signatureHash = PreperSignHash(signModel,out sig);
            var signRequest= PreperSignObject(signModel,  signatureHash);
            var result = await signHashRequest.Post(signRequest, URL);
            if (result == null)
            {
                throw new ArgumentNullException(nameof(signModel));
            }
            byte[] inArrayBytes = result;
            EmbedSignature emHash = new EmbedSignature();
            var signByte = emHash.EmbedHash(signatureHash.ReturnBytes, inArrayBytes, sig);
            //before LTV PDF
            var beforeLTV = signByte.SignedBytes;
            if (!string.IsNullOrEmpty(signModel.TSAurl))
            {
                var pades = emHash.AddLtv(signByte.SignedBytes, 80000, "SHA256", signModel.TSAurl, null, null, sig);
                return Convert.ToBase64String(pades.PadesSignedBytes);
            }
            return Convert.ToBase64String(beforeLTV);



        }

        private ExtractResponse PreperSignHash(SignModel signModel ,out Signature sig)
        {

            byte[] backImage = null;

            if (!string.IsNullOrEmpty(signModel.BackImage))
            {
                backImage = Convert.FromBase64String(signModel.BackImage);
            }
            byte[] originalBytes = Convert.FromBase64String(signModel.PdfFile);
         
            ExtractHash hashEx = new ExtractHash();
            SignatureAppearance app1 = new SignatureAppearance(true, new rectangle(signModel.RectangleX1, signModel.RectangleY1, signModel.RectangleX2, signModel.RectangleY2), signModel.PageNumber, backImage);

             sig = new Signature("firstSignature", false, app1);

            sig.setSignedBy(signModel.PersonFirstName, signModel.PersonSecondName);
            sig.Reason = signModel.Reason;
            sig.RTL = signModel.RTL;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ExtractResponse signatureHash = hashEx.AddEmptySigAndCalculateHash(originalBytes, sig, 80000);

            return signatureHash;
        }

        private SignHashRequest PreperSignObject(SignModel signModel, ExtractResponse signatureHash)
        {
 
            var file = Convert.FromBase64String(signatureHash.ReturnString);
            SignHashRequest signRequest = new SignHashRequest
            {
                AdssURL = signModel.SignHashRequestAdssUrl,
                RequestIdentifier = Guid.NewGuid(),
                ClientID = signModel.ClientID,
                MutualAuthentication = new MutualAuthenticationRequest
                {
                    EnableMutualAuth = signModel.EnableMutualAuth,
                    MutualAuthCertSerial = signModel.MutualAuthCertSerial
                },
                RequestMode = 3,
                FileBytes = file,
                SigningProfile = signModel.SigningProfile,
                Alias = signModel.Alias,
                PrivateKeyPassword = signModel.PrivateKeyPassword
            };

            return signRequest;
        }
    }
}
