﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using RequestServiceApp.Services;
using RequestServiceApp.View.Model;

namespace RequestServiceApp
{

    public partial class MainWindow : Window
    {
        public DbService DbService = new DbService();
        public SummaryRaportViewModel SummaryRaportViewModel = new SummaryRaportViewModel();
        public IEnumerable<string> fileList;
        
        public MainWindow()
        {
            InitializeComponent();
            menuItemOpenFile.Click += btnFileDialog_Click;
            buttonApplyFilters.Click += buttonApplyFilters_Click;
            DbService.printOnScreen = printDialog;
        }

        private void buttonApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            string id = null;
            double? minPrice = null;
            double? maxPrice = null;
            var groupByName = false;


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

            try
            {
                SummaryRaportViewModel = DbService.GetRequestsRaportViewModel(id, minPrice, maxPrice, groupByName);
            }
            catch (System.NullReferenceException)
            {
                MessageBox.Show("Proszę wczytaj rekordy z zamówieniami");
            }
            if (groupByName != false)
            {
                var groupByService = new GroupByService();
                var itemsGroupBy = groupByService.GroupBy(SummaryRaportViewModel.ListOfRequests);
                
                dataGridViewRequests.ItemsSource = itemsGroupBy;

                numberOfRequestsLabel.Content = "";
                numberOfAvgLabel.Content = "";
                numberOfSumLabel.Content = "";
            }
            else
            {
                dataGridViewRequests.ItemsSource = SummaryRaportViewModel.ListOfRequests;
                LoadInfoView();
            }
        }

        public void printDialog(string text)
        {
            MessageBox.Show(text);
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

                dataGridViewRequests.ItemsSource = viewModel.ListOfRequests;
                numberOfRequestsLabel.Content = viewModel.RequestsCount;
                numberOfAvgLabel.Content = viewModel.RequestsAvg;
                numberOfSumLabel.Content = viewModel.RequestsSum;
            }
        }

        private void ImagePanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
        }

        private void LoadInfoView()
        {
            numberOfRequestsLabel.Content = SummaryRaportViewModel.RequestsCount;
            numberOfAvgLabel.Content = SummaryRaportViewModel.RequestsAvg;
            numberOfSumLabel.Content = SummaryRaportViewModel.RequestsSum;
        }
    }
}

