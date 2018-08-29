using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOrder.Domain.CustomFormsContext.Entities;
using WorkOrder.Domain.CustomFormsContext.Enums;
using WorkOrder.Shared.Entities;
using WorkOrder.Shared.Enums;

namespace WorkOrder.Domain.Tests.CustomFormsContext
{
    [TestClass]
    public class CustomFormsUnitTests
    {
        private AppTenant _tenant;
        private CustomForm _customForm;

        [TestInitialize]
        public void Setup()
        {
            _tenant = new AppTenant("Treeze", "localhost:43500", null, null);
            _customForm = new CustomForm("Novo formulário", _tenant.Id);
        }

        [TestMethod]
        public void Deve_Criar_Um_Formulario_Dinamico()
        {
            _customForm = null;
            _customForm = new CustomForm("Novo formulário", _tenant.Id);
            Assert.IsTrue(_customForm.Valid);
            Assert.AreEqual(_customForm.EntityStatus, EntityStatus.Activated);
            Assert.AreEqual(_tenant.Id, _customForm.TenantId);
        }

        [TestMethod]
        public void Deve_Criar_Campos_Do_Formulario()
        {
            for (int i = 0; i < 9; i++)
            {
                var field = new CustomField($"Campo {i + 1}", $"Campo {i + 1} descricao", (EFieldType)i, _customForm.Id, true, _tenant.Id);
                _customForm.AddField(field);

                Assert.IsTrue(field.Valid);
                Assert.AreEqual(field.EntityStatus, EntityStatus.Activated);
                Assert.AreEqual(_customForm.Id, field.CustomFormId);
                Assert.AreEqual(_tenant.Id, field.TenantId);


            }
            Assert.IsTrue(_customForm.Valid);
            Assert.AreEqual(9, _customForm.Fields.Count);
        }

        [TestMethod]
        public void Deve_Criar_Campos_Do_Formulario_Com_Opcoes()
        {
            for (int i = 0; i < 9; i++)
            {
                var field = new CustomField($"Campo {i + 1}", $"Campo {i + 1} descricao", (EFieldType)i, _customForm.Id, false, _tenant.Id);

                for (int j = 0; j < 3; j++)
                {
                    var option = new CustomFieldOption($"Opção { j + 1} - Campo {i + 1}", field.Id, _tenant.Id);
                    field.AddOption(option);
                    Assert.IsTrue(option.Valid);
                    Assert.AreEqual(option.EntityStatus, EntityStatus.Activated);
                    Assert.AreEqual(field.Id, option.CustomFieldId);
                    Assert.AreEqual(_tenant.Id, option.TenantId);

                }

                Assert.IsTrue(field.Valid);
                Assert.AreEqual(field.EntityStatus, EntityStatus.Activated);
                Assert.AreEqual(_customForm.Id, field.CustomFormId);
                Assert.AreEqual(_tenant.Id, field.TenantId);
                Assert.AreEqual(3, field.Options.Count);

                _customForm.AddField(field);
            }
            Assert.IsTrue(_customForm.Valid);
            Assert.AreEqual(9, _customForm.Fields.Count);

        }

        [TestMethod]
        public void Deve_Vincular_Um_Formulario_A_Um_Componente()
        {
            var pageComponent = new PageComponent("Novo componente");

            Assert.IsTrue(pageComponent.Valid);
            Assert.AreEqual(pageComponent.EntityStatus, EntityStatus.Activated);


            var formPageComponent = new FormPageComponent(pageComponent.Id, _customForm.Id, _tenant.Id);

            Assert.IsTrue(formPageComponent.Valid);
            Assert.AreEqual(formPageComponent.EntityStatus, EntityStatus.Activated);
            Assert.AreEqual(_tenant.Id, formPageComponent.TenantId);
            Assert.AreEqual(pageComponent.Id, formPageComponent.PageComponentId);
        }

        [TestMethod]
        public void Teste_De_Enumeradores_EFieldType()
        {
            Assert.AreEqual(0, EFieldType.TextBox.GetHashCode());
            Assert.AreEqual(1, EFieldType.TextArea.GetHashCode());
            Assert.AreEqual(2, EFieldType.ComboBox.GetHashCode());
            Assert.AreEqual(3, EFieldType.CheckBox.GetHashCode());
            Assert.AreEqual(4, EFieldType.Radio.GetHashCode());
            Assert.AreEqual(5, EFieldType.DateTime.GetHashCode());
            Assert.AreEqual(6, EFieldType.DatePicker.GetHashCode());
        }

        [TestMethod]
        public void Deve_Criar_Uma_Resposta_De_Formulario()
        {
            Deve_Criar_Campos_Do_Formulario_Com_Opcoes();
            var customFormAnswer = new CustomFormAnswer(_customForm.Id, _tenant.Id);
            foreach (var customFormField in _customForm.Fields)
            {
                var option = customFormField.Options.FirstOrDefault();
                var answer = customFormField.HasOptions() ? option?.Id.ToString() : "Resposta Pergunta";
                var customFieldAnswer = new CustomFieldAnswer(customFormField.Id, answer, customFormAnswer.Id, option?.Id, _tenant.Id);
                customFormAnswer.AddField(customFieldAnswer);
                Assert.IsTrue(customFieldAnswer.Valid);
                Assert.AreEqual(_tenant.Id, customFieldAnswer.TenantId);
                Assert.AreEqual(customFieldAnswer.EntityStatus, EntityStatus.Activated);
            }
            Assert.IsTrue(customFormAnswer.Valid);
            Assert.AreEqual(_tenant.Id, customFormAnswer.TenantId);
            Assert.AreEqual(customFormAnswer.EntityStatus, EntityStatus.Activated);
        }
    }
}
