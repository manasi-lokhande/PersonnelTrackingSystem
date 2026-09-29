using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static void AddSalary(Salary salary)
        {
            try
            {
                db.Salaries.InsertOnSubmit(salary);
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void deleteSalary(int salaryID)
        {
            try
            {
                Salary s = db.Salaries.First(x => x.SalaryID == salaryID);
                db.Salaries.DeleteOnSubmit(s);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<SalaryDetailDTO> GetAll()
        {
            try
            {
                List<SalaryDetailDTO> SalaryList = new List<SalaryDetailDTO>();

                var list = (from s in db.Salaries
                            join
                             e in db.Employees on s.EmployeeID equals e.EmployeeID
                            join d in db.Departments on e.DepartmentID equals d.DepartmentID
                            join p in db.Positions on e.PositionID equals p.PositionID
                            join sm in db.SalaryMonths on s.MonthID equals sm.MonthID
                            select new
                            {
                                salaryID = s.SalaryID,
                                employeeID = e.EmployeeID,
                                userNo = e.UserNo,
                                name = e.FirstName,
                                surname = e.LastName,
                                departmentID = e.DepartmentID,
                                positionID = e.PositionID,
                                departmentName = d.DepartmentName,
                                positionName = p.PositionName,
                                salary = s.Amount,
                                year = s.Year,
                                monthID = s.MonthID,
                                month = sm.MonthName
                            }
                           ).OrderBy(x => x.salaryID).ToList();
                foreach (var item in list)
                {
                    SalaryDetailDTO dto = new SalaryDetailDTO();
                    dto.SalaryID = item.salaryID;
                    dto.EmployeeID = item.employeeID;
                    dto.UserNo = item.userNo;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.DepartmentID = item.departmentID;
                    dto.PositionID = item.positionID;
                    dto.DepartmentName = item.departmentName;
                    dto.PositionName = item.positionName;
                    dto.Salary = item.salary;
                    dto.Year = item.year;
                    dto.MonthID = item.monthID;
                    dto.Months = item.month;
                    SalaryList.Add(dto);

                }

                return SalaryList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<SalaryMonth> GetMonths()
        {
            return db.SalaryMonths.ToList();
        }

        public static void updateSalary(Salary update)
        {
            try
            {
                Salary salary = db.Salaries.First(x => x.SalaryID == update.SalaryID);
                salary.EmployeeID = update.EmployeeID;
                salary.Amount = update.Amount;
                salary.Year = update.Year;
                salary.MonthID = update.MonthID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
