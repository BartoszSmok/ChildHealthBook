using ChildHealthBook.Common.WebDtos.ChildDtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChildHealthBook.Analytics.API.Analytics
{
    public class AverageCounter : IAnalyticsCounter
    {
        //Count children average age
        public float GetClassSpecifiedFactor(IEnumerable<ChildWithEventsReadDto> children)
        {
            float childrenAgeSum = 0.0f;
            float childrenCount = children.ToList().Count;
            int daysInYearCount = 365;
            foreach(var child in children)
            {
                childrenAgeSum += (DateTime.Now - child.DateOfBirth).Days / daysInYearCount;
            }
            float average = -1;
            if(childrenCount != 0)
                average = childrenAgeSum / childrenCount;
            return average;
        }

        public float GetAverageChildrenCountPerParent(int childrenCount, int parentCount)
        {
            float average = -1;
            if (parentCount != 0)
             average = childrenCount / parentCount;
            return average;
        }
    }
}
