using FIAP.Pos.Tech.Challenge.Application.Commands.Client;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Client
{
    public class ClientFindByIdHandler : IRequestHandler<ClientFindByIdCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Client> _service;

        public ClientFindByIdHandler(IService<Domain.Entities.Client> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ClientFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
