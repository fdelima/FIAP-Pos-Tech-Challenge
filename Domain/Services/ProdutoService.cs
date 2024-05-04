using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Services
{
    public class ProdutoService : BaseService<Produto>
    {
        public ProdutoService(IRepository<Produto> repository, IValidator<Produto> validator) 
            : base(repository, validator)
        {
        }
    }
}
