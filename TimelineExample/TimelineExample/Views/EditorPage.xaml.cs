namespace TimelineExample.Views
{
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using TimelineExample.ViewModels;

    public sealed partial class EditorPage
    {
        public EditorPage()
        {
            this.InitializeComponent();
            this.DataContext = AppServiceLocator.EditorPageViewModel;
        }

        public EditorPageViewModel ViewModel => this.DataContext as EditorPageViewModel;

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

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                this.ViewModel.Content = textBox.Text;
            }
        }
    }
}