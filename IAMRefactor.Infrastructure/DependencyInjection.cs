using IAMRefactor.Application.Interface;
using IAMRefactor.Infrastructure.implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service ) {
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddScoped<IAMRefactor.Application.Interface.IConfigurationProvider, IAMRefactor.Infrastructure.implementation.ConfigurationProvider>();
            //builder.Services.AddScoped<ISignatureProviderFactory, SignatureProviderFactory>();
            return service;



        }
    }
}
