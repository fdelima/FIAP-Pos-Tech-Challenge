using FIAP.Pos.Tech.Challenge.Application.Commands.Client;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Client
{
    public class ClientDeleteHandler : IRequestHandler<ClientDeleteCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Client> _service;

        public ClientDeleteHandler(IService<Domain.Entities.Client> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ClientDeleteCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.DeleteAsync(command.Id, command.BusinessRules);
        }
    }
}
