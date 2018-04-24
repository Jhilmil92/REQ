using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Classes
{
    public class TakerBLL : ITakerBLL
    {
        private readonly ITakerRepository _takerRepository;
        public TakerBLL(ITakerRepository takerRepository)
        {
            _takerRepository = takerRepository;
        }
        public void CreateTaker(Domain.Classes.Taker taker)
        {
            _takerRepository.Create(taker);
        }

        public IQueryable<Domain.Classes.Taker> GetTakers()
        {
            return _takerRepository.GetTakers();
        }

        public Domain.Classes.Taker GetTakerById(int takerId)
        {
            var taker = _takerRepository.GetTakerById(takerId);
            return taker;
        }

        public void UpdateTaker(Domain.Classes.Taker taker)
        {
            _takerRepository.Update(taker);
        }

        public void DeleteTaker(int takerId)
        {
            _takerRepository.Delete(takerId);
        }
    }
}
