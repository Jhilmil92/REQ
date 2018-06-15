using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Req.Data.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        User CreateUser(string userName, string password, UserType userType, int TargetUserID);

        User GetUserById(int userId);
        User GetUserByName(string userName);

        IQueryable<User> GetAllUsers();
    }
}
