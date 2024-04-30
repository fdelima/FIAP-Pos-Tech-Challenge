using FIAP.Pos.Tech.Challenge.Application.Commands.Client;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Client
{
    public class ClientGetItemsHandler : IRequestHandler<ClientGetItemsCommand, PagingQueryResult<Domain.Entities.Client>>
    {
        private readonly IService<Domain.Entities.Client> _service;

        public ClientGetItemsHandler(IService<Domain.Entities.Client> service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Client>> Handle(ClientGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
