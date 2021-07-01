using System;

namespace ChildHealthBook.Common.AnalyticsDtos
{
    public class SharedStats
    {
        public float VaccinationFactor { get; set; }
        public float ChildrenAverageAge { get; set; }
        public float AverageChildrenCountPerParent { get; set; }
        public DateTime DateOfRecordCreationVaccinationFactor { get; set; }
        public DateTime DateOfRecordCreationChildrenAverageAge { get; set; }
        public DateTime DateOfRecordCreationAverageChildrenCountPerParent { get; set; }
    }
}
