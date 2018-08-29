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
    [Route("v1/custom-forms-answer")]
    public class CustomFormsAnswerController : BaseController
    {

        private readonly CustomFormsHandler _handler;

        private readonly ICustomFormAnswerRepository _customFormAnswerRepository;

        /// <inheritdoc />
        public CustomFormsAnswerController(IUnitOfWork uow, CustomFormsHandler handler, ICustomFormAnswerRepository customFormAnswerRepository) : base(uow)
        {
            _handler = handler;
            _customFormAnswerRepository = customFormAnswerRepository;
        }

        /// <summary>
        /// UC : Envia resposta do formulário dinamico
        /// </summary>
        /// <param name="request"> Objeto para enviar resposta do formulário dinâmico </param>
        /// <returns> Retorna resultado da requisição </returns>
        [HttpPost(""), AllowAnonymous]
        public async Task<IActionResult> Save([FromBody]SendCustomFormRequest request)
        {
            return await Response(_handler.Handle(request));
        }

        /// <summary>
        /// UC : Recuperar resposta por id do formulário dinamico
        /// </summary>
        /// <param name="id">Id do formulário dinamico </param>
        /// <returns> Retorna lista de respostas por id do formulario dinamico</returns>
        [HttpGet("{id}/by-custom-form")]
        public async Task<IActionResult> GetByCustomForm(Guid id)
        {
            return await Result(null);
        }

        /// <summary>
        /// UC : Recuperar detalhes da resposta por id 
        /// </summary>
        /// <param name="id">Id da respsota do formilario </param>
        /// <returns> Retorna detalhes da resposta por id </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return await Result(null);
        }
    }
}
