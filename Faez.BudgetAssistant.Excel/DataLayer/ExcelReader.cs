namespace Faez.BudgetAssistant.Excel.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using Models;
    using OfficeOpenXml;

    public sealed class ExcelReader : IExcelReader
    {
        private const int StartRow = 4;
        private const int EndRow = 27;

        private const int StartColumn = 2;
        private const int EndColumn = 13;

        private const string Euro = "€";
        private const string Ringgit = "MYR";

        private static readonly ISet<int> CreditRows = new HashSet<int> { 4, 5, 6 };

        public IEnumerable<BudgetEntry> GetEntries(Stream stream)
        {
            using (var excelPackage = new ExcelPackage(stream))
            {
                foreach (var worksheet in excelPackage.Workbook.Worksheets)
                {
                    var categoryLookup = GetCategoryLookup(worksheet);
                    var monthLookup = GetMonthLookup(worksheet);
                    var year = Convert.ToInt32(worksheet.Name);

                    for (var column = StartColumn; column <= EndColumn; column++)
                    {
                        if (!monthLookup.TryGetValue(column, out var month))
                        {
                            continue;
                        }

                        for (var row = StartRow; row <= EndRow; row++)
                        {
                            if (!categoryLookup.TryGetValue(row, out var category))
                            {
                                continue;
                            }

                            var cell = worksheet.Cells[row, column];

                            if (!(cell.Value is double amount))
                            {
                                continue;
                            }
                            
                            var entry = new BudgetEntry
                            {
                                Date = new DateTime(year, month, 1),
                                Category = category,
                                Currency = GetCurrency(cell.Text),
                                Amount = Convert.ToDecimal(amount),
                                IsCredit = CreditRows.Contains(row),
                                Comments = GetComments(cell)
                            };

                            yield return entry;
                        }
                    }
                }
            }
        }

        private static IDictionary<int, string> GetCategoryLookup(ExcelWorksheet worksheet)
        {
            var results = new Dictionary<int, string>();

            for (var row = StartRow; row <= EndRow; row++)
            {
                var value = worksheet.Cells[row, 1].Text;

                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                results.Add(row, value);
            }

            return results;
        }

        private static IDictionary<int, int> GetMonthLookup(ExcelWorksheet worksheet)
        {
            var results = new Dictionary<int, int>();

            for (var column = StartColumn; column <= EndColumn; column++)
            {
                var value = worksheet.Cells[3, column].Text;

                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                var month = DateTime.ParseExact(value, "MMMM", CultureInfo.InvariantCulture).Month;

                results.Add(column, month);
            }

            return results;
        }

        private static string[] GetComments(ExcelRangeBase cell)
        {
            return cell.Comment == null 
                ? new string[0] 
                : cell.Comment.Text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }

        private static string GetCurrency(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            if (text.StartsWith(Euro))
            {
                return "EUR";
            }

            return text.StartsWith(Ringgit) ? Ringgit : null;
        }
    }
}
