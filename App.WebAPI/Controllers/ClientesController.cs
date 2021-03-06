﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Application.Interfaces;
using Application.Input;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using App.WebAPI.Attributes;

namespace App.WebAPI.Controllers
{
    /// <summary>
    /// Controller para operações de clientes
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class ClientesController : BaseController
    {
        private IClienteApplicationService _appService { get; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="appService">Serviço de aplicação (injetado)</param>
        public ClientesController(IClienteApplicationService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Obtém uma lista de clientes utilizando paginação
        /// </summary>
        /// <param name="pagina">Página atual</param>
        /// <param name="porPagina">Quantidade de itens por página</param>
        /// <returns><see cref="IList{Cliente}"/></returns>
        [HttpGet]
        [ResponseOK(typeof(IList<Cliente>))]
        public async Task<IActionResult> Get(int pagina, int porPagina)
        {
            return Ok(await _appService.ListarTodos(pagina, porPagina));
        }

        /// <summary>
        /// Obtém um cliente informando o ID
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <returns><see cref="Cliente"/></returns>
        [HttpGet]
        [Route("{id}", Name = "GetClienteById")]
        [ResponseOK(typeof(Cliente))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _appService.Obter(id));
        }

        /// <summary>
        /// Insere um novo cliente
        /// </summary>
        /// <param name="cliente"><see cref="ClienteInput"/></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseCreated(typeof(Cliente))]
        [ResponseBadRequest]
        public async Task<IActionResult> Post([FromBody] ClienteInput cliente)
        {
            var c = await _appService.Adicionar(cliente);
            return Created("GetClienteById", c.Id, c);
        }

        /// <summary>
        /// Altera informações de um cliente
        /// </summary>
        /// <param name="id">Identificação do cliente</param>
        /// <param name="cliente"><see cref="ClienteInput"/></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ResponseAccepted]
        [ResponseBadRequest]
        [ResponseNotFound]
        public async Task<IActionResult> Put(int id, [FromBody]ClienteInput cliente)
        {
            await _appService.Atualizar(id, cliente);
            return Accepted();
        }

        /// <summary>
        /// Exclui um Cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ResponseNoContent]
        [ResponseNotFound]
        public async Task<IActionResult> Delete(int id)
        {
            await _appService.Excluir(id);
            return Deleted();
        }
    }
}
