using Autofac;
using BusinessLogicLayer.BLL.Interfaces;
using DataAccessLayer.Infrastructure.Classes;
using DataAccessLayer.Infrastructure.Interfaces;
using DataAccessLayer.Req.Data.Infrastructure.Classes;
using DataAccessLayer.Req.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
   public class BusinessLayerModuleDependencies : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JobBLL>().As<IJobBLL>();
            builder.RegisterType<JobRepository>().As<IJobRepository>();
            builder.RegisterType<StakeHolderRepository>().As<IStakeHolderRepository>();
            builder.RegisterType<TakerRepository>().As<ITakerRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
        }
    }
}
