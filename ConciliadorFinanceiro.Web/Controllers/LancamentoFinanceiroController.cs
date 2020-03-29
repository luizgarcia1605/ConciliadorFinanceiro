using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ConciliadorFinanceiro.Web.Controllers
{
    public class LancamentoFinanceiroController : Controller
    {
        private readonly IOptions<APIConfig> _options;

        public LancamentoFinanceiroController(IOptions<APIConfig> options)
        {
            _options = options;
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            var lancamentos = new List<LancamentoFinanceiro>();

            using (var client = new HttpClient())
            {
                using (var resposta = await client.GetAsync(_options.Value.URILancamentoFinanceiro))
                {
                    if (resposta.IsSuccessStatusCode)
                    {
                        var json = await resposta.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<LancamentoFinanceiro[]>(json).ToList();
                    }
                    else if (resposta.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        TempData["Erro"] = "Erro na consulta dos lançamentos cadastrados";
                    }
                }
            }

            return View(lancamentos);
        }

        #endregion

        #region Detalhar

        public async Task<IActionResult> Detalhar(int id)
        {
            var lancamento = new LancamentoFinanceiro();

            using (var client = new HttpClient())
            {
                using (var resposta = await client.GetAsync(String.Format("{0}{1}", _options.Value.URILancamentoFinanceiro, id)))
                {
                    if (resposta.IsSuccessStatusCode)
                    {
                        var json = await resposta.Content.ReadAsStringAsync();
                        lancamento = JsonConvert.DeserializeObject<LancamentoFinanceiro>(json);
                    }
                    else
                    {
                        TempData["Erro"] = "Erro na busca dos detalhes do lançamento";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View(lancamento);
        }

        #endregion

        #region Cadastrar

        public ActionResult Cadastrar()
        {
            CarregaListas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(LancamentoFinanceiro lancamento)
        {
            try
            {
                var lancamentoRetorno = new LancamentoFinanceiro();

                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(lancamento);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(_options.Value.URILancamentoFinanceiro, content);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Erro"] = "Erro no cadastro de lançamento";
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion

        #region Editar

        public async Task<IActionResult> Editar(int id)
        {
            var lancamento = new LancamentoFinanceiro();

            using (var client = new HttpClient())
            {
                using (var resposta = await client.GetAsync(String.Format("{0}{1}", _options.Value.URILancamentoFinanceiro, id)))
                {
                    if (resposta.IsSuccessStatusCode)
                    {
                        var json = await resposta.Content.ReadAsStringAsync();
                        lancamento = JsonConvert.DeserializeObject<LancamentoFinanceiro>(json);
                        CarregaListas();
                    }
                    else
                    {
                        TempData["Erro"] = "Erro na busca do lançamento para edição";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View(lancamento);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, LancamentoFinanceiro lancamento)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var responseMessage = await client.PutAsJsonAsync(_options.Value.URILancamentoFinanceiro, lancamento))
                    {
                        if (!responseMessage.IsSuccessStatusCode)
                        {
                            TempData["Erro"] = "Erro na edição do lançamento";
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Deletar

        public async Task<IActionResult> Deletar(int id)
        {
            var lancamento = new LancamentoFinanceiro();

            using (var client = new HttpClient())
            {
                using (var resposta = await client.GetAsync(String.Format("{0}{1}", _options.Value.URILancamentoFinanceiro, id)))
                {
                    if (resposta.IsSuccessStatusCode)
                    {
                        var json = await resposta.Content.ReadAsStringAsync();
                        lancamento = JsonConvert.DeserializeObject<LancamentoFinanceiro>(json);
                    }
                    else
                    {
                        TempData["Erro"] = "Erro na busca do lançamento para exclusão";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            return View(lancamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id, IFormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var resposta = await client.DeleteAsync(String.Format("{0}{1}", _options.Value.URILancamentoFinanceiro, id)))
                    {
                        if (!resposta.IsSuccessStatusCode)
                        {
                            TempData["Erro"] = "Erro na exclusão do lançamento";
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Listas

        private void CarregaListas()
        {
            var tipos = from TipoLancamento t in Enum.GetValues(typeof(TipoLancamento))
                        select new { Id = (int)t, Descricao = t.ToString() };

            var status = from StatusLancamento s in Enum.GetValues(typeof(StatusLancamento))
                         select new { Id = (int)s, Descricao = s.ToString() };

            ViewBag.TipoLancamento = new SelectList(tipos, "Id", "Descricao");
            ViewBag.StatusLancamento = new SelectList(status, "Id", "Descricao");
        }

        #endregion

    }
}