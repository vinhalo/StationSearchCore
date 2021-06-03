using Microsoft.AspNetCore.Mvc;
using StationSearchCore.Domain.Interfaces;
using StationSearchCore.Web.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace StationSearchCore.Web.Controllers
{
    public class StationsController : ControllerBase
    {
        private ILookupService LookupService { get; } 

        public StationsController(ILookupService lookupService) =>
            LookupService = lookupService;

        // GET api/values
        public async Task<StationSearchResult> GetStations(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                filter = " ";
            }

            filter = filter.Replace("\"", "");

            var stations = await LookupService.GetAllStartingWithAsync(filter);

            // This should go inside a domain NextCharService so that service can be tested independently and this endpoint can be thinner.
            var nextPossibleChars = stations
                .Select(station => station[filter.Length])
                .OrderBy(x => x)
                .Distinct();

            return new StationSearchResult(nextPossibleChars, stations);
        }
    }
}
