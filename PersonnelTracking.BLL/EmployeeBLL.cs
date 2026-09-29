using PersonnelTracking.DAL;
using PersonnelTracking.DAL.DAO;
using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.BLL
{
    public class EmployeeBLL
    {
        public static void AddEmployee(Employee employee)
        {
            EmployeeDAO.AddEmployee(employee);
        }

        public static void deleteEmployee(int employeeID)
        {
            EmployeeDAO.deleteEmployee(employeeID);
        }

        public static EmployeeDTO GetAll()
        {
            EmployeeDTO dto = new EmployeeDTO();
            dto.Departments = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPosition();
            dto.Employees = EmployeeDAO.GetEmployee();
            
            return dto;
        }

        public static List<Employee> GetEmployee(int v, string text)
        {
            return EmployeeDAO.GetEmployee(v, text);
        }

        public static bool isUnique(int v)
        {
            List<Employee> employee = EmployeeDAO.GetUsers(v);
            if (employee.Count>0)          
                return false;            
            else            
                return true;            
        }

        public static void updateEmployee(Employee update)
        {
            EmployeeDAO.updateEmployee(update);
        }
    }
}
