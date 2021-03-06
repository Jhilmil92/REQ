﻿using Domain.Classes;
using Domain.Classes.Req.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BLL.Interfaces
{
    public interface IStakeHolderBLL
    {
        void CreateStakeHolder(StakeHolderRegistrationViewModel registrationViewModel);
        IQueryable<StakeHolder> GetStakeHolders();
        StakeHolder GetStakeHolderById(int stakeHolderId);
        void UpdateStakeHolder(StakeHolder stakeHolder);
        void DeleteStakeHolder(int stakeHolderID);
    }
}
