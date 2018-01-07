namespace Faez.BudgetAssistant.Excel.Models
{
    using System;
    using System.Collections.Generic;

    public sealed class BudgetEntry
    {
        public DateTime Date { get; set; }

        public string Category { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public bool IsCredit { get; set; }

        public IList<string> Comments { get; set; }
    }
}
