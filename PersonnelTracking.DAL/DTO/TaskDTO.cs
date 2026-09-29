using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DTO
{
    public class TaskDTO:Task
    {
        public List<Department> departments = new List<Department>();
        public List<PositionDTO> Positions = new List<PositionDTO>();
        public List<EmployeeDetailsDTO> Employees = new List<EmployeeDetailsDTO>();
        public List<TaskState> taskStates = new List<TaskState>();
        public List<TaskDetailDTO> task = new List<TaskDetailDTO>();
    }
}
