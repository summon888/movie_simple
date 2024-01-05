using Application.EventSourcedNormalizers;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        void Register(CustomerViewModel customerViewModel);

        IEnumerable<CustomerViewModel> GetAll();

        IEnumerable<CustomerViewModel> GetAll(int skip, int take);

        CustomerViewModel GetById(Guid id);

        void Update(CustomerViewModel customerViewModel);

        void Remove(Guid id);

        IList<CustomerHistoryData> GetAllHistory(Guid id);
    }
}
