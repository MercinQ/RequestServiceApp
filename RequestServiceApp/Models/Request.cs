﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace RequestServiceApp.Models
{
    public class Request
    {
        [XmlElement("clientId")]
        public string ClientId { get; set; }

        [XmlElement("requestId")]
        public long RequestId { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }
   
        [XmlElement("price")]
        public double Price { get; set; }


        public List<Request> CsvToObjectList(string file)
        {
            var listToReturn = File.ReadAllLines(file)
                            .Skip(1)
                            .Select(p => CsvToObject(p))
                            .ToList();

            return ListFilter(listToReturn);
        }

        public List<Request> JsonToObjectList(string file)
        {
            var jsonText = File.ReadAllText(file);
            var requests = JsonConvert.DeserializeObject<Requests>(jsonText);

            var listToReturn = new List<Request>();
            listToReturn = requests.ListOfRequests;

            return ListFilter(listToReturn);
        }

        public List<Request> XmlToObjectList(string file)
        {
            var xmlText = File.ReadAllText(file);
            var listToReturn = new List<Request>();
            var serializer = new XmlSerializer(typeof(Requests));

            using (TextReader reader = new StringReader(xmlText))
            {
                Requests result = (Requests)serializer.Deserialize(reader);
                listToReturn = result.ListOfRequests;
            }
            return ListFilter(listToReturn);
        }

        public List<Request> ListFilter(List<Request> list)
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

        private Request CsvToObject(string csvText)
        {
            var values = csvText.Split(',');
            var request = new Request
            {
                ClientId = values[0],
                RequestId = (long)Convert.ToDouble(values[1]),
                Name = values[2],
                Quantity = Convert.ToInt32(values[3]),
                Price = double.Parse(values[4], CultureInfo.InvariantCulture)
            };
            return request;
        }
    }
}
