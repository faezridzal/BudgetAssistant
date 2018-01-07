namespace Faez.BudgetAssistant.Excel.UnitTests.Helpers
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class PathHelper
    {
        public static string GetAssemblyPath()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);

            return Path.GetDirectoryName(path);
        }
    }
}
