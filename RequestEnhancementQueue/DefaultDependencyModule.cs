using Autofac;
using BusinessLogicLayer;
using BusinessLogicLayer.BLL.Classes;
using BusinessLogicLayer.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RequestEnhancementQueue
{
    public class DefaultDependencyModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileBLL>().As<IFileBLL>();
            builder.RegisterType<JobBLL>().As<IJobBLL>();
             builder.RegisterType<StakeHolderBLL>().As<IStakeHolderBLL>();
             builder.RegisterType<TakerBLL>().As<ITakerBLL>();
             builder.RegisterType<LoginManager>().As<ILoginManager>();

            builder.RegisterModule<BusinessLayerModuleDependencies>();

        }
    }
}