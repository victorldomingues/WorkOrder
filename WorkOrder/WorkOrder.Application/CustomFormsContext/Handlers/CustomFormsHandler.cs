using System;
using Flunt.Notifications;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Application.CustomFormsContext.Requests;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Domain.CustomFormsContext.Enums;
using WorkOrder.Shared.Commands;
using WorkOrder.Shared.Commands.Interfaces;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Application.CustomFormsContext.Handlers
{
    public class CustomFormsHandler : Notifiable,
        ICommandHandler<CreateCustomFormRequest>,
        ICommandHandler<UpdateCustomFormRequest>,
        ICommandHandler<SendCustomFormRequest>
    {

        private readonly AppTenant _tenant;

        private readonly ICustomFormRepository _customFormRepository;

        private readonly ICustomFieldRepository _customFieldRepository;

        private readonly ICustomFieldOptionRepository _customFieldOptionRepository;

        private readonly ICustomFormAnswerRepository _customFormAnswerRepository;

        private readonly IFormPageComponentRepository _formPageComponentRepository;

        public CustomFormsHandler(AppTenant tenant, ICustomFormRepository customFormRepository, ICustomFieldRepository customFieldRepository, ICustomFieldOptionRepository customFieldOptionRepository, IFormPageComponentRepository formPageComponentRepository, ICustomFormAnswerRepository customFormAnswerRepository)
        {
            _customFormRepository = customFormRepository;
            _customFieldRepository = customFieldRepository;
            _customFieldOptionRepository = customFieldOptionRepository;
            _formPageComponentRepository = formPageComponentRepository;
            _customFormAnswerRepository = customFormAnswerRepository;
            _tenant = tenant;
        }

        public ICommandResult Handle(CreateCustomFormRequest command)
        {

            if (!command.IsValid())
                return new CommandResult(false, "Request inválida", command);

            var customForm = new CustomForm(command.Name, _tenant.Id);

            AddNotifications(customForm.Notifications);

            if (command.Fields != null)
                foreach (var fieldCommand in command.Fields)
                {

                    var customField = new CustomField(fieldCommand.Name, fieldCommand?.Description, fieldCommand.Type, customForm.Id, fieldCommand.Mandatory, _tenant.Id);

                    AddNotifications(customField.Notifications);

                    if (customField.HasOptions())
                    {
                        if (fieldCommand.Options == null || fieldCommand.Options.Count == 0)
                            AddNotification("Type", $"Campo {fieldCommand.Name} deve conter opções.");
                    }

                    if (fieldCommand.Options != null)
                        foreach (var optionCommand in fieldCommand.Options)
                        {
                            var customFieldOption =
                                new CustomFieldOption(optionCommand.Name, customField.Id, _tenant.Id);

                            AddNotifications(customFieldOption.Notifications);

                            customField.AddOption(customFieldOption);
                        }

                    customForm.AddField(customField);

                }
            else
                AddNotification("Fields", "Os campos do formulário não foram inseridos.");

            if (Valid)
            {
                _customFormRepository.Save(customForm);

                if (command.PageComponentId.HasValue)
                {
                    var formPageComponent = new FormPageComponent(command.PageComponentId.Value, customForm.Id, _tenant.Id);

                    AddNotifications(formPageComponent.Notifications);

                    _formPageComponentRepository.Save(formPageComponent);
                }

            }

            if (Valid)
                return new CommandResult(true, "Formulário cadastrado com sucesso", new { Id = customForm.Id, Name = customForm.Name });
            else
                return new CommandResult(false, "Erro ao cadastrar formulário", Notifications);
        }

        public ICommandResult Handle(UpdateCustomFormRequest command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(SendCustomFormRequest command)
        {

            if (!command.IsValid())
                return new CommandResult(false, "Request inválida", command);

            var customFormAnswer = new CustomFormAnswer(command.CustomFormId, _tenant.Id);

            AddNotifications(customFormAnswer);

            foreach (var fieldCommand in command.Fields)
            {

                var customField = _customFieldRepository.Get(fieldCommand.CustomFieldId);

                if (customField.Mandatory)
                {
                    if (!customField.HasOptions())
                    {
                        if (string.IsNullOrEmpty(fieldCommand.Answer))
                            AddNotification("Answer", $"O Campo {customField.Name} é obrigatório");
                    }
                    else
                    {
                        if (fieldCommand.CustomFieldOptionId == null)
                            AddNotification("CustomFieldOptionId", $"O Campo {customField.Name} deve ser selecionado.");
                    }
                }


                var customFieldAnswer = new CustomFieldAnswer(fieldCommand.CustomFieldId, fieldCommand?.Answer, customFormAnswer.Id, fieldCommand?.CustomFieldOptionId, _tenant.Id);

                AddNotifications(customFieldAnswer.Notifications);

                customFormAnswer.AddField(customFieldAnswer);

            }

            if (Valid)
                _customFormAnswerRepository.Save(customFormAnswer);

            if (Valid)
                return new CommandResult(true, "Resposta do formulário cadastrada com sucesso", new { Id = customFormAnswer.Id });
            else
                return new CommandResult(false, "Erro ao cadastrar resposta do formulário", Notifications);

        }

    }
}
