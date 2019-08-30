using System;
using System.Collections.Generic;
using DemoStore.Domain.StoreContext.CustomerCommands.Inputs;
using DemoStore.Domain.StoreContext.CustomerCommands.Outputs;
using DemoStore.Domain.StoreContext.Entities;
using DemoStore.Domain.StoreContext.Handlers;
using DemoStore.Domain.StoreContext.Queries;
using DemoStore.Domain.StoreContext.Repositories;
using DemoStore.Domain.StoreContext.ValueObjects;
using DemoStore.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DemoStore.Api.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;
        private readonly CustomerHandler _handler;

        public CustomerController(ICustomerRepository repository, CustomerHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        [Route("v1/customers")]
        [ResponseCache(Duration = 15)]
        public IEnumerable<ListCustomerQueryResult> Get()
        {
            return _repository.Get();
        }

        [HttpGet]
        [Route("v1/customers/{id}")]
        public GetCustomerQueryResult GetById(Guid id)
        {
            return _repository.Get(id);
        }

        [HttpGet]
        [Route("v2/customers/{document}")]
        public GetCustomerQueryResult GetByDocument(Guid document)
        {
            return _repository.Get(document);
        }

        [HttpGet]
        [Route("v1/customers/{id}/orders")]
        public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
        {
            return _repository.GetOrders(id);
        }

        [HttpPost]
        [Route("v1/customers")]
        public ICommandResult Post([FromBody]CreateCustomerCommand command)
        {
            var result = (CreateCustomerCommandResult)_handler.Handle(command);            
            return result;
        }
    }
}