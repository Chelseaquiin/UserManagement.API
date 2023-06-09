﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Implementations
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public T GetService<T>() where T : class
        {
            if (_serviceProvider.GetService(typeof(T)) is not T service)
                throw new InvalidOperationException("Type Not Supported");
            return service;
        }
    }
}
