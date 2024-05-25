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
    /// Controller dos Pedidos cadastrados
    /// </summary>
    [Route("api/[Controller]")]
    public class PedidoController : ApiController
    {
        private readonly IPedidoAppService _service;

        /// <summary>
        /// Construtor do controller dos Pedidos cadastrados
        /// </summary>
        public PedidoController(IPedidoAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna os Pedidos cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Pedido>> Get(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Pedido> param = new PagingQueryParam<Pedido>() { CurrentPage = currentPage, Take = take };
            return await _service.GetItemsAsync(param, param.SortProp());
        }

        /// <summary>
        /// Recupera o Pedido cadastrado pelo seu Id
        /// </summary>
        /// <returns>Pedido encontrada</returns>
        /// <response code="200">Pedido encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Pedido cadastrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindById(Guid id)
        {
            return ExecuteCommand(await _service.FindByIdAsync(id));
        }

        /// <summary>
        ///  Consulta os Pedidos cadastrados no sistema com o filtro informado.
        /// </summary>
        /// <param name="filter">Filtros para a consulta dos Pedidos</param>
        /// <returns>Retorna as Pedidos cadastrados a partir dos parametros informados</returns>
        /// <response code="200">Listagem dos Pedidos recuperada com sucesso</response>
        /// <response code="400">Erro ao recuperar listagem dos Pedidos cadastrados</response>
        [HttpPost("consult")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Pedido>> Consult(PagingQueryParam<Pedido> param)
        {
            return await _service.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        /// Inseri o Pedido cadastrado.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido inserida com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do Pedido.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(Pedido model)
        {
            return ExecuteCommand(await _service.PostAsync(model));
        }

        /// <summary>
        /// Altera o Pedido cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <param name="model">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do Pedido.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, Pedido model)
        {
            return ExecuteCommand(await _service.PutAsync(id, model));
        }

        /// <summary>
        /// Deleta o Pedido cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Pedido.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return ExecuteCommand(await _service.DeleteAsync(id));
        }

        /// <summary>
        /// Pedido em preparação.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Pedido.</response>
        [HttpPatch("IniciarPreparacao/{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> IniciarPreparacaoAsync(Guid id)
        {
            return ExecuteCommand(await _service.IniciarPreparacaoAsync(id));
        }

        /// <summary>
        /// Pedido pronto.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Pedido.</response>
        [HttpPatch("FinalizarPreparacao/{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FinalizarPreparacaoAsync(Guid id)
        {
            return ExecuteCommand(await _service.FinalizarPreparacaoAsync(id));
        }

        /// <summary>
        /// Pedido finalizado.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Pedido.</response>
        [HttpPatch("Finalizar/{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FinalizarAsync(Guid id)
        {
            return ExecuteCommand(await _service.FinalizarAsync(id));
        }

    }
}