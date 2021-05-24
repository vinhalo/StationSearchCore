using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StationSearchCore.Service;

namespace StationSearchCore.Web.Controllers
{
    public class StationsController : ControllerBase
    {
        private LookupService s = new LookupService();

        // GET api/values
        public async Task<StationSearchResult> GetStations(string filter)
        {
            if (filter == "" || filter == null)
            {
                filter = " ";
            }

            filter = filter.Replace("\"", "");

            var stations = await s.GetAllStartingWithAsync(filter);

            var nextPossibleChars = stations.Where(station => station.Length > filter.Length).Select(station => station[filter.Length]).OrderBy(x => x).Distinct();

            return new StationSearchResult(nextPossibleChars, stations.OrderBy(x => x));
        }

    }

    public class StationSearchResult
    {
        public StationSearchResult() { }

        public StationSearchResult(IEnumerable<char> nextPossibleCharacters, IEnumerable<string> stations)
        {
            NextPossibleCharacters = nextPossibleCharacters ?? new char[0];
            Stations = stations ?? new string[0];
        }

        public IEnumerable<char> NextPossibleCharacters { get; private set; }

        public IEnumerable<string> Stations { get; private set; }
    }

}
