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
    public class LoginManager : ILoginManager
    {
        IUserRepository userRepo;
        private readonly IClientRepository _clientRepository;
        public LoginManager(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
            this._clientRepository = new ClientRepository();
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


        public Client GetClientByClientName(string clientName)
        {
            return _clientRepository.GetClients().SingleOrDefault(x => x.ClientOrganization.Equals(clientName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
