using BusinessLogicLayer.BLL.Interfaces;
using Domain.Classes;
using Req.Enums.Req.Common.Datastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer.BLL.Classes
{
    public class JobQueueService : IJobQueueService
    {
        private static JobQueueService instance;
        private readonly PriorityQueue<Job> _priorityQueue;
        private const int jobQueueCount = 50;
        private IJobBLL _jobBLL;

        private JobQueueService(IJobBLL _jobBLL)
        {
            _priorityQueue = new PriorityQueue<Job>(jobQueueCount,true);
            this._jobBLL = _jobBLL;
            InitializeQueue();
        }

        public static JobQueueService GetInstance()
        {
            if (instance == null)
            {
                instance = new JobQueueService(_jobBLL);
            }

            return instance;
        }


        public PriorityQueue<Job> PriorityQue
        {
            get
            {
                return _priorityQueue;
            }
        }

        public void Enqueue(Job item)
        {
            _priorityQueue.Enqueue(item);
        }

        public Job Dequeue()
        {
            return _priorityQueue.Dequeue();
        }


        private void InitializeQueue()
        {
            var idleJobs = _jobBLL.GetJobs().Where(d => d.AssignedTo == null);
            foreach(var job in idleJobs)
            {
                Enqueue(job);
            }
        }
    }
}
