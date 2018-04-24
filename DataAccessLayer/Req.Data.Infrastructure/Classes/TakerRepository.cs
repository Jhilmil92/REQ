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
    public class TakerRepository:ITakerRepository
    {
        private readonly IReqDataSource _dataContext;

        public TakerRepository(IReqDataSource dataContext)
        {
            _dataContext = dataContext;
        }

        public void Create(Domain.Classes.Taker taker)
        {
            var newTaker = new Taker
            {
                TakerId = taker.TakerId,
                TakerName = taker.TakerName
            };
            _dataContext.Takers = newTaker as IQueryable<Taker>;
            _dataContext.Save();
        }

        public void Update(Domain.Classes.Taker taker)
        {
            this._dataContext.Save();
        }

        public Domain.Classes.Taker GetTakerById(int takerID)
        {
            var taker = _dataContext.Takers.SingleOrDefault(c => c.TakerId == takerID);
            return taker;
        }

        public IQueryable<Domain.Classes.Taker> GetTakers()
        {
            return _dataContext.Takers;
        }

        public void Delete(int takerId)
        {
            var taker = new Taker { TakerId = takerId};
            var dataContext = _dataContext as DataContext;
            dataContext.Entry(taker).State = EntityState.Deleted;
            _dataContext.Save();
        }
    }
}
