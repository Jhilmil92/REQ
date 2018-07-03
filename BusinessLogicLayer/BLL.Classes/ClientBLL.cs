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
    public class ClientBLL : IClientBLL
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        public ClientBLL()
        {
            _clientRepository = new ClientRepository();
        }
        public void CreateClient(Domain.Classes.Req.Domain.ViewModels.ClientRegistrationViewModel registrationViewModel)
        {
            var client = new Client()
            {
                ClientOrganization = registrationViewModel.ClientOrganization,
                JoinDate = registrationViewModel.JoinDate
            };

            var createdClient = _clientRepository.Create(client);
        }

        public IQueryable<Domain.Classes.Req.Domain.Entities.Client> GetClients()
        {
            return _clientRepository.GetClients();
        }

        public Domain.Classes.Req.Domain.Entities.Client GetClientById(int clientId)
        {
            var client = _clientRepository.GetClientById(clientId);
            return client;
        }

        public void UpdateClient(Domain.Classes.Req.Domain.Entities.Client client)
        {
            _clientRepository.Update(client);
        }

        public void DeleteClient(int clientId)
        {
            _clientRepository.Delete(clientId);
        }
    }
}
