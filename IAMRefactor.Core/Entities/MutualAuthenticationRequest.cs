using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Core.Entities
{
    public class MutualAuthenticationRequest
    {
        public bool EnableMutualAuth { get; set; }
        public string MutualAuthCertSerial { get; set; }
    }
}
