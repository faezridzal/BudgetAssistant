namespace Faez.BudgetAssistant.Transferwise.DataLayer
{
    using System.Collections.Generic;
    using Models;

    public interface IMoneyTransferReader
    {
        IEnumerable<MoneyTransfer> Read();
    }
}
