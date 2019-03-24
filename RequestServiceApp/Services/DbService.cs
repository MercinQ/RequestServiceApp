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
        public RequestsRaportViewModel GetRequestsRaportViewModel(int ID, double minPrice, double maxPrice)
        {
            //linq 
            // if(ID != null)
            //write linq here


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
