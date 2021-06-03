using StationSearchCore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StationSearchCore.Service
{
    // After all this refactoring, it is revealed that LookupService is just PrefixTree
    public class LookupService : ILookupService
    {
        private IPrefixTree PrefixTree { get; }

        public LookupService(IPrefixTree prefixTree) =>
            PrefixTree = prefixTree;

        public async Task<IEnumerable<string>> GetAllStartingWithAsync(string name) =>
            await PrefixTree.FindAsync(name);

        public IEnumerable<string> GetAllStartingWith(string name) =>
            PrefixTree.Find(name);
    }
}
