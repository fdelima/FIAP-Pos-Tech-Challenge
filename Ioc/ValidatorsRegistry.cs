using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.IoC
{
    internal static class ValidatorsRegistry
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            //TODO: Validators :: 3 - Adicione sua configuração aqui

            //Validators
            services.AddScoped(typeof(IValidator<Client>), typeof(ClientValidator));
        }
    }
}
