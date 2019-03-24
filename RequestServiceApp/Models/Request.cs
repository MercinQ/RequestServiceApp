using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace RequestServiceApp.Models
{
    public class Request
    {
        [Required()]
        [MaxLength(6), RegularExpression(@"^\S*$")]
        [XmlElement("clientId")]
        public string ClientId { get; set; }

        [Required()]
        public long RequestId { get; set; }

        [Required()]
        [MaxLength(6)]
        [XmlElement("name")]
        public string Name { get; set; }

        [Required()]
        [MaxLength(6)]
        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [Required()]
        [MaxLength(6)]
        [XmlElement("price")]
        public double Price { get; set; }

        static public Request CsvToObject(string csvText)
        {
            string[] values = csvText.Split(',');
            Request order = new Request
            {
                ClientId = values[0],
                RequestId = (long)Convert.ToDouble(values[1]),
                Name = values[2],
                Quantity = Convert.ToInt32(values[3]),
                Price = double.Parse(values[4], CultureInfo.InvariantCulture)
            };
            return order;
        }

        public static List<Request> JsonToObjectList(string file)
        {
            string jsonText = File.ReadAllText(file);
            var requests = JsonConvert.DeserializeObject<Requests>(jsonText);

            List<Request> listToReturn = new List<Request>();
            listToReturn = requests.ListOfRequests;

            return listToReturn;
        }
        public static List<Request> XmlToObjectList(string file)
        {
            string xmlText = File.ReadAllText(file);
            List<Request> listToReturn = new List<Request>();

            XmlSerializer serializer = new XmlSerializer(typeof(Requests));
            using (TextReader reader = new StringReader(xmlText))
            {
                Requests result = (Requests)serializer.Deserialize(reader);
                listToReturn = result.ListOfRequests;
            }
            return listToReturn;
        }
    }
}
