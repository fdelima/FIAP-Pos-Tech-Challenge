using FIAP.Pos.Tech.Challenge.Application.Commands.Client;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Client
{
    public class ClientPostHandler : IRequestHandler<ClientPostCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Client> _service;

        public ClientPostHandler(IService<Domain.Entities.Client> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ClientPostCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.InsertAsync(command.Entity, command.BusinessRules);
        }
    }
}
