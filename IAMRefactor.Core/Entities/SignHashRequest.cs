using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Core.Entities
{
    public class SignHashRequest
    {
        public Guid RequestIdentifier { get; set; }
        public string AdssURL { get; set; }
        public string ClientID { get; set; }
        public string Alias { get; set; }
        public string PrivateKeyPassword { get; set; }
        public byte[] FileBytes { get; set; }
        public string SigningProfile { get; set; }
        public int RequestMode { get; set; }
        public MutualAuthenticationRequest MutualAuthentication { get; set; }
    }
}
