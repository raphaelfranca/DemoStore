using System;
using DemoStore.Domain.StoreContext.CustomerCommands.Inputs;
using DemoStore.Domain.StoreContext.CustomerCommands.Outputs;
using DemoStore.Domain.StoreContext.Entities;
using DemoStore.Domain.StoreContext.Repositories;
using DemoStore.Domain.StoreContext.Services;
using DemoStore.Domain.StoreContext.ValueObjects;
using DemoStore.Shared.Commands;
using FluentValidator;

namespace DemoStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler :
        Notifiable,
        ICommandHandler<CreateCustomerCommand>,
        ICommandHandler<AddAddressCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly IEmailService _emailService;

        public CustomerHandler(ICustomerRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateCustomerCommand command)
        {
            // Verificar se o CPF já existe na base
            if (_repository.CheckDocument(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se o E-mail já existe na base
            if (_repository.CheckEmail(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Criar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            // Criar a entidade
            var customer = new Customer(name, document, email, command.Phone);

            // Validar entidades e VOs
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if (Invalid)
                return new CreateCustomerCommandResult(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

            // Persistir o cliente
            _repository.Save(customer);

            // Enviar um E-mail de boas vindas
            _emailService.Send(email.Address, "raphaelfrancabsb@gmail.com", "Bem vindo", "Seja bem vindo ao Balta Store!");

            // Retornar o resultado para tela
            return new CreateCustomerCommandResult(true, "Bem vindo a Store", new
            {
                Id = customer.Id,
                Name = name.ToString(),
                Email = email.Address
            });
        }

        public ICommandResult Handle(AddAddressCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}