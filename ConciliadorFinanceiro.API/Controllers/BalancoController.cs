using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesBusiness;
using Microsoft.AspNetCore.Mvc;

namespace ConciliadorFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalancoController : ControllerBase
    {
        private readonly ILancamentoFinanceiroBusiness _businessLancamento;
        private readonly IBalancoBusiness _businessBalanco;

        #region Construtores

        public BalancoController(ILancamentoFinanceiroBusiness businessLancamento,
                                    IBalancoBusiness businessBalanco)
        {
            _businessLancamento = businessLancamento;
            _businessBalanco = businessBalanco;
        }

        #endregion

        #region Get

        [HttpGet("BalancoDiario")]
        public async Task<ActionResult<Balanco>> BalancoDiario(DateTime data)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ValidationProblem();

                var lancamentosDia = await _businessLancamento.ConsultarLista
                    (new List<string> { $"CAST(DataHoraLancamento AS DATE) = '{data:yyyy-MM-dd}'" });

                var balancoDia = await _businessBalanco.GerarBalancoDiario(lancamentosDia);

                return Ok(balancoDia);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("BalancoPeriodo")]
        public async Task<ActionResult<Balanco>> BalancoPeriodo(DateTime datainicio, DateTime datafinal)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ValidationProblem();

                var lancamentosDia = await _businessLancamento.ConsultarLista
                    (new List<string> { $"CAST(DataHoraLancamento AS DATE) BETWEEN '{datainicio:yyyy-MM-dd}' AND '{datafinal:yyyy-MM-dd}'" });

                var balancoDia = await _businessBalanco.GerarBalancoPeriodo(lancamentosDia);

                return Ok(balancoDia);
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