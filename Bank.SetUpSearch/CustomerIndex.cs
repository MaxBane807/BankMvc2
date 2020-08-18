using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Bank.SetUpSearch
{
    class CustomerIndex
    {
        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; }

        [JsonPropertyName("givenName")]
        public string Givenname { get; set; }
       
        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
        
        [JsonPropertyName("streetAddress")]
        public string Streetaddress { get; set; }

        [JsonPropertyName("nationalId")]
        public string NationalId { get; set; }
    }
}
