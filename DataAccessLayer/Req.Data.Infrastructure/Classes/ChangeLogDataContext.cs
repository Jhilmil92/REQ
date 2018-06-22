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
    public class ChangeLogDataContext:DbContext,IChangeLogDataSource
    {
        public ChangeLogDataContext():base("DefaultConnection")
        {

        }

        public DbSet<ChangeLog> ChangeLogs { get; set; }

        IQueryable<ChangeLog> IChangeLogDataSource.ChangeLogs
        {
            get
            {
                return ChangeLogs;
            }
        }

        public void Save()
        {
            SaveChanges();
        }

        
    }
}   
