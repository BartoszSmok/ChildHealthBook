using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.AnalyticsDtos
{
    public class WebSharedStats
    {
        public float VaccinationFactor { get; set; }
        public float ChildrenAverageAge { get; set; }
        public float AverageChildrenCountPerParent { get; set; }
        public DateTime DateOfRecordCreationVaccinationFactor { get; set; }
        public DateTime DateOfRecordCreationChildrenAverageAge { get; set; }
        public DateTime DateOfRecordCreationAverageChildrenCountPerParent { get; set; }
    }
}
