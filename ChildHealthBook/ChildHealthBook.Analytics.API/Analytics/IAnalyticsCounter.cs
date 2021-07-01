using ChildHealthBook.Common.WebDtos.ChildDtos;
using System.Collections.Generic;

namespace ChildHealthBook.Analytics.API.Analytics
{
    public interface IAnalyticsCounter
    {
        float GetClassSpecifiedFactor(IEnumerable<ChildWithEventsReadDto> children);
    }
}
