using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class PedidoService : BaseService<Pedido>
    {
        public PedidoService(IRepository<Pedido> repository, IValidator<Pedido> validator) 
            : base(repository, validator)
        {
        }
    }
}
