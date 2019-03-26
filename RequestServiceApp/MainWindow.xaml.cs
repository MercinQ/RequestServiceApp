using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using RequestServiceApp.Models;
using RequestServiceApp.Services;
using RequestServiceApp.View.Model;

namespace RequestServiceApp
{

    public partial class MainWindow : Window
    {
        public DbService DbService = new DbService();
        public RequestsRaportViewModel RequestsRaportViewModel = new RequestsRaportViewModel();
        public IEnumerable<string> fileList;

        public MainWindow()
        {
            InitializeComponent();
            menuItemOpenFile.Click += btnFileDialog_Click;
            buttonApplyFilters.Click += buttonApplyFilters_Click;
        }


        private void buttonApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            string id = null;
            double? minPrice = null;
            double? maxPrice = null;
            bool groupByName = false;


            if ((bool)checkBoxCustomerId.IsChecked)
            {
                id = textBoxCustomerId.Text;
            }

            if ((bool)checkBoxPriceRange.IsChecked)
            {
                minPrice = longUpDownMinValue.Value;
                maxPrice = longUpDownMaxValue.Value;
            }

            if ((bool)checkBoxGroupByName.IsChecked)
            {
                groupByName = true;
            }


            RequestsRaportViewModel = DbService.GetRequestsRaportViewModel(id, minPrice, maxPrice, groupByName);
            dataGridViewRequests.ItemsSource = RequestsRaportViewModel.RequestList;
            LoadInfoView();
        }


        private void btnFileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                fileList = openFileDialog.FileNames;
                
                DbService.LoadRequests(fileList);
                var viewModel = DbService.GetRequestsRaportViewModel(null, null, null, false);
                dataGridViewRequests.ItemsSource = viewModel.RequestList;

                numberOfRequestsLabel.Content = viewModel.RequestsCount;
                numberOfAvgLabel.Content = viewModel.RequestsAvg;
                numberOfSumLabel.Content = viewModel.RequestsSum;
                
            }

        }


        private void ImagePanel_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void LoadInfoView()
        {
                numberOfRequestsLabel.Content = RequestsRaportViewModel.RequestsCount;
                numberOfAvgLabel.Content = RequestsRaportViewModel.RequestsAvg;
                numberOfSumLabel.Content = RequestsRaportViewModel.RequestsSum;
        }
    }
}

