using WorkOrder.Domain.SharedContext.Enums;

namespace WorkOrder.Application.SharedContext.Requests
{
    public class CreateAddressRequest
    {
        public string PublicPlace { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string Complement { get; set; }
        public EAddressType Type { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Country { get; set; }
    }
}
