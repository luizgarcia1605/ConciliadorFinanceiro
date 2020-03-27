using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Enums;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesBusiness;
using Microsoft.AspNetCore.Mvc;

namespace ConciliadorFinanceiro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoFinanceiroController : ControllerBase
    {
        //TODO: fazer as saídas dos try-catchs
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
            if (!ModelState.IsValid)
                return NotFound();

            if (lancamentoFinanceiro != null)
                lancamentoFinanceiro.DataHoraLancamento = DateTime.Now;

            await _businessLancamento.Cadastrar(lancamentoFinanceiro);

            return Ok(lancamentoFinanceiro);
        }

        #endregion

        #region Put

        [HttpPut]
        public async Task<IActionResult> Editar(LancamentoFinanceiro lancamentoFinanceiro)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var lancamentoAtualizar = await _businessLancamento.Consultar(lancamentoFinanceiro);

            if (lancamentoAtualizar.Status == StatusLancamento.Conciliado)
                return BadRequest();

            lancamentoAtualizar.Valor = lancamentoFinanceiro.Valor;
            lancamentoAtualizar.Tipo = lancamentoFinanceiro.Tipo;
            lancamentoAtualizar.Status = lancamentoFinanceiro.Status;

            await _businessLancamento.Editar(lancamentoAtualizar);

            return Ok(lancamentoAtualizar);
        }

        #endregion

        #region Delete

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<LancamentoFinanceiro>> Deletar(int id)
        {
            await _businessLancamento.Deletar(new LancamentoFinanceiro() { Id = id });
            return Ok();
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