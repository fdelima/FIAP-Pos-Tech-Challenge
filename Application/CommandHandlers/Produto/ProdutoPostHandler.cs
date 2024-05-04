using FIAP.Pos.Tech.Challenge.Application.Commands.Produto;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Produto
{
    public class ProdutoPostHandler : IRequestHandler<ProdutoPostCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Produto> _service;

        public ProdutoPostHandler(IService<Domain.Entities.Produto> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProdutoPostCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.InsertAsync(command.Entity, command.BusinessRules);
        }
    }
}
