using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildHealthBook.Web.Models.AnalyticsDtos
{
    public class WebChildrenAverageCountPerParentRecord
    {
        public float Average { get; set; }
        public DateTime DateOfRecordCreation { get; set; }
    }
}
