using System;
using Newtonsoft.Json;
using StockAnalyzer.Core.Domain;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using StockAnalyzer.Core;

namespace StockAnalyzer.Windows
{
    public partial class MainWindow : Window
    {
        private static string API_URL = "https://ps-async.fekberg.com/api/stocks";
        private static string MSFT = "MSFT";

        private Stopwatch stopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
        }



        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            BeforeLoadingStockData();

            if (string.IsNullOrWhiteSpace(StockIdentifier.Text))
            {
                StockIdentifier.Text = MSFT;
            }

            var store = new DataStore();

            var responseTask = store.GetStockPrices(StockIdentifier.Text);

            Stocks.ItemsSource = await responseTask;

            //using (var client = new HttpClient())
            //{
            //    var responseTask = client.GetAsync($"{API_URL}/{StockIdentifier?.Text}");

            //    var response = await responseTask;

            //    var content = await response.Content.ReadAsStringAsync();

            //    var data = JsonConvert.DeserializeObject<IEnumerable<StockPrice>>(content);

            //    Stocks.ItemsSource = data;
            //}

            AfterLoadingStockData();
        }

        private void BeforeLoadingStockData()
        {
            stopwatch.Restart();
            StockProgress.Visibility = Visibility.Visible;
            StockProgress.IsIndeterminate = true;
        }

        private void AfterLoadingStockData()
        {
            StocksStatus.Text = $"Loaded stocks for {StockIdentifier.Text} in {stopwatch.ElapsedMilliseconds}ms";
            StockProgress.Visibility = Visibility.Hidden;
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;
        }

        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
