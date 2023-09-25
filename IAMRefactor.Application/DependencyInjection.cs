using IAMRefactor.Application.Service.ALM;
using IAMRefactor.Application.Service.SignatureBussiness;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {

            service.AddScoped<ISignatureService, SignatureService>();
            service.AddScoped<IALMService, ALMService>();
            //builder.Services.AddScoped<ISamlService, SamlService>();
            return service;
        }
      }
    }
