using IAMRefactor.Common.Enums;
using IAMRefactor.Common.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.Interface
{
    public interface ISignatureProviderFactory
    {
        ISignatureProvider CreateFromAlgorithmName(Type signingKeyType, ShaHashingAlgorithm hashingAlgorithm);

        ISignatureProvider CreateFromAlgorithmUri(Type signingKeyType, string algorithmUri);

        ShaHashingAlgorithm ValidateShaHashingAlgorithm(string shaHashingAlgorithm);
    }
}
