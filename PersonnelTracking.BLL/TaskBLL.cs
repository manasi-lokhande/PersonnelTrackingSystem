using PersonnelTracking.DAL.DAO;
using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelTracking.DAL;

namespace PersonnelTracking.BLL
{
    public class TaskBLL
    {
        public static void AddTask(DAL.Task task)
        {
            TaskDAO.AddTask(task);
        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            TaskDAO.ApproveTask(taskID, isAdmin);
        }

        public static void deleteTask(int taskID)
        {
            TaskDAO.deleteTask(taskID);
        }

        public static TaskDTO GetAll()
        {
            TaskDTO dto = new TaskDTO();
            dto.Employees = EmployeeDAO.GetEmployee();
            dto.departments = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPosition();
            dto.taskStates = TaskDAO.GetTaskState();
            dto.task = TaskDAO.GetAll();
            return dto;
        }

        public static void updateTask(DAL.Task update)
        {
            TaskDAO.updateTask(update);
        }
    }
}
