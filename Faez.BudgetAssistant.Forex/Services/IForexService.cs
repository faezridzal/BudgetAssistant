namespace Faez.BudgetAssistant.Forex.Services
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IForexService
    {
        Task<ForexRate> GetRateAsync(DateTime date, string fromCurrency, string toCurrency);
    }
}
