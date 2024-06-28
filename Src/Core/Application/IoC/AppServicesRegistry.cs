using FIAP.Pos.Tech.Challenge.Application.Services;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.Application.IoC
{
    internal static class AppServicesRegistry
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IAppService<Domain.Entities.Cliente>), typeof(ClienteAppService));
            services.AddScoped(typeof(IAppService<Domain.Entities.Dispositivo>), typeof(DispositivoAppService));
            services.AddScoped(typeof(IAppService<Domain.Entities.Notificacao>), typeof(NotificacaoAppService));
            services.AddScoped(typeof(IPedidoAppService), typeof(PedidoAppService));
            services.AddScoped(typeof(IProdutoAppService), typeof(ProdutoAppService));
        }
    }
}