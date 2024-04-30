using FIAP.Pos.Tech.Challenge.Application.Commands.Client;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Client
{
    public class ClientPutHandler : IRequestHandler<ClientPutCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Client> _service;

        public ClientPutHandler(IService<Domain.Entities.Client> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ClientPutCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.UpdateAsync(command.Entity, command.BusinessRules);
        }
    }
}
