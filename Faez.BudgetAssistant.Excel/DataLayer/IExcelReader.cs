namespace Faez.BudgetAssistant.Excel.DataLayer
{
    using System.Collections.Generic;
    using System.IO;
    using Models;

    public interface IExcelReader
    {
        IEnumerable<BudgetEntry> GetEntries(Stream stream);
    }
}
