using RequestServiceApp.Models;
using RequestServiceApp.View.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RequestServiceApp.Services
{
    public class DbService
    {
        public RequestsRaportViewModel GetRequestsRaportViewModel(string id, double? minPrice, double? maxPrice, bool groupByName, Requests requests)
        {


            if (id != null && minPrice == null && maxPrice == null && groupByName == false)
            {
                var list = requests.ListOfRequests.FindAll(r => r.ClientId == id).ToList();
                requests.ListOfRequests = list;

                return new RequestsRaportViewModel()
                {
                    Requests = requests
                };
            }
            



            return null;
        }

        //save database here
        public Requests LoadRequests(IEnumerable<string> fileList)
        {
            var requests = new Requests();

            foreach (string text in fileList)
            {
                if (Regex.Match(text, @"..csv").Success)
                {
                    var rows = File.ReadAllLines(text)
                                .Skip(1)
                                .Select(p => Request.CsvToObject(p))
                                .ToList();

                    requests.ListOfRequests.AddRange(rows);
                }

                if (Regex.Match(text, @"..json").Success)
                {
                    requests.ListOfRequests.AddRange(Request.JsonToObjectList(text));
                }

                if (Regex.Match(text, @".*.xml").Success)
                {
                    requests.ListOfRequests.AddRange(Request.XmlToObjectList(text));
                }
            }
            return requests;
        }
    }
}
