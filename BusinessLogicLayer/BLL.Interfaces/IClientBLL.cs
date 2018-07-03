using Domain.Classes.Req.Domain.Entities;
using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Interfaces
{
    public interface IClientBLL
    {
        void CreateClient(ClientRegistrationViewModel registrationViewModel);
        IQueryable<Client> GetClients();
        Client GetClientById(int clientId);
        void UpdateClient(Client client);
        void DeleteClient(int clientId);
    }
}
