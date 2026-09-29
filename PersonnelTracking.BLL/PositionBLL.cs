using PersonnelTracking.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonnelTracking.DAL.DAO;
using PersonnelTracking.DAL.DTO;
namespace PersonnelTracking.BLL
{
    public class PositionBLL
    {
        public static void AddPosition(Position position)
        {
           PositionDAO.AddPosition(position);
        }

        public static void deletePosition(int positionID)
        {
            PositionDAO.deletePosition(positionID);
        }

        public static List<PositionDTO> GetPosition()
        {
            return PositionDAO.GetPosition();
        }

        public static void updatePosition(Position position, bool control)
        {
            PositionDAO.updatePosition(position);
            if (control)
                EmployeeDAO.updateEmployee(position);
        }
    }
}
