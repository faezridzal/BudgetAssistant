namespace Faez.BudgetAssistant.Dropbox.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFileLister : IDisposable
    {
        Task<IReadOnlyCollection<string>> GetFiles();
    }
}
