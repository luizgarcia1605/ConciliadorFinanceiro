using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesBusiness;
using Microsoft.AspNetCore.Mvc;

namespace ConciliadorFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoFinanceiroController : ControllerBase
    {
        private readonly ILancamentoFinanceiroBusiness _businessLancamento;

        #region Construtores

        public LancamentoFinanceiroController(ILancamentoFinanceiroBusiness businessLancamento)
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

        #region Put

        [HttpPut]
        public async Task<IActionResult> Editar(LancamentoFinanceiro lancamentoFinanceiro)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ValidationProblem();

                await _businessLancamento.Editar(lancamentoFinanceiro);

                return Ok(lancamentoFinanceiro);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        #endregion

        #region Delete

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<LancamentoFinanceiro>> Deletar(int id)
        {
            try
            {
                await _businessLancamento.Deletar(new LancamentoFinanceiro() { Id = id });
                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        #endregion

        #region Get

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LancamentoFinanceiro>> Consultar(int id)
        {
            var lancamento = await _businessLancamento.Consultar(id);

            if (lancamento == null)
                return NotFound();

            return Ok(lancamento);
        }

        [HttpGet]
        public async Task<IEnumerable<LancamentoFinanceiro>> ConsultarLista()
        {
            var lancamentos = await _businessLancamento.ConsultarLista();
            return lancamentos.AsEnumerable();
        }

        #endregion

    }
}