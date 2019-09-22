using Newtonsoft.Json;
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


    }
}
