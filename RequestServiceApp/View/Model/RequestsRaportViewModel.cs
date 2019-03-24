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
        public Requests Requests { get; set; }


        public string RequestsCount
        {
            get
            {
                return $"Łączna liczba zamówień :  { Requests.RequestCount} ";
            }
        }

        public string RequestsSum
        {
            get
            {
                return $"Łączna kwota zamówień : { Requests.RequestsSum}";
            }
        }

        public string RequestsAvg
        {
            get
            {
                return $"Łączna kwota zamówień : { Requests.RequestAvg}";
            }
        }
    }
}
