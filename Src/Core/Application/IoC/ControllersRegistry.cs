using FIAP.Pos.Tech.Challenge.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.Application.IoC
{
    internal static class ControllersRegistry
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IController<Domain.Entities.Cliente>), typeof(ClienteController));
            services.AddScoped(typeof(IController<Domain.Entities.Dispositivo>), typeof(DispositivoController));
            services.AddScoped(typeof(IController<Domain.Entities.Notificacao>), typeof(NotificacaoController));
            services.AddScoped(typeof(IPedidoController), typeof(PedidoController));
            services.AddScoped(typeof(IProdutoController), typeof(ProdutoController));
        }
    }
}