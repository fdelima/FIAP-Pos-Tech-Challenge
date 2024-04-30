using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.Commands.Client
{
    public class ClientPutCommand : IRequest<ModelResult>
    {
        public ClientPutCommand(Guid id, Domain.Entities.Client entity,
            string[]? businessRules = null)
        {
            Id = id;
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public Domain.Entities.Client Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}