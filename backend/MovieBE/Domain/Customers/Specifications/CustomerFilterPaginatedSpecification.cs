using Domain.Core.Specifications;
using Domain.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Specifications
{
    public class CustomerFilterPaginatedSpecification : BaseSpecification<Customer>
    {
        public CustomerFilterPaginatedSpecification(int skip, int take)
            : base(i => true)
        {
            ApplyPaging(skip, take);
        }
    }
}
