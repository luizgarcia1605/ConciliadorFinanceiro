using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConciliadorFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalancoController : ControllerBase
    {
        private readonly ILancamentoFinanceiroBusiness _businessLancamento;

        #region Construtores

        public BalancoController(ILancamentoFinanceiroBusiness businessLancamento)
        {
            _businessLancamento = businessLancamento;
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<ActionResult<LancamentoFinanceiro>> Cadastrar(LancamentoFinanceiro lancamentoFinanceiro)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ValidationProblem();

                await _businessLancamento.Cadastrar(lancamentoFinanceiro);

                return Ok(lancamentoFinanceiro);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        #endregion
    }
}