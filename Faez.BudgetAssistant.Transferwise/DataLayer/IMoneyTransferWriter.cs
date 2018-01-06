namespace Faez.BudgetAssistant.Transferwise.DataLayer
{
    using System.Collections.Generic;
    using Models;

    public interface IMoneyTransferWriter
    {
        void Write(IEnumerable<MoneyTransfer> moneyTransfers);
    }
}
