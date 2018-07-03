using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Req.Data.Infrastructure.Interfaces
{
    public interface IStaffRepository
    {
        Staff Create(Staff staff);
        void Update(Staff staff);
        Staff GetStaffById(int staffID);
        IQueryable<Staff> GetStaff();
        void Delete(int staffId);
    }
}
