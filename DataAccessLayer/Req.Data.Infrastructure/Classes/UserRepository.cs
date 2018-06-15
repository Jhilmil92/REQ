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
    public class UserRepository:IUserRepository
    {

        private readonly IReqDataSource _dataContext;

        public UserRepository()
        {
            _dataContext = new DataContext();
        }

        public User CreateUser(string userName, string password, Domain.Classes.Req.Domain.Entities.UserType userType, int targetUserID)
        {
            User user = new User
            {
                UserName = userName,
                Password = password,
                UserType = userType,
                TargetUserID = targetUserID
            };

            ((DbSet<User>)_dataContext.Users).Add(user);
            _dataContext.Save();
            return user;
        }

        public User GetUserById(int userId)
        {
           return _dataContext.Users.SingleOrDefault(x => x.UserId == userId);
        }

        public User GetUserByName(string userName)
        {
            return _dataContext.Users.SingleOrDefault(x =>string.Compare(x.UserName,userName,true) == 0);
        }

        public IQueryable<User> GetAllUsers()
        {
            return _dataContext.Users;
        }
    }
}
