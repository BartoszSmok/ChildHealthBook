using ChildHealthBook.Common.WebDtos.ChildDtos;
using System.Collections.Generic;
using System.Linq;

namespace ChildHealthBook.Analytics.API.Analytics
{
    public class FactorCounter : IAnalyticsCounter
    {

        //Count children vaccinated to all children factor
        public float GetClassSpecifiedFactor(IEnumerable<ChildWithEventsReadDto> children)
        {
            float childrenCount = children.ToList().Count;
            float childrenVaccinatedCount = children
                .Where(child => child.MedicalEvents
                .Where(medev => medev.EventType == "Vaccination")
                .Any())
                .ToList().Count;

            float factor = childrenVaccinatedCount / childrenCount;
            return factor;
        }
    }
}
