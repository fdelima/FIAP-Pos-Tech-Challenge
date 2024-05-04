using FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Cliente;
using FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Dispositivo;
using FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Pedido;
using FIAP.Pos.Tech.Challenge.Application.CommandHandlers.PedidoItem;
using FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Produto;
using FIAP.Pos.Tech.Challenge.Application.Commands.Cliente;
using FIAP.Pos.Tech.Challenge.Application.Commands.Dispositivo;
using FIAP.Pos.Tech.Challenge.Application.Commands.Pedido;
using FIAP.Pos.Tech.Challenge.Application.Commands.PedidoItem;
using FIAP.Pos.Tech.Challenge.Application.Commands.Produto;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FIAP.Pos.Tech.Challenge.IoC
{
    internal static class CommandsRegistry
    {
        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //Cliente
            services.AddScoped<IRequestHandler<ClientePostCommand, ModelResult>, ClientePostHandler>();
            services.AddScoped<IRequestHandler<ClientePutCommand, ModelResult>, ClientePutHandler>();
            services.AddScoped<IRequestHandler<ClienteDeleteCommand, ModelResult>, ClienteDeleteHandler>();
            services.AddScoped<IRequestHandler<ClienteFindByIdCommand, ModelResult>, ClienteFindByIdHandler>();
            services.AddScoped<IRequestHandler<ClienteGetItemsCommand, PagingQueryResult<Cliente>>, ClienteGetItemsHandler>();

            //Dispositivo
            services.AddScoped<IRequestHandler<DispositivoPostCommand, ModelResult>, DispositivoPostHandler>();
            services.AddScoped<IRequestHandler<DispositivoPutCommand, ModelResult>, DispositivoPutHandler>();
            services.AddScoped<IRequestHandler<DispositivoDeleteCommand, ModelResult>, DispositivoDeleteHandler>();
            services.AddScoped<IRequestHandler<DispositivoFindByIdCommand, ModelResult>, DispositivoFindByIdHandler>();
            services.AddScoped<IRequestHandler<DispositivoGetItemsCommand, PagingQueryResult<Dispositivo>>, DispositivoGetItemsHandler>();

            //Pedido
            services.AddScoped<IRequestHandler<PedidoPostCommand, ModelResult>, PedidoPostHandler>();
            services.AddScoped<IRequestHandler<PedidoPutCommand, ModelResult>, PedidoPutHandler>();
            services.AddScoped<IRequestHandler<PedidoDeleteCommand, ModelResult>, PedidoDeleteHandler>();
            services.AddScoped<IRequestHandler<PedidoFindByIdCommand, ModelResult>, PedidoFindByIdHandler>();
            services.AddScoped<IRequestHandler<PedidoGetItemsCommand, PagingQueryResult<Pedido>>, PedidoGetItemsHandler>();

            //Client
            services.AddScoped<IRequestHandler<PedidoItemPostCommand, ModelResult>, PedidoItemPostHandler>();
            services.AddScoped<IRequestHandler<PedidoItemPutCommand, ModelResult>, PedidoItemPutHandler>();
            services.AddScoped<IRequestHandler<PedidoItemDeleteCommand, ModelResult>, PedidoItemDeleteHandler>();
            services.AddScoped<IRequestHandler<PedidoItemFindByIdCommand, ModelResult>, PedidoItemFindByIdHandler>();
            services.AddScoped<IRequestHandler<PedidoItemGetItemsCommand, PagingQueryResult<PedidoItem>>, PedidoItemGetItemsHandler>();

            //Client
            services.AddScoped<IRequestHandler<ProdutoPostCommand, ModelResult>, ProdutoPostHandler>();
            services.AddScoped<IRequestHandler<ProdutoPutCommand, ModelResult>, ProdutoPutHandler>();
            services.AddScoped<IRequestHandler<ProdutoDeleteCommand, ModelResult>, ProdutoDeleteHandler>();
            services.AddScoped<IRequestHandler<ProdutoFindByIdCommand, ModelResult>, ProdutoFindByIdHandler>();
            services.AddScoped<IRequestHandler<ProdutoGetItemsCommand, PagingQueryResult<Produto>>, ProdutoGetItemsHandler>();
        }
    }
}
