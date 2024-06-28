using FIAP.Pos.Tech.Challenge.Application.Commands.Cliente;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.CommandHandlers.Cliente
{
    internal class ClientePutHandler : IRequestHandler<ClientePutCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Cliente> _service;

        public ClientePutHandler(IService<Domain.Entities.Cliente> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ClientePutCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.UpdateAsync(command.Entity, command.BusinessRules);
        }
    }
}
