﻿using Domain.Customers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Validatons
{
    public class RemoveCustomerCommandValidation : CustomerValidation<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidation()
        {
            ValidateId();
        }
    }
}
