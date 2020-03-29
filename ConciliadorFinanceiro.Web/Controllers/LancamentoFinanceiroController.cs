using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConciliadorFinanceiro.Base.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConciliadorFinanceiro.Web.Controllers
{
    public class LancamentoFinanceiroController : Controller
    {
        const string apiURI = "https://localhost:44300/api/LancamentoFinanceiro/";

        #region Index

        public async Task<IActionResult> Index()
        {
            var lancamentos = new List<LancamentoFinanceiro>();

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(apiURI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<LancamentoFinanceiro[]>(json).ToList();
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }
                }
            }

            return View(lancamentos);
        }

        #endregion

        #region Detalhar

        public async Task<IActionResult> Detalhar(int id)
        {
            var lancamentos = new LancamentoFinanceiro();

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(String.Format("{0}{1}", apiURI, id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jason = await response.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<LancamentoFinanceiro>(jason);
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }
                }
            }

            return View(lancamentos);
        }

        #endregion

        #region Cadastrar

        public async Task<ActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(LancamentoFinanceiro lancamento)
        {
            try
            {
                var lancamentos = new LancamentoFinanceiro();

                using (var client = new HttpClient())
                {
                    var serializedProduto = JsonConvert.SerializeObject(lancamento);
                    var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(apiURI, content);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Editar

        public async Task<IActionResult> Editar(int id)
        {
            var lancamentos = new LancamentoFinanceiro();

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(String.Format("{0}{1}", apiURI, id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<LancamentoFinanceiro>(ProdutoJsonString);
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }
                }
            }

            return View(lancamentos);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, LancamentoFinanceiro lancamento)
        {
            try
            {
                var lancamentos = new LancamentoFinanceiro();
                using (var client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.PutAsJsonAsync(apiURI + id, lancamento);

                    if (!responseMessage.IsSuccessStatusCode)
                        TempData["Erro"] = "Erro";
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
            var lancamentos = new LancamentoFinanceiro();

            using (var client = new HttpClient())
            {

                using (var response = await client.GetAsync(String.Format("{0}{1}", apiURI, id)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ProdutoJsonString = await response.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<LancamentoFinanceiro>(ProdutoJsonString);
                    }
                    else
                    {
                        TempData["Erro"] = "Erro";
                    }

                }
            }

            return View(lancamentos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id, IFormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiURI);
                    HttpResponseMessage responseMessage = await
                                    client.DeleteAsync(String.Format("{0}{1}", apiURI, id));

                    if (!responseMessage.IsSuccessStatusCode)
                        TempData["Erro"] = "Erro";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion

    }
}