using System;
using Flunt.Validations;
using WorkOrder.Domain.SharedContext.Enums;
using WorkOrder.Shared.Entities;

namespace WorkOrder.Domain.SharedContext.Entities
{
    public class Address : TenantEntity, IValidatable
    {
        protected Address()
        {

        }

        private string _publicPlace;
        private string _neighborhood;
        private string _city;
        private string _state;
        private string _number;
        private string _zipCode;
        private string _complement;
        private string _latitude;
        private string _longitude;
        private string _country;
        private EAddressType _addressType;

        public Address(Guid tenantId, string publicPlace, string neighborhood, string city, string state, string number, string zipCode, string complement)
        {
            SetTenantId(tenantId);
            _publicPlace = publicPlace;
            _neighborhood = neighborhood;
            _city = city;
            _state = state;
            _number = number;
            _zipCode = zipCode.Trim().Replace(".", "").Replace("-", "");
            Complement = complement;
            AddressType = EAddressType.Default;
            Validate();
        }

        public Address(Guid tenantId, string publicPlace, string neighborhood, string city, string state, string number, string zipCode, string complement, string country)
            : this(tenantId, publicPlace, neighborhood, city, state, number, zipCode, complement)
        {
            _country = country;
        }

        public Address(Guid tenantId, string publicPlace, string neighborhood, string city, string state, string number, string zipCode, string complement, string latitude, string longitude)
            : this(tenantId, publicPlace, neighborhood, city, state, number, zipCode, complement)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        public Address(Guid tenantId, string publicPlace, string neighborhood, string city, string state, string number, string zipCode, string complement, string country, string latitude, string longitude)
            : this(tenantId, publicPlace, neighborhood, city, state, number, zipCode, complement, latitude, longitude)
        {
            _country = country;
        }

        public string PublicPlace
        {
            get => _publicPlace;
            set
            {
                _publicPlace = value;

            }
        }

        public string Neighborhood
        {
            get => _neighborhood;
            set
            {
                _neighborhood = value;

            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;

            }
        }

        public string State
        {
            get => _state;
            set
            {
                _state = value;

            }
        }

        public string Number
        {
            get => _number;
            set
            {
                _number = value;

            }
        }

        public string ZipCode
        {
            get => _zipCode;
            set
            {

                _zipCode = value.Trim().Replace(".", "").Replace("-", "");
            }
        }

        public string Complement
        {
            get => _complement;
            set
            {

                _complement = value;
            }
        }

        public EAddressType AddressType
        {
            get => _addressType;
            set => _addressType = value;
        }

        public string Latitude { get => _latitude; set => _latitude = value; }
        public string Longitude { get => _longitude; set => _longitude = value; }
        public string Country { get => _country; set => _country = value; }


        public override string ToString()
        {
            return $"{_publicPlace} - {_number}, {_neighborhood}, {_zipCode}, {_city} - {_state}.";
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(PublicPlace, "PublicPlace", "O campo logradouro é obrigatório!")
                .IsNotNullOrEmpty(Neighborhood, "Neighborhood", "O campo bairro é obrigatório!")
                .IsNotNullOrEmpty(City, "City", "O campo cidade é obrigatório!")
                .IsNotNullOrEmpty(State, "State", "O campo estado é obrigatório!")
                .IsNotNullOrEmpty(Number, "Number", "O campo número é obrigatório!")
                .IsNotNullOrEmpty(ZipCode, "ZipCode", "O campo cep é obrigatório"))
                ;
        }
    }
}
