﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.IoC
{
    /// <summary>
    /// Configura a injeção de dependência
    /// </summary>
    public static class NativeInjectorSetup
    {
        /// <summary>
        /// Registra as dependências aos serviços da aplicação
        /// </summary>
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDatabase(configuration);
            services.RegisterRepositories();
            services.RegisterDomainServices();
            services.RegisterAppServices();
            services.RegisterCommands();
            services.RegisterValidators();
        }
    }
}
