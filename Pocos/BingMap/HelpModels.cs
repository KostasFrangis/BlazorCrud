using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocos.BingMap
{
    public class Address
    {
        public string countryRegion { get; set; }
        public string locality { get; set; }
        public string adminDistrict { get; set; }
        public string countryRegionIso2 { get; set; }
        public string postalCode { get; set; }
        public string addressLine { get; set; }
        public string formattedAddress { get; set; }
        public string adminDistrict2 { get; set; }
    }

    public class Resource
    {
        public string __type { get; set; }
        public List<Value> value { get; set; }
    }

    public class ResourceSet
    {
        public int estimatedTotal { get; set; }
        public List<Resource> resources { get; set; }
    }

    public class Root
    {
        public string authenticationResultCode { get; set; }
        public string brandLogoUri { get; set; }
        public string copyright { get; set; }
        public List<ResourceSet> resourceSets { get; set; }
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public string traceId { get; set; }
    }

    public class Value
    {
        public string __type { get; set; }
        public Address address { get; set; }
        public string name { get; set; }
    }
}
