using KpiNew.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KpiNew.Dto
{
    public class KpiDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public ICollection<EmployeeDto> Employee { get; set; } = new List<EmployeeDto>();

    }

    public class CreateKpiRequestModel
    {
        public string Name { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Kpi Can only be between 0 .. 10")]
        public int Rating { get; set; }
    }
    public class UpdateKpiRequestModel
    {
        public string Name { get; set; }
        public int Rating { get; set; }
    }
}
