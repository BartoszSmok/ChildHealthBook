using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.AnalyticsDtos
{
    public class ChildrenVaccinatedStats
    {
        public int ChildrenCount { get; set; }
        public int ChildrenVaccinatedCount { get; set; }
        public double PercentageFactor { get; set; }
    }
}
