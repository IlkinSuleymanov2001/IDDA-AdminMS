﻿using Application.Common.Pipelines.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public  class NotFoundException:Exception,IException
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException() : base()
        {
        }
    }
}
