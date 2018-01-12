namespace Faez.BudgetAssistant.Forex.Models
{
    using System;

    public sealed class ForexRate
    {
        public DateTime Date { get; set; }

        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public decimal Rate { get; set; }
    }
}
