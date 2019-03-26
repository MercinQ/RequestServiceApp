using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
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
        [XmlElement("requestId")]
        public long RequestId { get; set; }

        [Required()]
        [MaxLength(255)]
        [XmlElement("name")]
        public string Name { get; set; }

        [Required()]
        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [Required()]
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

            return ListFilter(listToReturn);
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
            return ListFilter(listToReturn);
        }

        public static List<Request> ListFilter(List<Request> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if(!(list[i].ClientId.Length <= 6 && Regex.Match(list[i].ClientId, @"^\S*$").Success))
                {
                    list.RemoveAt(i);
                }

                if (!(list[i].Name.Length <= 255))
                {
                    list.RemoveAt(i);
                }

            }
            return list;
        }


    }
}
