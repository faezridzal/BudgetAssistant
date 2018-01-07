namespace Faez.BudgetAssistant.Dropbox.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::Dropbox.Api;

    public sealed class FileLister : IFileLister
    {
        private const string ApplicationRoot = "/Apps/Faez.BudgetAssistant";

        private readonly DropboxClient _client;
        
        public FileLister(string accessToken)
        {
            _client = new DropboxClient(accessToken);
        }

        public async Task<IReadOnlyCollection<string>> GetFiles()
        {
            var folders = await _client.Files.ListFolderAsync(ApplicationRoot);
            
            return folders.Entries
                .Where(e => e.IsFile)
                .Select(e => e.PathDisplay)
                .ToList();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
