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
using Newtonsoft.Json;

namespace ConciliadorFinanceiro.Web.Controllers
{
    public class BalancoController : Controller
    {
        const string apiURI = "https://localhost:44300/api/Balanco/";

        public async Task<IActionResult> BalancoDiario()
        {
            var data = DateTime.Now;

            var lancamentos = new List<Balanco>();

            using (var client = new HttpClient())
            {
                using (var resposta = await client.GetAsync($"{apiURI}BalancoDiario?data={data:yyyy-MM-dd}"))
                {
                    if (resposta.IsSuccessStatusCode)
                    {
                        var json = await resposta.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<Balanco[]>(json).ToList();
                    }
                    else if (resposta.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        TempData["Erro"] = "Erro na consulta dos lançamentos cadastrados";
                    }
                }
            }

            return View(lancamentos);
        }

        public async Task<IActionResult> BalancoMensal()
        {
            var data = DateTime.Now;

            var lancamentos = new List<Balanco>();
            var inicioPeriodo = new DateTime(data.Year, data.Month, 1);
            var finalPeriodo = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));

            using (var client = new HttpClient())
            {
                using (var resposta = await client.GetAsync($"{apiURI}BalancoPeriodo?datainicio={inicioPeriodo:yyyy-MM-dd}&datafinal={finalPeriodo:yyyy-MM-dd}"))
                {
                    if (resposta.IsSuccessStatusCode)
                    {
                        var json = await resposta.Content.ReadAsStringAsync();
                        lancamentos = JsonConvert.DeserializeObject<Balanco[]>(json).ToList();
                    }
                    else if (resposta.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        TempData["Erro"] = "Erro na consulta dos lançamentos cadastrados";
                    }
                }
            }

            return View(lancamentos);
        }

    }
}