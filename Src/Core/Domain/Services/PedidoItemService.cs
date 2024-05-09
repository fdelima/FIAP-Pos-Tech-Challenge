using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class PedidoItemService : BaseService<PedidoItem>
    {
        public PedidoItemService(IRepository<PedidoItem> repository, IValidator<PedidoItem> validator)
            : base(repository, validator)
        {
        }
    }
}
