using System.Collections.Generic;

namespace StationSearchCore.Web.Dtos
{
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
