namespace TimelineExample.Views
{
    using TimelineExample.Services.Data;
    using TimelineExample.ViewModels;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class HomePage
    {
        public HomePage()
        {
            this.InitializeComponent();
            this.DataContext = AppServiceLocator.HomePageViewModel;
        }

        public HomePageViewModel ViewModel => this.DataContext as HomePageViewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.ViewModel?.OnNavigatedTo(e.Parameter);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            this.ViewModel?.OnNavigatingFrom(() => e.Cancel = true);
        }

        private void OnListItemClicked(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is MyTextItem item)
            {
                this.ViewModel.NavigateToItem(item);
            }
        }
    }
}