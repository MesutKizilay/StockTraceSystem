using System.Runtime.CompilerServices;

namespace StockTraceSystem.App
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        //readonly string AllowedHost = new Uri("http://localhost:6838/").Host;
        readonly string AllowedHost = new Uri("httap://192.168.150.59:6838/").Host;

        public MainPage()
        {
            InitializeComponent();
        }

        //private void OnCounterClicked(object? sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}


        void Browser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Loading.IsVisible = Loading.IsRunning = true;

            // Domain dışı linkleri sistem tarayıcısında aç (opsiyonel)
            if (Uri.TryCreate(e.Url, UriKind.Absolute, out var uri) && uri.Host != AllowedHost)
            {
                e.Cancel = true;
                Launcher.OpenAsync(uri);
            }
        }

        void Browser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Loading.IsRunning = false;
            Loading.IsVisible = false;
            //Refresher.IsRefreshing = false;
        }

        void Refresher_Refreshing(object sender, EventArgs e) => Browser.Reload();

        // Android geri tuşu: WebView içinde geri
        protected override bool OnBackButtonPressed()
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}
