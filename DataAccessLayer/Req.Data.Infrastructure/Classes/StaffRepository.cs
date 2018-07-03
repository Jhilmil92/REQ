using DataAccessLayer.Infrastructure.Classes;
using DataAccessLayer.Infrastructure.Interfaces;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Req.Data.Infrastructure.Classes
{
    public class StaffRepository : IStaffRepository
    {
        private readonly IReqDataSource _dataContext;
        public StaffRepository()
        {
            _dataContext = new DataContext();
        }
        public Domain.Classes.Req.Domain.Entities.Staff Create(Domain.Classes.Req.Domain.Entities.Staff staff)
        {
            ((DbSet<Staff>)_dataContext.Staff).Add(staff);
            _dataContext.Save();
            return staff;
        }

        public void Update(Domain.Classes.Req.Domain.Entities.Staff staff)
        {
            this._dataContext.Save();
        }

        public Domain.Classes.Req.Domain.Entities.Staff GetStaffById(int staffID)
        {
            var staff = _dataContext.Staff.SingleOrDefault(d=>d.StaffId == staffID);
            return staff;
        }

        public IQueryable<Staff> GetStaff()
        {
            return _dataContext.Staff;
        }

        public void Delete(int staffId)
        {
            Staff staff = new Staff
            {
                StaffId = staffId
            };
            var dataContext = _dataContext as DataContext;
            dataContext.Entry(staff).State = EntityState.Deleted;
            _dataContext.Save();
        }
    }
}
