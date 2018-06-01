namespace Faez.BudgetAssistant.Forex.DataLayer
{
    using System.Collections.Generic;
    using Models;

    public sealed class MaybankForexReader : IForexReader
    {
        public IEnumerable<ForexRate> ReadAll(string payload)
        {
            return null;
        }
    }
}
