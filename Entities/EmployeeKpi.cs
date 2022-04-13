using KpiNew.Enum;
using System;
using System.Collections.Generic;

namespace KpiNew.Entities
{
    public class EmployeeKpi : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int KpiId { get; set; }
        public Kpi Kpi { get; set; }
        public double KpiRating { get; set; }
        public Month Month { get; set; }
        public int Year { get; set; }

    }
}
