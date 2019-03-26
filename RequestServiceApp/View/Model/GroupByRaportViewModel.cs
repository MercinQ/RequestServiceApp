using RequestServiceApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestServiceApp.View.Model
{
    public class RequestViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class GroupByRaportViewModel
    {
        public List<RequestViewModel> RequestViewModels = new List<RequestViewModel>();

        public GroupByRaportViewModel(List<Request> list)
        {
            //init list
            for (int i = 0; i < list.Count; i++)
            {
                RequestViewModels.Add(new RequestViewModel {Name="",Quantity = 0 });
            }

           
            for (int i=0; i < list.Count; i++)
            {
                RequestViewModels[i].Name = list[i].Name;
                RequestViewModels[i].Quantity = list[i].Quantity;
            }
        }
    }
}
