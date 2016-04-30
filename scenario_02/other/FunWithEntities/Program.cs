﻿using System;
using Infrastructure.Messaging;
using Payroll.Domain.CommandHandlers;
using Payroll.Domain.Commands;
using Payroll.Domain.Model;
using Payroll.Domain.Repositories;

namespace FunWithEntities
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new DummyBus();
            var repository = new InMemoryESEmployeeRepository(bus);
            var handler = new EmployeeCommandsHandler(repository);

            var employeeId = Guid.NewGuid();

            handler.Handle(new RegisterEmployeeCommand(
                employeeId,
                new FullName("John", "Doe"), 
                150M
                ));

            handler.Handle(new ChangeHomeAddressCommand(employeeId, BrazilianAddress.Factory.New(
                street: "Somewhere avenue",
                number: 42,
                addtionalInfo: null,
                neighborhood: "Bellvue",
                city: "Bigtown",
                state: "RS",
                postalCode: "95000-000")
                )); 

            handler.Handle(new RaiseSalaryCommand(employeeId, 25M));

            var employee = repository.Load(employeeId);

            Console.WriteLine("Done!");
        }
    }

    public class DummyBus : IBus
    {
        public void SendCommand(object theCommand)
        {
            //throw new System.NotImplementedException();
        }

        public void RaiseEvent(object theEvent)
        {
            //throw new System.NotImplementedException();
        }

        public void RegisterHandler<T>()
        {
            //throw new System.NotImplementedException();
        }
    }

}