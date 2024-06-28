using FIAP.Pos.Tech.Challenge.Application.Commands.Produto;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Produto
{
    internal class ProdutoDeleteHandler : IRequestHandler<ProdutoDeleteCommand, ModelResult>
    {
        private readonly IProdutoService _service;

        public ProdutoDeleteHandler(IProdutoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProdutoDeleteCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.DeleteAsync(command.Id, command.BusinessRules);
        }
    }
}
