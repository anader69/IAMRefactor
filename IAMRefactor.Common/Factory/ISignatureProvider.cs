using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Common.Factory
{
    public interface ISignatureProvider
    {
        string SignatureUri { get; }

        byte[] SignData(AsymmetricAlgorithm key, byte[] data);

        bool VerifySignature(AsymmetricAlgorithm key, byte[] data, byte[] signature);
    }
}
