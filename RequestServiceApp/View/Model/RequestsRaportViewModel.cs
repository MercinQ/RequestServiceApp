using RequestServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestServiceApp.View.Model
{
    public class RequestsRaportViewModel
    {
        public List<Request> RequestList { get; set; }

        public string RequestsCount
        {
            get
            {
                return $"Łączna liczba zamówień :  {RequestList.Count} ";
            }
        }

        public string RequestsSum
        {
            get
            {
                double sum = Sum();
                
                return $"Łączna suma zamówień : {sum}";
            }
        }

        public string RequestsAvg
        {
            get
            {
                double avg = Sum() / RequestList.Count;
                return $"Łączna średnia zamówień : { avg}";
            }
        }


        public double Sum()
        {
            double sum = 0.0;
            foreach (Request request in RequestList)
            {
                sum += request.Quantity * request.Price;
            }
            return sum;
        }

        public RequestsRaportViewModel() 
        {
            RequestList = new List<Request>();
        }
    }
}
