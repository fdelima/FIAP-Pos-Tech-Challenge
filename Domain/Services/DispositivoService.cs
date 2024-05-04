using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class DispositivoService : BaseService<Dispositivo>
    {
        public DispositivoService(IRepository<Dispositivo> repository, IValidator<Dispositivo> validator) 
            : base(repository, validator)
        {
        }
    }
}
