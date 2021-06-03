using System;
using System.Collections.Generic;
using System.Text;

namespace StationSearchCore.Domain.Interfaces
{
    public interface IStationRepository
    {
        IList<string> GetAll();
    }
}
