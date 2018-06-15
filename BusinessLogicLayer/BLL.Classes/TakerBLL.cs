using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Classes;
using DataAccessLayer.Infrastructure.Interfaces;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using Domain.Classes;
using Domain.Classes.Req.Domain.Entities;
using Domain.Classes.Req.Domain.ViewModels;
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
        private readonly IUserRepository _userRepository;
        public TakerBLL(ITakerRepository takerRepository,IUserRepository userRepository)
        {
            _takerRepository = takerRepository;
            _userRepository = userRepository;
        }

        //public TakerBLL(ITakerRepository takerRepository)
        //{
        //    _takerRepository = takerRepository;
        //}
        public void CreateTaker(TakerRegistrationViewModel registrationViewModel)
        {
            var taker = new Taker
            {
                TakerName = registrationViewModel.TakerName
            };
           var createdTaker = _takerRepository.Create(taker);
           _userRepository.CreateUser(registrationViewModel.TakerUserName, registrationViewModel.TakerPassword,UserType.Taker,createdTaker.TakerId);
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
