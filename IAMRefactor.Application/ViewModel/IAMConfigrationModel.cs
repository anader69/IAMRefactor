using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.ViewModel
{
    public class IAMConfigrationModel
    {
        public string PrivatePfxPath { get; set; }
        public string PrivatePfxSubject { get; set; }
        public string PrivatePfxPassword { get; set; }
        public string CallbackUrl { get; set; }
        public string ClientId { get; set; }
        public string PublicCertificatePath { get; set; }
        public string PublicCertificateSubject { get; set; }
        public string IamAuthorizeUrl { get; set; }
    }
}
