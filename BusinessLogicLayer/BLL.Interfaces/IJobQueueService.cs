using Domain.Classes;
using Req.Enums.Req.Common.Datastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Interfaces
{
    public interface IJobQueueService
    {
        void Enqueue(Job job);

        Job Dequeue();

        PriorityQueue<Job> PriorityQue
        {
            get;

        }
    }
}
