using RequestServiceApp.Models;
using System.Collections.Generic;

namespace RequestServiceApp.View.Model
{
    public class SummaryRaportViewModel
    {
        public List<Request> ListOfRequests;
        public Requests Requests = new Requests();

        public SummaryRaportViewModel()
        {
            ListOfRequests = Requests.ListOfRequests;
        }

        public string RequestsCount
        {
            get
            {
                return $"Łączna liczba zamówień :  {Requests.GetCount()}";
            }
        }

        public string RequestsSum
        {
            get
            {
                return $"Łączna suma zamówień : {Requests.GetSum()}";
            }
        }

        public string RequestsAvg
        {
            get
            {
                return $"Łączna średnia zamówień : {Requests.GetAvg()}";
            }
        }
    }
}
