using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class NotificacaoService : BaseService<Notificacao>
    {
        public NotificacaoService(IRepository<Notificacao> repository, IValidator<Notificacao> validator) 
            : base(repository, validator)
        {
        }
    }
}
