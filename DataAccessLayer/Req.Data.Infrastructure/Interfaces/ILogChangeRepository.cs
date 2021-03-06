﻿using Domain.Classes.Req.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Req.Data.Infrastructure.Interfaces
{
    public interface ILogChangeRepository
    {
        IQueryable<ChangeLog> GetChangeLogsByJobId(int jobId);
        void AddChangeLog(ChangeLog changeLog);

        void Save();
    }
}
