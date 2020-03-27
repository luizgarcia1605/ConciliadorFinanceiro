﻿using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesBusiness;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository;
using ConciliadorFinanceiro.Repository;
using ConciliadorFinanceiro.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliadorFinanceiro.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILancamentoFinanceiroRepository, LancamentoFinanceiroRepository>();
            services.AddScoped<ILancamentoFinanceiroBusiness, LancamentoFinanceiroBusiness>();
            services.AddScoped<IDatabase, SqlDatabase>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
