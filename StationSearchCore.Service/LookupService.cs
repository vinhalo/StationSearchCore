using StationSearchCore.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StationSearchCore.Service
{
    // After all this refactoring, it is revealed that LookupService is just PrefixTree
    public class LookupService : ILookupService
    {
        private IPrefixTree PrefixTree { get; }

        public LookupService(IPrefixTree prefixTree) =>
            PrefixTree = prefixTree;

        public async Task<IEnumerable<string>> GetAllStartingWithAsync(string name)
        {
            var stations = await PrefixTree.FindAsync(name); 
            return stations.OrderBy(x => x);
        }

        public IEnumerable<string> GetAllStartingWith(string name)
        {
            return PrefixTree.Find(name).OrderBy(x => x);
        }
    }
}
