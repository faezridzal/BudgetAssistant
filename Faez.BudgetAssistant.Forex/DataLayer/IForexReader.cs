namespace Faez.BudgetAssistant.Forex.DataLayer
{
    using System.Collections.Generic;
    using Models;

    public interface IForexReader
    {
        IEnumerable<ForexRate> ReadAll(string payload);
    }
}