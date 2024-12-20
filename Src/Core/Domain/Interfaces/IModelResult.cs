﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Models;
using FluentValidation.Results;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Pedido.Domain.Interfaces
{
    /// <summary>
    /// Interface regulamentando as propriedades e métodos necessários a uma entidade
    /// </summary>
    public interface IModelResult
    {
        List<string> Messages { get; }
        List<string> Errors { get; }
        object Model { get; }
        bool IsValid { get; }

        void Add(ModelResult model);
        void AddError(string error);
        void AddError(IReadOnlyList<string> errors);
        void AddMessage(string message);
        void AddMessage(IReadOnlyList<string> messages);
        void AddValidations(ValidationResult validations);
        IReadOnlyList<string> ListErrors();
        IReadOnlyList<string> ListMessages();
    }
}