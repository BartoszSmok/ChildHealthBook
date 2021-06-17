using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.AnalyticsDtos
{
    public class SharedStats
    {
        public double VaccinationFactor { get; set; }
        public double ChildrenAverageAge { get; set; }
        public double AverageChildrenCountPerParent { get; set; }
    }
}
