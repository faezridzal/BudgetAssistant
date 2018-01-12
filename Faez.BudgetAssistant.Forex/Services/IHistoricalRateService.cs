namespace Faez.BudgetAssistant.Forex.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IHistoricalRateService
    {
        Task<ForexRate> GetHistoricalRateAsync(DateTime date, string fromCurrency,
            string toCurrency);
    }
}
