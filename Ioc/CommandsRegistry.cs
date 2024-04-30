using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Client;
using FIAP.Pos.Tech.Challenge.Application.Commands.Client;

namespace FIAP.Pos.Tech.Challenge.IoC
{
    internal static class CommandsRegistry
    {
        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //Client
            services.AddScoped<IRequestHandler<ClientPostCommand, ModelResult>, ClientPostHandler>();
            services.AddScoped<IRequestHandler<ClientPutCommand, ModelResult>, ClientPutHandler>();
            services.AddScoped<IRequestHandler<ClientDeleteCommand, ModelResult>, ClientDeleteHandler>();
            services.AddScoped<IRequestHandler<ClientFindByIdCommand, ModelResult>, ClientFindByIdHandler>();
            services.AddScoped<IRequestHandler<ClientGetItemsCommand, PagingQueryResult<Client>>, ClientGetItemsHandler>();
        }
    }
}
