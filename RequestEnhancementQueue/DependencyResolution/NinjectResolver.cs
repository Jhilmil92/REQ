﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestEnhancementQueue.DependencyResolution
{
    public class NinjectResolver:IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver ()
	    {
            _kernel = new StandardKernel();
            AddBindings();
	    }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {

        }

    }
}