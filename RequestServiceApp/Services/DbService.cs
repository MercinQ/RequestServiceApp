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

        public Requests Requests = null; //"database"

        public RequestsRaportViewModel GetRequestsRaportViewModel(string id, double? minPrice, double? maxPrice, bool groupByName)
        {
            var newList = new List<Request>(Requests.ListOfRequests);
            var viewModel = new RequestsRaportViewModel();



            if (groupByName != false)
            {
                //var temp = newList.

                   
                    

            }

            if (id != null)
            {
                newList = newList.FindAll(x => x.ClientId == id).ToList();
            }

            if (minPrice != null && maxPrice != null)
            {
                newList = newList.FindAll(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();
            }

            if (minPrice != null)
            {
                newList = newList.FindAll(x => x.Price >= minPrice).ToList();
            }

            if (maxPrice != null)
            {
                newList = newList.FindAll(x => x.Price <= maxPrice).ToList();
            }

           
            viewModel.RequestList = newList;

            return viewModel;
        }

        //save database here
        public void LoadRequests(IEnumerable<string> fileList)
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
            Requests = requests;
        }
    }
}
