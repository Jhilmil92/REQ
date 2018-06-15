using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Classes
{
    public class LoginManager : ILoginManager
    {
        IUserRepository userRepo;
        public LoginManager(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public User ValidateCredentials(string userName, string password)
        {
            return userRepo.GetAllUsers().SingleOrDefault(x => 
                x.UserName.Equals(userName,StringComparison.InvariantCultureIgnoreCase)
                && x.Password.Equals(password));
        }


        public User GetUserByUserName(string userName)
        {
            return userRepo.GetAllUsers().SingleOrDefault(x =>
                x.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
