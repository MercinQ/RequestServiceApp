using RequestServiceApp.Models;
using RequestServiceApp.View.Model;
using System.Collections.Generic;

namespace RequestServiceApp.Services
{
    public class GroupByService
    {    
        public List<GroupByRequestViewModel> GroupBy(List<Request> list)
        {
            var viewModel = new List<GroupByRequestViewModel>();

            for (int i = 0; i < list.Count; i++)
            {
                viewModel.Add(new GroupByRequestViewModel { Name = "", Quantity = 0 });
            }

            for (int i = 0; i < list.Count; i++)
            {
                viewModel[i].Name = list[i].Name;
                viewModel[i].Quantity = list[i].Quantity;
            }

            return viewModel;
        }
    }
}
