using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DTO
{
    public class SalaryDTO
    {
        public List<EmployeeDetailsDTO> Employee = new List<EmployeeDetailsDTO>();
        public List<Department> Departments = new List<Department>();
        public List<PositionDTO> Positions = new List<PositionDTO>();
        public List<SalaryMonth> SalaryMonths = new List<SalaryMonth>();
        public List<SalaryDetailDTO> Salary = new List<SalaryDetailDTO>();
    }
}
