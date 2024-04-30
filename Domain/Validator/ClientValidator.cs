using FIAP.Pos.Tech.Challenge.Domain.Messages;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class ClientValidator : AbstractValidator<Client>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public ClientValidator()
        {
            RuleFor(c => c.IdClient).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Name).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.LastName).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
