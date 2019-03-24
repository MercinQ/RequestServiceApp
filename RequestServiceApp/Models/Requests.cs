using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RequestServiceApp.Models
{
    class Requests
    {
        [XmlElement("request")]
        [JsonProperty("requests")]
        public List<Request> ListOfRequests { get; set; }
        public int RequestCount
        {
            get
            {
                return ListOfRequests.Count;
            }
        }
        public double RequestsSum
        {
            get
            {
                double sum = Sum();
                return sum;
            }
        }
        public double RequestAvg
        {
            get
            {
                double avg = Sum() / ListOfRequests.Count;
                return avg;
            }
        }

        public Requests()
        {
            ListOfRequests = new List<Request>();
        }


        public double Sum()
        {
            double sum = 0.0;
            foreach (Request request in ListOfRequests)
            {
                sum += request.Quantity * request.Price;
            }
            return sum;
        }
    }
}
