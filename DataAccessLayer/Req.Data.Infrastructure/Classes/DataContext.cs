using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Classes
{
    public class DataContext:DbContext,IReqDataSource
    {
        public DataContext():base("DefaultConnection")
        {

        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<StakeHolder> StakeHolders { get; set; }
        public DbSet<Taker> Takers { get; set; }


        IQueryable<Job> IReqDataSource.Jobs
        {
            get
            {
                return Jobs;
            }
            set 
            {
            }
        }

        IQueryable<JobType> IReqDataSource.JobTypes
        {
            get
            {
                return JobTypes;
            }
        }

        IQueryable<Priority> IReqDataSource.Priorities
        {
            get
            {
                return Priorities;
            }
        }

        IQueryable<StakeHolder> IReqDataSource.StakeHolders
        {
            get
            {
                return StakeHolders;
            }
            set
            {
            }
        }

        IQueryable<Taker> IReqDataSource.Takers
        {
            get
            {
                return Takers;
            }
            set
            {
            }
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}
