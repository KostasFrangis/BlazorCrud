using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml;

namespace Pocos
{
    public class Customer
    {
        [Key]
        [Required]
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string? CompanyName { get; set; }
        [DataMember]
        public string? ContactName { get; set; }
        [DataMember]
        public string? Address { get; set; }
        [DataMember]
        public string? City { get; set; }
        [DataMember]
        public string? Region { get; set; }
        [DataMember]
        public string? PostalCode { get; set; }
        [DataMember]
        public string? Country { get; set; }
        [DataMember]
        public string? Phone { get; set; }
    }
}