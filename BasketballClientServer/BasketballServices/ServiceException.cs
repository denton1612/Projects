﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballServices
{
    public class ServiceException: Exception
    {
        public ServiceException() { }

        public ServiceException(string message) : base(message) { }

    }
}
