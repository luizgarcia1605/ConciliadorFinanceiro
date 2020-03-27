using ConciliadorFinanceiro.Base.Domain.Entities;
using ConciliadorFinanceiro.Base.Domain.Interfaces.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConciliadorFinanceiro.Repository.Repository
{
    public class LancamentoFinanceiroRepository : BaseRepository<LancamentoFinanceiro>, ILancamentoFinanceiroRepository
    {
        public LancamentoFinanceiroRepository(IDatabase database) : base(database) { }
    }
}
