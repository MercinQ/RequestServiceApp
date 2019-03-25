using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using RequestServiceApp.Models;
using RequestServiceApp.Services;

namespace BootcampCoreServicesTaskWpf
{

    public partial class MainWindow : Window
    {

        private Requests requests = null;
        private DbService dbService = new DbService();

        public MainWindow()
        {
            InitializeComponent();
            menuItemOpenFile.Click += btnFileDialog_Click;
            buttonApplyFilters.Click += buttonApplyFilters_Click;
        }


        private void buttonApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            int? id = null;
            double? minPrice = null;
            double? maxPrice = null;

            if ((bool)checkBoxCustomerId.IsChecked)
                id = longUpDownCustomerId.Value;
            //dla pozostałych tak samo



        }


        private void btnFileDialog_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var fileList = openFileDialog.FileNames;
                //tu zmień
                requests = dbService.LoadRequests(fileList);
                dataGridViewRequests.ItemsSource = requests.ListOfRequests;
            }

        }

        private void btnNumberOfOrders_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
