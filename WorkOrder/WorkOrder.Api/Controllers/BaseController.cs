using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using WorkOrder.Infra.Transactions;
using WorkOrder.Shared.Commands.Interfaces;

namespace WorkOrder.Api.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        ///     Unity Of Work
        /// </summary>
        protected readonly IUnitOfWork Uow;


        /// <inheritdoc />
        protected BaseController(IUnitOfWork uow)
        {
            Uow = uow;
        }

        protected Guid LoggedUser => new Guid(User?.Identity?.Name);

        /// <summary>
        ///     Retorno para consultas
        /// </summary>
        /// <param name="result">IActionResult</param>
        /// <returns> OK  - 200 </returns>
        protected async Task<IActionResult> Result(object result)
        {
            return Ok(result);
        }

        protected async Task<IActionResult> Error(string error)
        {
            return StatusCode(500, error);
        }

        protected async Task<IActionResult> Response(object result)
        {
            return Ok(result);
        }

        protected async Task<IActionResult> Response(ICommandResult result)
        {
            if (result.Success)
                try
                {
                    Commit();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    var list = new List<Notification> { new Notification("SQL", "Erro ao salvar informações") };
                    return BadRequest(new
                    {
                        Success = false,
                        Data = list
                    });
                }

            return BadRequest(result);
        }

        protected async Task<IActionResult> Response(object result, IEnumerable<Notification> notifications)
        {
            if (!notifications.Any())
                try
                {
                    Commit();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    var list = new List<Notification> { new Notification("SQL", "Erro ao salvar informações") };
                    return BadRequest(new
                    {
                        Success = false,
                        Data = list
                    });
                }

            return BadRequest(notifications);
        }

        protected async Task<IActionResult> Response(bool isValid)
        {
            try
            {
                if (isValid)
                {
                    Commit();

                    return Ok(new { Success = true });
                }

                return Ok(new { Success = false });
            }
            catch
            {
                var list = new List<Notification> { new Notification("SQL", "Erro ao salvar informações") };
                return BadRequest(new
                {
                    Success = false,
                    Data = list
                });
            }
        }


        private void Commit()
        {
            Uow.Commit();
        }
    }
}
