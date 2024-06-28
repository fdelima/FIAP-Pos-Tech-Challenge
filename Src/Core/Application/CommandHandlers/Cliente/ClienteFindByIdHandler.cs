using FIAP.Pos.Tech.Challenge.Application.Commands.Cliente;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Cliente
{
    internal class ClienteFindByIdHandler : IRequestHandler<ClienteFindByIdCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Cliente> _service;

        public ClienteFindByIdHandler(IService<Domain.Entities.Cliente> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ClienteFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
