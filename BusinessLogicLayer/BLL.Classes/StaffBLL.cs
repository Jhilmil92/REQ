using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Req.Data.Infrastructure.Classes;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Classes
{
    public class StaffBLL : IStaffBLL
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IUserRepository _userRepository;
        public StaffBLL()
        {
            _staffRepository = new StaffRepository();
            _userRepository = new UserRepository();
        }

        public void CreateStaff(Domain.Classes.Req.Domain.ViewModels.StaffRegistrationViewModel registrationViewModel)
        {
            var staff = new Staff()
            {
                StaffName = registrationViewModel.StaffName
            };

            var createdStaff = _staffRepository.Create(staff);
            _userRepository.CreateUser(registrationViewModel.StaffUserName, registrationViewModel.StaffPassword, UserType.Staff,createdStaff.StaffId);
        }
        public IQueryable<Domain.Classes.Req.Domain.Entities.Staff> GetStaff()
        {
            return _staffRepository.GetStaff();
        }

        public Domain.Classes.Req.Domain.Entities.Staff GetStaffById(int staffId)
        {
            var staff = _staffRepository.GetStaffById(staffId);
            return staff;
        }

        public void UpdateStaff(Domain.Classes.Req.Domain.Entities.Staff staff)
        {
            _staffRepository.Update(staff);
        }

        public void DeleteStaff(int staffId)
        {
            _staffRepository.Delete(staffId);
        }

    }
}
