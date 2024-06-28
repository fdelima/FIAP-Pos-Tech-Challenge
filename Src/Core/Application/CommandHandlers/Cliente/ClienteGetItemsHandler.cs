using FIAP.Pos.Tech.Challenge.Application.Commands.Cliente;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Cliente
{
    internal class ClienteGetItemsHandler : IRequestHandler<ClienteGetItemsCommand, PagingQueryResult<Domain.Entities.Cliente>>
    {
        private readonly IService<Domain.Entities.Cliente> _service;

        public ClienteGetItemsHandler(IService<Domain.Entities.Cliente> service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Cliente>> Handle(ClienteGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
