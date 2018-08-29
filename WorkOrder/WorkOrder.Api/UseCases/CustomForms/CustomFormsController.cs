using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkOrder.Api.Controllers;
using WorkOrder.Application.CustomFormsContext.Handlers;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Application.CustomFormsContext.Requests;
using WorkOrder.Infra.Transactions;

namespace WorkOrder.Api.UseCases.CustomForms
{
    /// <inheritdoc />
    [Route("v1/custom-forms")]
    public class CustomFormsController : BaseController
    {

        private readonly CustomFormsHandler _handler;

        private readonly ICustomFormRepository _customFormRepository;

        /// <inheritdoc />
        public CustomFormsController(IUnitOfWork uow, CustomFormsHandler handler, ICustomFormRepository customFormRepository) : base(uow)
        {
            _handler = handler;
            _customFormRepository = customFormRepository;
        }

        /// <summary>
        /// UC : Cria um novo formulário dinamico
        /// </summary>
        /// <param name="request"> Objeto para criar um novo formulário dinâmico </param>
        /// <returns> Retorna resultado da requisição </returns>
        [HttpPost("")]
        public async Task<IActionResult> Save([FromBody]CreateCustomFormRequest request)
        {
            return await Response(_handler.Handle(request));
        }

        /// <summary>
        /// UC : Requisita todos os formulários cadastrados
        /// </summary>
        /// <returns> Retorna todos os formulários cadastrados </returns>
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return await Result(_customFormRepository.Query());
        }


        /// <summary>
        /// UC : Requisita detalhes do formulário por Id
        /// </summary>
        /// <param name="id">Id do formulário</param>
        /// <returns>Retorna detalhes do formulário por Id</returns>
        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> Get(Guid id)
        {
            return await Result(_customFormRepository.Query(id));
        }

    }
}
