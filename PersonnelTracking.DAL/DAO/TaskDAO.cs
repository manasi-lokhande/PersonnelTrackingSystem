using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static void AddTask(Task task)
        {
            try
            {
                db.Tasks.InsertOnSubmit(task);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            try
            {
                Task task= db.Tasks.First(x => x.TaskID == taskID);
                if (isAdmin)
                    task.TaskStateID = TaskStateDTO.Approved;
                else
                    task.TaskStateID = TaskStateDTO.Delivered;
                task.TaskDeliveryDate = DateTime.Today;
                db.SubmitChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void deleteTask(int taskID)
        {
            try
            {
                Task ts = db.Tasks.First(x => x.TaskID == taskID);
                db.Tasks.DeleteOnSubmit(ts);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<TaskDetailDTO> GetAll()
        {
            try
            {
                List<TaskDetailDTO> taskList = new List<TaskDetailDTO>();
                var list = (from t in db.Tasks join
                             ts in db.TaskStates on
                             t.TaskStateID equals ts.TaskStateID join
                             e in db.Employees on
                             t.EmployeeID equals e.EmployeeID join
                             d in db.Departments on
                             e.DepartmentID equals d.DepartmentID join
                             p in db.Positions on
                             e.PositionID equals p.PositionID
                            select new
                            {
                                taskID = t.TaskID,
                                taskTitle = t.TaskTitle,
                                taskContent = t.TaskContent,
                                taskStartDate = t.TaskStartDate,
                                taskDeliveryDate = t.TaskDeliveryDate,
                                taskStateID = t.TaskStateID,
                                stateName = ts.StateName,
                                employeeID = e.EmployeeID,
                                userNo = e.UserNo,
                                name = e.FirstName,
                                surname = e.LastName,
                                departmentId = e.DepartmentID,
                                positionId = e.PositionID,
                                departmentName = d.DepartmentName,
                                positionName = p.PositionName,
                            }).OrderBy(x => x.taskID).ToList();

                foreach (var item in list)
                {
                    TaskDetailDTO dto = new TaskDetailDTO();
                    dto.TaskID = item.taskID;
                    dto.TaskTitle = item.taskTitle;
                    dto.TaskContent = item.taskContent;
                    dto.TaskStartDate = item.taskStartDate;
                    dto.TaskDeliveryDate = item.taskDeliveryDate;
                    dto.TaskStateID = item.taskStateID;
                    dto.StateName = item.stateName;
                    dto.EmployeeId = item.employeeID;
                    dto.UserNo = item.userNo;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.DepartmentID = item.departmentId;
                    dto.DepartmentName = item.departmentName;
                    dto.PositionID = item.positionId;
                    dto.PositionName = item.positionName;
                    taskList.Add(dto);
                }
                return taskList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<TaskState> GetTaskState()
        {
            return db.TaskStates.ToList();
        }

        public static void updateTask(Task update)
        {
            try
            {
                Task task = db.Tasks.First(x => x.TaskID == update.TaskID);
                task.EmployeeID = update.EmployeeID;
                task.TaskTitle = update.TaskTitle;
                task.TaskContent = update.TaskContent;
                task.TaskStateID = update.TaskStateID;                
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
