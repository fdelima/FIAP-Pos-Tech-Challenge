using FIAP.Pos.Tech.Challenge.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.Application.IoC
{
    internal static class AppServicesRegistry
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IController<Domain.Entities.Cliente>), typeof(ClienteAppService));
            services.AddScoped(typeof(IController<Domain.Entities.Dispositivo>), typeof(DispositivoAppService));
            services.AddScoped(typeof(IController<Domain.Entities.Notificacao>), typeof(NotificacaoAppService));
            services.AddScoped(typeof(IPedidoAppService), typeof(PedidoAppService));
            services.AddScoped(typeof(IProdutoAppService), typeof(ProdutoAppService));
        }
    }
}