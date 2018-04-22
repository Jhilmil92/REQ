using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Interfaces
{
    public interface ITakerRepository
    {
        void CreateTaker(Taker taker);
        void EditTaker(Taker taker);
        Taker GetTakerById(int takerID);
        IQueryable<Taker> GetTakers();
        void DeleteTaker(int takerId);
    }
}
