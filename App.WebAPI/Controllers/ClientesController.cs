﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application;
using Domain.SharedKernel.Queries;
using Domain.Models;
using Application.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;
using Application.Input;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.WebAPI.Controllers
{
    /// <summary>
    /// Controller para operações de clientes
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class ClientesController : Controller
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
        [ProducesResponseType(typeof(IList<Cliente>), 200)]
        public IActionResult Get(int pagina, int porPagina)
        {
            return Ok(_appService.ListarTodos(pagina, porPagina));
        }

        /// <summary>
        /// Obtém um cliente informando o ID
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <returns><see cref="Cliente"/></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            return Ok(_appService.Obter(id));
        }

        /// <summary>
        /// Insere um novo cliente
        /// </summary>
        /// <param name="cliente"><see cref="ClienteInput"/></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] ClienteInput cliente)
        {
            _appService.Adicionar(cliente);
            return Ok();
        }

        /// <summary>
        /// Altera informações de um cliente
        /// </summary>
        /// <param name="id">Identificação do cliente</param>
        /// <param name="cliente"><see cref="ClienteInput"/></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody]ClienteInput cliente)
        {
            _appService.Atualizar(id, cliente);
            return Ok();
        }

        /// <summary>
        /// Exclui um Cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            _appService.Excluir(id);
            return Ok();
        }
    }
}