using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.Pos.Tech.Challenge.Api.Controllers
{
    //TODO: Controller :: 1 - Duplicar esta controller de exemplo e trocar o nome da entidade.
    /// <summary>
    /// Controller das Notificações cadastrados
    /// </summary>
    [Route("api/[Controller]")]
    public class NotificacaoController : ApiController
    {
        private readonly IAppService<Notificacao> _service;

        /// <summary>
        /// Construtor do controller das Notificações cadastrados
        /// </summary>
        public NotificacaoController(IAppService<Notificacao> service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna as Notificações cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Notificacao>> Get(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Notificacao> param = new PagingQueryParam<Notificacao>() { CurrentPage = currentPage, Take = take };
            return await _service.GetItemsAsync(param, param.SortProp());
        }

        /// <summary>
        /// Recupera a Notificacao cadastrado pelo seu Id
        /// </summary>
        /// <returns>Notificacao encontrada</returns>
        /// <response code="200">Notificacao encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Notificacao cadastrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindById(Guid id)
        {
            return ExecuteCommand(await _service.FindByIdAsync(id));
        }

        /// <summary>
        ///  Consulta as Notificações cadastrados no sistema com o filtro informado.
        /// </summary>
        /// <param name="filter">Filtros para a consulta das Notificações</param>
        /// <returns>Retorna as Notificações cadastrados a partir dos parametros informados</returns>
        /// <response code="200">Listagem das Notificações recuperada com sucesso</response>
        /// <response code="400">Erro ao recuperar listagem das Notificações cadastrados</response>
        [HttpPost("consult")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Notificacao>> Consult(PagingQueryParam<Notificacao> param)
        {
            return await _service.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        /// Inseri o Notificacao cadastrado.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do Notificacao cadastrado.</returns>
        /// <response code="200">Notificacao inserida com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do Notificacao.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(Notificacao model)
        {
            return ExecuteCommand(await _service.PostAsync(model));
        }

        /// <summary>
        /// Altera o Notificacao cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Notificacao cadastrado.</param>
        /// <param name="model">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do Notificacao cadastrado.</returns>
        /// <response code="200">Notificacao alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do Notificacao.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, Notificacao model)
        {
            return ExecuteCommand(await _service.PutAsync(id, model));
        }

        /// <summary>
        /// Deleta o Notificacao cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Notificacao cadastrado.</param>
        /// <returns>Retorna o result do Notificacao cadastrado.</returns>
        /// <response code="200">Notificacao deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Notificacao.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return ExecuteCommand(await _service.DeleteAsync(id));
        }

    }
}