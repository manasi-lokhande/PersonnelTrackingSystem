using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DTO
{
    public class EmployeeDTO:Employee
    {
        public List<Department> Departments { get; set; }
        public List<PositionDTO> Positions { get; set; }
        public List<EmployeeDetailsDTO> Employees { get; set; }

    }
}
