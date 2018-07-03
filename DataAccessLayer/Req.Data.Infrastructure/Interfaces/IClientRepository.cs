using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Req.Data.Infrastructure.Interfaces
{
    public interface IClientRepository
    {
        Client Create(Client client);
        void Update(Client client);
        Client GetClientById(int clientID);
        IQueryable<Client> GetClients();
        void Delete(int clientId);
    }
}
