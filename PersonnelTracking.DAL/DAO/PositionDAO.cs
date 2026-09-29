using PersonnelTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelTracking.DAL.DAO
{
    public class PositionDAO: EmployeeContext
    {
        public static void AddPosition(Position position)
        {
            db.Positions.InsertOnSubmit(position);
            db.SubmitChanges();
        }

        public static void deletePosition(int positionID)
        {
            try
            {
                Position pos = db.Positions.First(x => x.PositionID == positionID);
                db.Positions.DeleteOnSubmit(pos);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<PositionDTO> GetPosition()
        {
            try
            {
                var list = (from p in db.Positions
                            join
                           d in db.Departments on
                           p.DepartmentID equals d.DepartmentID
                            select new
                            {
                                positionID = p.PositionID,
                                positionName = p.PositionName,
                                departmentName = d.DepartmentName,
                                departmentID = p.DepartmentID
                            }).OrderBy(x => x.positionID).ToList();

                List<PositionDTO> positionlist = new List<PositionDTO>();
                foreach (var item in list)
                {
                    PositionDTO dto = new PositionDTO();
                    dto.PositionID = item.positionID;
                    dto.PositionName = item.positionName;
                    dto.DepartmentID = item.departmentID;
                    dto.DepartmentName = item.departmentName;
                    positionlist.Add(dto);
                }
                return positionlist;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public static void updatePosition(Position position)
        {
            try
            {
                Position ps = db.Positions.First(x => x.PositionID == position.PositionID);
                ps.PositionName = position.PositionName;
                ps.DepartmentID = position.DepartmentID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
