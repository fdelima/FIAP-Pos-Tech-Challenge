using FIAP.Pos.Tech.Challenge.Application.Commands.Produto;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Produto
{
    public class ProdutoFindByIdHandler : IRequestHandler<ProdutoFindByIdCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Produto> _service;

        public ProdutoFindByIdHandler(IService<Domain.Entities.Produto> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProdutoFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
