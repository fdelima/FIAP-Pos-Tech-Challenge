using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.Commands.Client
{
    public class ClientPostCommand: IRequest<ModelResult>
    {
        public ClientPostCommand(Domain.Entities.Client entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Domain.Entities.Client Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}