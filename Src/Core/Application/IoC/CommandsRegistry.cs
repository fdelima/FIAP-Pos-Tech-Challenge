﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Notificacao.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Notificacao.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.Pedido.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.PedidoItem.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.UseCases.PedidoItem.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Application.IoC
{
    internal static class CommandsRegistry
    {
        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //Notificacao
            services.AddScoped<IRequestHandler<NotificacaoPostCommand, ModelResult>, NotificacaoPostHandler>();
            services.AddScoped<IRequestHandler<NotificacaoPutCommand, ModelResult>, NotificacaoPutHandler>();
            services.AddScoped<IRequestHandler<NotificacaoDeleteCommand, ModelResult>, NotificacaoDeleteHandler>();
            services.AddScoped<IRequestHandler<NotificacaoFindByIdCommand, ModelResult>, NotificacaoFindByIdHandler>();
            services.AddScoped<IRequestHandler<NotificacaoGetItemsCommand, PagingQueryResult<Notificacao>>, NotificacaoGetItemsHandler>();

            //Pedido
            services.AddScoped<IRequestHandler<PedidoPostCommand, ModelResult>, PedidoPostHandler>();
            services.AddScoped<IRequestHandler<PedidoPutCommand, ModelResult>, PedidoPutHandler>();
            services.AddScoped<IRequestHandler<PedidoDeleteCommand, ModelResult>, PedidoDeleteHandler>();
            services.AddScoped<IRequestHandler<PedidoFindByIdCommand, ModelResult>, PedidoFindByIdHandler>();
            services.AddScoped<IRequestHandler<PedidoGetItemsCommand, PagingQueryResult<Domain.Entities.Pedido>>, PedidoGetItemsHandler>();
            services.AddScoped<IRequestHandler<PedidoGetListaCommand, PagingQueryResult<Domain.Entities.Pedido>>, PedidoGetIListaHandler>();

            //Pedido Item
            services.AddScoped<IRequestHandler<PedidoItemPostCommand, ModelResult>, PedidoItemPostHandler>();
            services.AddScoped<IRequestHandler<PedidoItemPutCommand, ModelResult>, PedidoItemPutHandler>();
            services.AddScoped<IRequestHandler<PedidoItemDeleteCommand, ModelResult>, PedidoItemDeleteHandler>();
            services.AddScoped<IRequestHandler<PedidoItemFindByIdCommand, ModelResult>, PedidoItemFindByIdHandler>();
            services.AddScoped<IRequestHandler<PedidoItemGetItemsCommand, PagingQueryResult<PedidoItem>>, PedidoItemGetItemsHandler>();

        }
    }
}
