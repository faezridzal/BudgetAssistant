namespace Faez.BudgetAssistant.Forex.DataLayer
{
    using System.Collections.Generic;
    using System.IO;
    using Models;

    public interface IForexReader
    {
        IEnumerable<ForexRate> ReadAll(Stream stream);
    }
}