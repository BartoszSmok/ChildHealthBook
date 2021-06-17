using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.AnalyticsDtos
{
    public class ChildrenAverageCountPerParent
    {
        public int FamilyCount { get; set; }
        public int ChildrenCount { get; set; }
        public double AverageCountPerFamily { get; set; }
    }
}
