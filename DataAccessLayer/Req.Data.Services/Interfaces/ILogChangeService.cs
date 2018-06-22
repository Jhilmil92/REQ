using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Req.Data.Services.Interfaces
{
    public interface ILogChangeService
    {
        void LogChanges(DbContext dbContext, Job job);

        object GetPrimaryKeyValue(DbEntityEntry entry);
    }
}
