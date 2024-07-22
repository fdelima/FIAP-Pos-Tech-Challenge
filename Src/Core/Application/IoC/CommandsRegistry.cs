using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Cliente.Commands;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Commands;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Notificacao.Commands;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Application.UseCases.PedidoItem.Commands;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Cliente.Handlers;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Handlers;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Notificacao.Handlers;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Handlers;
using FIAP.Pos.Tech.Challenge.Application.UseCases.PedidoItem.Handlers;
using FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Handlers;

namespace FIAP.Pos.Tech.Challenge.Application.IoC
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
            services.AddScoped<IRequestHandler<PedidoGetItemsCommand, PagingQueryResult<Pedido>>, PedidoGetItemsHandler>();
            services.AddScoped<IRequestHandler<PedidoIniciarPreparacaCommand, ModelResult>, PedidoIniciarPreparacaHandler>();
            services.AddScoped<IRequestHandler<PedidoFinalizarPreparacaCommand, ModelResult>, PedidoFinalizarPreparacaHandler>();
            services.AddScoped<IRequestHandler<PedidoFinalizarCommand, ModelResult>, PedidoFinalizarHandler>();
            services.AddScoped<IRequestHandler<PedidoGetListaCommand, PagingQueryResult<Pedido>>, PedidoGetIListaHandler>();
            services.AddScoped<IRequestHandler<PedidoWebhookPagamentoCommand, ModelResult>, PedidoWebhookPagamentoHandler>();
            services.AddScoped<IRequestHandler<PedidoConsultarPagamentoCommand, ModelResult>, PedidoConsultarPagamentoHandler>();

            //Pedido Item
            services.AddScoped<IRequestHandler<PedidoItemPostCommand, ModelResult>, PedidoItemPostHandler>();
            services.AddScoped<IRequestHandler<PedidoItemPutCommand, ModelResult>, PedidoItemPutHandler>();
            services.AddScoped<IRequestHandler<PedidoItemDeleteCommand, ModelResult>, PedidoItemDeleteHandler>();
            services.AddScoped<IRequestHandler<PedidoItemFindByIdCommand, ModelResult>, PedidoItemFindByIdHandler>();
            services.AddScoped<IRequestHandler<PedidoItemGetItemsCommand, PagingQueryResult<PedidoItem>>, PedidoItemGetItemsHandler>();

            //Produto
            services.AddScoped<IRequestHandler<ProdutoPostCommand, ModelResult>, ProdutoPostHandler>();
            services.AddScoped<IRequestHandler<ProdutoPutCommand, ModelResult>, ProdutoPutHandler>();
            services.AddScoped<IRequestHandler<ProdutoDeleteCommand, ModelResult>, ProdutoDeleteHandler>();
            services.AddScoped<IRequestHandler<ProdutoFindByIdCommand, ModelResult>, ProdutoFindByIdHandler>();
            services.AddScoped<IRequestHandler<ProdutoGetItemsCommand, PagingQueryResult<Produto>>, ProdutoGetItemsHandler>();
            services.AddScoped<IRequestHandler<ProdutoGetCategoriasCommand, PagingQueryResult<KeyValuePair<short, string>>>, ProdutoGetCategoriasHandler>();
        }
    }
}
