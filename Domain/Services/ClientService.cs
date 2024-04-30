using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class ClientService : BaseService<Client>
    {
        public ClientService(IRepository<Client> repository, IValidator<Client> validator) 
            : base(repository, validator)
        {
        }
    }
}
