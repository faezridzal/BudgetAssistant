namespace Faez.BudgetAssistant.Transferwise.Models
{
    using System;

    public sealed class MoneyTransfer
    {
        public DateTime Date { get; set; }

        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public decimal EffectiveRate { get; set; }

        public decimal Amount { get; set; }
    }
}
