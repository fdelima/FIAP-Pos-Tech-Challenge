using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FIAP.Pos.Tech.Challenge.Domain.Models;

namespace FIAP.Pos.Tech.Challenge.Api.Controllers
{
    //TODO: Controller :: 1 - Duplicar esta controller de exemplo e trocar o nome da entidade.
    /// <summary>
    /// Controller dos clientes cadastrados
    /// </summary>
    [Route("api/[Controller]")]
    public class ClientController : ApiController
    {
        private readonly IAppService<Client> _service;

        /// <summary>
        /// Construtor do controller dos clientes cadastrados
        /// </summary>
        public ClientController(IAppService<Client> service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna os clientes cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Client>> Get(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Client> param = new PagingQueryParam<Client>() { CurrentPage = currentPage, Take = take };
            return await _service.GetItemsAsync(param, param.SortProp());
        }

        /// <summary>
        /// Recupera o cliente cadastrado pelo seu Id
        /// </summary>
        /// <returns>Cliente encontrada</returns>
        /// <response code="200">Cliente encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Cliente cadastrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindById(Guid id)
        {
            return ExecuteCommand(await _service.FindByIdAsync(id));
        }

        /// <summary>
        ///  Consulta os clientes cadastrados no sistema com o filtro informado.
        /// </summary>
        /// <param name="filter">Filtros para a consulta dos clientes</param>
        /// <returns>Retorna as clientes cadastrados a partir dos parametros informados</returns>
        /// <response code="200">Listagem dos clientes recuperada com sucesso</response>
        /// <response code="400">Erro ao recuperar listagem dos clientes cadastrados</response>
        [HttpPost("consult")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Client>> Consult(PagingQueryParam<Client> param)
        {
            return await _service.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        /// Inseri o cliente cadastrado.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do cliente cadastrado.</returns>
        /// <response code="200">Cliente inserida com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do cliente.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(Client model)
        {
            return ExecuteCommand(await _service.PostAsync(model));
        }

        /// <summary>
        /// Altera o cliente cadastrado.
        /// </summary>
        /// <param name="id">Identificador do cliente cadastrado.</param>
        /// <param name="model">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do cliente cadastrado.</returns>
        /// <response code="200">Cliente alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do cliente.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, Client model)
        {
            return ExecuteCommand(await _service.PutAsync(id, model));
        }

        /// <summary>
        /// Deleta o cliente cadastrado.
        /// </summary>
        /// <param name="id">Identificador do cliente cadastrado.</param>
        /// <returns>Retorna o result do cliente cadastrado.</returns>
        /// <response code="200">Cliente deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do cliente.</response>
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