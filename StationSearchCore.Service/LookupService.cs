using StationSearchCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StationSearchCore.Service
{
    public class LookupService : ILookupService
    {
        private IStationRepository StationRepository { get; }

        public LookupService(IStationRepository stationRepository) =>
            StationRepository = stationRepository;

        public Task<IEnumerable<string>> GetAllStartingWithAsync(string name)
        {
            return Task.FromResult(GetAllStartingWith(name)); 
        }

        public IEnumerable<string> GetAllStartingWith(string name)
        {
            name = name?.ToUpper() ?? throw new ArgumentNullException(nameof(name));

            return StationRepository
                .GetAll()
                .Where(x => x.ToUpper().StartsWith(name))
                .OrderBy(x => x);
        }

        public IEnumerable<char> NextPossibleChars(IEnumerable<string> stations, string filter)
        {
            return stations
                .Where(x => x.Length > filter.Length)
                .Select(station => station.Skip(filter.Length).Take(1).First())
                .OrderBy(x => x)
                .Distinct();
        }
    }
}
