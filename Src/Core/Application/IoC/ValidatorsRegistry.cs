using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.IoC
{
    internal static class ValidatorsRegistry
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            //TODO: Validators :: 3 - Adicione sua configuração aqui

            //Validators
            services.AddScoped(typeof(IValidator<Notificacao>), typeof(NotificacaoValidator));
            services.AddScoped(typeof(IValidator<PedidoItem>), typeof(PedidoItemValidator));
            services.AddScoped(typeof(IValidator<Domain.Entities.Pedido>), typeof(PedidoValidator));
        }
    }
}
