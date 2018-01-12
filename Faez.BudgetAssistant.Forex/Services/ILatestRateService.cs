namespace Faez.BudgetAssistant.Forex.Services
{
    using System.Threading.Tasks;
    using Models;

    public interface ILatestRateService
    {
        Task<ForexRate> GetLatestRateAsync(string fromCurrency, string toCurrency);
    }
}
