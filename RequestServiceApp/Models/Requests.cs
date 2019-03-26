using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RequestServiceApp.Models
{
    [XmlRoot("requests")]
    public class Requests
    {
        [XmlElement("request")]
        [JsonProperty("requests")]
        public List<Request> ListOfRequests { get; set; }

        public Requests()
        {
            ListOfRequests = new List<Request>();
        }
    }
}
