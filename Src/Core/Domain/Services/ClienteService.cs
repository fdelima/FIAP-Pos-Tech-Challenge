using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class ClienteService : BaseService<Cliente>
    {
        public ClienteService(IRepository<Cliente> repository, IValidator<Cliente> validator)
            : base(repository, validator)
        {
        }
    }
}
