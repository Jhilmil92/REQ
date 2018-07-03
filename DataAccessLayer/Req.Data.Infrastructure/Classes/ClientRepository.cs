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
    public class ClientRepository:IClientRepository
    {
        private readonly IReqDataSource _dataContext;
        public ClientRepository()
        {
            _dataContext = new DataContext();
        }

        public Domain.Classes.Req.Domain.Entities.Client Create(Domain.Classes.Req.Domain.Entities.Client client)
        {
            ((DbSet<Client>)_dataContext.Clients).Add(client);
            _dataContext.Save();
            return client;
        }

        public void Update(Domain.Classes.Req.Domain.Entities.Client client)
        {
            this._dataContext.Save();
        }

        public Domain.Classes.Req.Domain.Entities.Client GetClientById(int clientID)
        {
            var client = _dataContext.Clients.SingleOrDefault(d => d.ClientId == clientID);
            return client;
        }

        public IQueryable<Domain.Classes.Req.Domain.Entities.Client> GetClients()
        {
            return _dataContext.Clients;
        }

        public void Delete(int clientId)
        {
            Client client = new Client
            {
                ClientId = clientId
            };
            var dataContext = _dataContext as DataContext;
            dataContext.Entry(client).State = EntityState.Deleted;
            _dataContext.Save();
        }
    }
}
