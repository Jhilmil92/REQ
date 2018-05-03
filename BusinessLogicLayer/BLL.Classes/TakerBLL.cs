using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Classes;
using DataAccessLayer.Infrastructure.Interfaces;
using Domain.Classes;
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
        public TakerBLL()
        {
            _takerRepository = new TakerRepository();
        }

        //public TakerBLL(ITakerRepository takerRepository)
        //{
        //    _takerRepository = takerRepository;
        //}
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


        public Domain.Classes.Taker ValidateLoginCredentials(Domain.Classes.Req.Domain.ViewModels.LoginViewModel loginViewModel)
        {
            //fetch login credentials from the database and check it against the view model.
            //Return bool status accordingly.
            bool isValid = false;
            Taker taker = null;
            IQueryable<Taker> takers = _takerRepository.GetTakers();
            if (takers != null && takers.Count() != 0)
            {
                taker = takers.SingleOrDefault((d => (d.UserName == loginViewModel.UserName && d.Password == loginViewModel.Password)));
                if (taker!= null)
                {
                    isValid = true;
                }
            }
            if (isValid)
            {
                return taker;
            }
            else
            {
                return null;
            }
        }
    }
}
