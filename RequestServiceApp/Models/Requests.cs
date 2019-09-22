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

        public double GetSum()
        {
            var sum = 0.0;
            foreach (var request in ListOfRequests)
            {
                sum += request.Quantity * request.Price;
            }
            return sum;
        }

        public double GetAvg()
        {
            return GetSum() / GetCount();
        }

        public double GetCount()
        {
            return ListOfRequests.Count;
        }
    }
}
