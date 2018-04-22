using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Classes
{
    public class TakerDAL:ITakerDAL
    {
        private readonly IReqDataSource _dataContext;

        public TakerDAL(IReqDataSource dataContext)
        {
            _dataContext = dataContext;
        }

        public void CreateTaker(Domain.Classes.Taker taker)
        {
            var newTaker = new Taker
            {
                TakerId = taker.TakerId,
                TakerName = taker.TakerName
            };
            _dataContext.Takers = newTaker as IQueryable<Taker>;
        }

        public void EditTaker(Domain.Classes.Taker taker)
        {
            
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

        public void DeleteTaker(int takerId)
        {

        }
    }
}
