using Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Interfaces
{
    public interface ITakerBLL
    {
        void CreateTaker(Taker taker);
        IQueryable<Taker> GetTakers();
        Taker GetTakerById(int takerId);
        void UpdateTaker(Taker taker);
        void DeleteTaker(int takerId);
    }
}
