using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.IoC
{
    internal static class DomainServicesRegistry
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IService<Domain.Entities.Client>), typeof(ClientService));
        }
    }
}