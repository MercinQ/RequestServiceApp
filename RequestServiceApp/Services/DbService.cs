using RequestServiceApp.Models;
using RequestServiceApp.View.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RequestServiceApp.Services
{
    public class DbService
    {
        public Requests Requests = null; //"database"
        public Action<string> printOnScreen;

        public RequestsRaportViewModel GetRequestsRaportViewModel(string id, double? minPrice, double? maxPrice, bool groupByName)
        {
            var newList = new List<Request>(Requests.ListOfRequests);
            var viewModel = new RequestsRaportViewModel();
            


            if (groupByName != false && id != null) //Group By and clientId fitler
            {
                newList = newList.Where(x => x.ClientId == id).GroupBy(x => x.Name)
                    .Select(x => new Request { Name = x.Key, Quantity = x.Sum(y => y.Quantity) })
                    .ToList();

                viewModel.RequestList = newList;
                return viewModel;
            }

            if (groupByName != false) //Group By filter
            {
                newList = newList.GroupBy(x => x.Name)
                    .Select(x => new Request { Name = x.Key, Quantity = x.Sum(y => y.Quantity) })
                    .ToList();

                viewModel.RequestList = newList;
                return viewModel;
            }

            if (id != null && minPrice != null && maxPrice != null) //filtr by Id and price range
            {
                newList = newList.FindAll(x => x.Price >= minPrice && x.Price <= maxPrice)
                    .Where(x => x.ClientId == id)
                    .ToList();

                viewModel.RequestList = newList;
                return viewModel;
            }

            if (id != null && minPrice != null) //filtr by Id and minPrice
            {
                newList = newList.FindAll(x => x.Price >= minPrice)
                    .Where(x => x.ClientId == id)
                    .ToList();

                viewModel.RequestList = newList;
                return viewModel;
            }

            if (id != null && maxPrice != null) //filtr by Id and maxPrice
            {
                newList = newList.FindAll(x => x.Price <= maxPrice)
                    .Where(x => x.ClientId == id)
                    .ToList();

                viewModel.RequestList = newList;
                return viewModel;
            }

            if (id != null) //filtr by Id
            {
                newList = newList.FindAll(x => x.ClientId == id).ToList();

                viewModel.RequestList = newList;
                return viewModel;
            }

            if (minPrice != null && maxPrice != null) //filtr by Price Range
            {
                newList = newList.FindAll(x => x.Price >= minPrice && x.Price <= maxPrice).ToList();

            }

            if (minPrice != null) //filr by minPrice
            {
                newList = newList.FindAll(x => x.Price >= minPrice).ToList();
            }

            if (maxPrice != null) //filtr by maxPrice
            {
                newList = newList.FindAll(x => x.Price <= maxPrice).ToList();
            }

            viewModel.RequestList = newList;
            return viewModel;

        }

        //save "database" here
        public void LoadRequests(IEnumerable<string> fileList)
        {
            var requests = new Requests();

            foreach (string text in fileList)
            {
                if (Regex.Match(text, @"..csv").Success)
                {
                    try
                    {
                        requests.ListOfRequests.AddRange(Request.CsvToObjectList(text));
                    }
                    catch (Exception ex)
                    {
                        printOnScreen(ex.Message);
                    }
                }

                if (Regex.Match(text, @"..json").Success)
                {
                    try
                    {
                        requests.ListOfRequests.AddRange(Request.JsonToObjectList(text));
                    }
                    catch (Exception ex)
                    {
                        printOnScreen(ex.Message);
                    }
                }

                if (Regex.Match(text, @".*.xml").Success)
                {
                    try
                    {
                        requests.ListOfRequests.AddRange(Request.XmlToObjectList(text));
                    }
                    catch (Exception ex)
                    {
                        printOnScreen(ex.Message);
                    }
                }
            }
            Requests = requests;
        }
    }
}
