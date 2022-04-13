using System.Collections.Generic;

namespace KpiNew.Entities
{
    public class Kpi : BaseEntity
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public ICollection<EmployeeKpi> EmployeeKpis { get; set; } = new List<EmployeeKpi>();
    }
}
