using Domain.Core.Bus;
using Domain.Core.CommandHandlers;
using Domain.Core.Interfaces;
using Domain.Core.Notifications;
using Domain.Customers.Commands;
using Domain.Customers.Entities;
using Domain.Customers.Events;
using Domain.Customers.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.CommandHandlers
{
    public class CustomerCommandHandler : CommandHandler,
    IRequestHandler<RegisterNewCustomerCommand, bool>,
    IRequestHandler<UpdateCustomerCommand, bool>,
    IRequestHandler<RemoveCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler _bus;

        public CustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _customerRepository = customerRepository;
            _bus = bus;
        }

        public Task<bool> Handle(RegisterNewCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var customer = new Customer(message.Id, message.DisplayName, message.Email);

            if (_customerRepository.GetByEmail(customer.Email) != null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
                return Task.FromResult(false);
            }

            _customerRepository.Add(customer);

            if (Commit())
            {
                _bus.RaiseEvent(new CustomerRegisteredEvent(customer.Id, customer.DisplayName, customer.Email));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var customer = new Customer(message.Id, message.DisplayName, message.Email);
            var existingCustomer = _customerRepository.GetByEmail(customer.Email);

            if (existingCustomer != null && existingCustomer.Id != customer.Id)
            {
                if (!existingCustomer.Equals(customer))
                {
                    _bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _customerRepository.Update(customer);

            if (Commit())
            {
                _bus.RaiseEvent(new CustomerUpdatedEvent(customer.Id, customer.DisplayName, customer.Email));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            _customerRepository.Remove(message.Id);

            if (Commit())
            {
                _bus.RaiseEvent(new CustomerRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }
    }
}
