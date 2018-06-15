using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.Entities;
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
        public DbSet<StakeHolder> StakeHolders { get; set; }
        public DbSet<Taker> Takers { get; set; }

        public DbSet<User> Users { get; set; }


        IQueryable<Job> IReqDataSource.Jobs
        {
            get
            {
                return Jobs;
            }
        }

        IQueryable<StakeHolder> IReqDataSource.StakeHolders
        {
            get
            {
                return StakeHolders;
            }
        }

        IQueryable<Taker> IReqDataSource.Takers
        {
            get
            {
                return Takers;
            }
        }

        IQueryable<User> IReqDataSource.Users
        {
            get
            {
                return Users;
            }
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}
