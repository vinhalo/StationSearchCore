using System.Collections.Generic;
using System.Threading.Tasks;

namespace StationSearchCore.Domain.Interfaces
{
    public interface ILookupService
    {
        Task<IEnumerable<string>> GetAllStartingWithAsync(string name);
        IEnumerable<string> GetAllStartingWith(string name);
    }
}
