using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Infra.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Infra.IoC
{
    internal static class GatewaysRegistry
    {
        public static void RegisterGateways(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IGateways<>), typeof(BaseGateway<>));
        }
    }
}