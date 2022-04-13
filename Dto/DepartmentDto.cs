using KpiNew.Entities;
using System.Collections.Generic;

namespace KpiNew.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EmployeeDto> Employee { get; set; }
    }

    public class CreateDepartmentRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateDepartmentRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
