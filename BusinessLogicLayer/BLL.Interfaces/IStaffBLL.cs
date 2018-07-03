using Domain.Classes.Req.Domain.Entities;
using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Interfaces
{
    public interface IStaffBLL
    {
        void CreateStaff(StaffRegistrationViewModel registrationViewModel);
        IQueryable<Staff> GetStaff();
        Staff GetStaffById(int staffId);
        void UpdateStaff(Staff staff);
        void DeleteStaff(int staffId);
    }
}
