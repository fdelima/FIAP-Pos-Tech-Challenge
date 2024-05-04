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
            services.AddScoped(typeof(IService<Domain.Entities.Cliente>), typeof(ClienteService));
            services.AddScoped(typeof(IService<Domain.Entities.Dispositivo>), typeof(DispositivoService));
            services.AddScoped(typeof(IService<Domain.Entities.PedidoItem>), typeof(PedidoItemService));
            services.AddScoped(typeof(IService<Domain.Entities.Pedido>), typeof(PedidoService));
            services.AddScoped(typeof(IService<Domain.Entities.Produto>), typeof(ProdutoService));
        }
    }
}