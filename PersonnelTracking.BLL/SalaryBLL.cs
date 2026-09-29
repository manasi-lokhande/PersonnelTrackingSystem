using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelTracking.DAL;
using PersonnelTracking.DAL.DAO;

namespace PersonnelTracking.BLL
{
    public class SalaryBLL
    {
        public static void AddSalary(Salary salary)
        {
             SalaryDAO.AddSalary(salary);
        }

        public static void deleteSalary(int salaryID)
        {
            SalaryDAO.deleteSalary(salaryID);
        }

        public static SalaryDTO GetAll()
        {
            SalaryDTO dto = new SalaryDTO();
            dto.Employee = EmployeeDAO.GetEmployee();
            dto.Departments = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPosition();
            dto.SalaryMonths = SalaryDAO.GetMonths();
            dto.Salary = SalaryDAO.GetAll();
            return dto;
        }

        public static void updateSalary(Salary update)
        {
            SalaryDAO.updateSalary(update);
        }
    }
}
