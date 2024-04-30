using FIAP.Pos.Tech.Challenge.Application.Services;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.IoC
{
    internal static class AppServicesRegistry
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IAppService<Domain.Entities.Client>), typeof(ClientAppService));
        }
    }
}