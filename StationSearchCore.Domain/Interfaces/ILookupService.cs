using System.Collections.Generic;
using System.Threading.Tasks;

namespace StationSearchCore.Domain.Interfaces
{
    public interface ILookupService
    {
        /// <summary>
        /// Async: All station names starting with given search term
        /// </summary>
        /// <param name="name">Search term</param>
        /// <returns>List of station names</returns>
        Task<IEnumerable<string>> GetAllStartingWithAsync(string name);
        
        /// <summary>
        /// All station names starting with given search term
        /// </summary>
        /// <param name="name">Search term</param>
        /// <returns>List of station names</returns>
        IEnumerable<string> GetAllStartingWith(string name);

        /// <summary>
        /// All next possible chars of stations after given filter 
        /// </summary>
        /// <param name="stations">List of station names</param>
        /// <param name="filter">Search term</param>
        /// <returns>List of next possible chars</returns>
        /// <remarks>
        /// stations = ["AB", "ABA", "AZ"]
        /// (1) filter = "" returns ['A']
        /// (2) filter = "A" returns ['B', 'Z']
        /// (3) filter = "AB" returns ['A']
        /// (4) filter = "ABA" returns []
        /// </remarks>
        IEnumerable<char> NextPossibleChars(IEnumerable<string> stations, string filter);
    }
}
