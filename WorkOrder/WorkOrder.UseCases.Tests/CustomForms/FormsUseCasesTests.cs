using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WorkOrder.Application.CustomFormsContext.Handlers;
using WorkOrder.Application.CustomFormsContext.Repositories;
using WorkOrder.Application.CustomFormsContext.Requests;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Domain.CustomFormsContext.Enums;
using WorkOrder.Shared.Entities;

namespace WorkOrder.UseCases.Tests.CustomForms
{
    [TestClass]
    public class FormsUseCasesTests
    {

        private AppTenant _tenant;
        private CustomFormsHandler _handler;
        private PageComponent _pageComponent;

        private List<CustomForm> _customForms;
        private List<CustomField> _customFields;
        private List<CustomFieldOption> _customFieldOptions;
        private List<FormPageComponent> _formPageComponents;
        private List<CustomFormAnswer> _customFormAnswers;

        private ICustomFormRepository _customFormRepository;
        private Mock<ICustomFormRepository> _mockCustomFormRepository;

        private ICustomFieldRepository _customFieldRepository;
        private Mock<ICustomFieldRepository> _mockCustomFieldRepository;

        private ICustomFieldOptionRepository _customFieldOptionRepsitory;
        private Mock<ICustomFieldOptionRepository> _mockCustomFieldOptionRepository;

        private IFormPageComponentRepository _formPageComponentRepository;
        private Mock<IFormPageComponentRepository> _mockFormPageComponentRepository;

        private ICustomFormAnswerRepository _customFormAnswerRepository;
        private Mock<ICustomFormAnswerRepository> _mockCustomFormAnswerRepository;

        [TestInitialize]
        public void Setup()
        {
            _tenant = new AppTenant("Treeze", "localhost:43500", null, null);

            _pageComponent = new PageComponent("Minha Página");

            _customForms = new List<CustomForm>();
            _customFields = new List<CustomField>();
            _customFieldOptions = new List<CustomFieldOption>();
            _formPageComponents = new List<FormPageComponent>();
            _customFormAnswers = new List<CustomFormAnswer>();

            _mockCustomFormRepository = new Mock<ICustomFormRepository>();
            _mockCustomFieldRepository = new Mock<ICustomFieldRepository>();
            _mockCustomFieldOptionRepository = new Mock<ICustomFieldOptionRepository>();
            _mockFormPageComponentRepository = new Mock<IFormPageComponentRepository>();
            _mockCustomFormAnswerRepository = new Mock<ICustomFormAnswerRepository>();


            _mockCustomFormRepository.Setup(mr => mr.Save(It.IsAny<CustomForm>())).Callback((CustomForm target) =>
            {
                _customForms.Add(target);
                if (target.Fields == null) return;
                foreach (var targetField in target.Fields)
                {
                    _customFields.Add(targetField);

                    if (targetField.Options == null) continue;
                    foreach (var targetFieldOption in targetField.Options)
                    {
                        _customFieldOptions.Add(targetFieldOption);
                    }
                }
            });

            _mockCustomFieldRepository.Setup(mr => mr.Save(It.IsAny<CustomField>())).Callback((CustomField target) => { _customFields.Add(target); });
            _mockCustomFieldRepository.Setup(mr => mr.Get(It.IsAny<Guid>())).Returns((Guid id) => _customFields.FirstOrDefault(x => x.Id == id && x.TenantId == _tenant.Id));

            _mockCustomFieldOptionRepository.Setup(mr => mr.Save(It.IsAny<CustomFieldOption>())).Callback((CustomFieldOption target) => { _customFieldOptions.Add(target); });
            _mockFormPageComponentRepository.Setup(mr => mr.Save(It.IsAny<FormPageComponent>())).Callback((FormPageComponent target) => { _formPageComponents.Add(target); });
            _mockCustomFormAnswerRepository.Setup(mr => mr.Save(It.IsAny<CustomFormAnswer>())).Callback((CustomFormAnswer target) => { _customFormAnswers.Add(target); });



            _customFormRepository = _mockCustomFormRepository.Object;
            _customFieldRepository = _mockCustomFieldRepository.Object;
            _customFieldOptionRepsitory = _mockCustomFieldOptionRepository.Object;
            _formPageComponentRepository = _mockFormPageComponentRepository.Object;
            _customFormAnswerRepository = _mockCustomFormAnswerRepository.Object;

            _handler = new CustomFormsHandler(_tenant, _customFormRepository, _customFieldRepository, _customFieldOptionRepsitory, _formPageComponentRepository, _customFormAnswerRepository);
        }

        [TestMethod]
        public void Deve_Criar_Um_Novo_Formulario()
        {

            var command = new CreateCustomFormRequest()
            {
                Name = "Meu Formulário",
                Fields = new List<CreateCustomFieldRequest>()
                {
                    new CreateCustomFieldRequest()
                    {
                        Name = "Campo 1",
                        Description = "Descricao Campo 1",
                        Mandatory = false,
                        Type = EFieldType.TextBox
                    },
                    new CreateCustomFieldRequest()
                    {
                        Name = "Campo 2",
                        Mandatory = true,
                        Type = EFieldType.CheckBox
                    },
                    new CreateCustomFieldRequest()
                    {
                        Name = "Campo 3",
                        Description = "Descricao Campo 3",
                        Mandatory = true,
                        Type = EFieldType.ComboBox,
                        Options = new List<CreateCustomFieldOptionRequest>()
                        {
                            new CreateCustomFieldOptionRequest()
                            {
                                Name = "Opção 1 - Campo 3"
                            },
                            new CreateCustomFieldOptionRequest()
                            {
                                Name = "Opção 2 - Campo 3"
                            },
                            new CreateCustomFieldOptionRequest()
                            {
                                Name = "Opção 3 - Campo 3"
                            }
                        }
                    },
                }

            };

            _handler.Handle(command);

            Assert.IsTrue(_handler.Valid);
            Assert.AreEqual(1, _customForms.Count);

        }

        [TestMethod]
        public void Deve_Responder_Um_Formulario()
        {

            Deve_Criar_Um_Novo_Formulario();

            var customForm = _customForms.First();

            var request = new SendCustomFormRequest()
            {
                CustomFormId = customForm.Id,
                Fields = new List<SendCustomFieldRequest>()
            };

            foreach (var formularioField in customForm.Fields)
            {
                request.Fields.Add(new SendCustomFieldRequest()
                {
                    Answer = formularioField.HasOptions() ? (formularioField.Mandatory ? formularioField.Options.First().Id.ToString() : null) : (formularioField.Mandatory ? "Resposta do campo" : ""),
                    CustomFieldId = formularioField.Id,
                    CustomFieldOptionId = formularioField.HasOptions() ? formularioField.Options.First().Id : (Guid?)null
                });

            }

            _handler.Handle(request);

            Assert.IsTrue(_handler.Valid);
            Assert.AreEqual(1, _customFormAnswers.Count);

        }


    }
}
