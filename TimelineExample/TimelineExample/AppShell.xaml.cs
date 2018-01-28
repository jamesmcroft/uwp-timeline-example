namespace TimelineExample
{
    using System;
    using System.Linq;

    using TimelineExample.Extensions;
    using TimelineExample.ViewModels;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public sealed partial class AppShell
    {
        public AppShell()
        {
            this.InitializeComponent();
            this.DataContext = AppServiceLocator.AppShellViewModel;

            this.ViewModel.NavigateByTag = (tag, parameter) =>
                {
                    Type pageType = NavigationViewItemExtensions.GetAssociatedPageTypeFromTag(tag);
                    if (pageType == null)
                    {
                        return;
                    }

                    bool navigated = this.ContentFrame.Navigate(pageType, parameter);
                    if (navigated)
                    {
                        this.SetSelectedItemByTag(tag);
                    }
                };
        }

        public AppShellViewModel ViewModel => this.DataContext as AppShellViewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.ViewModel?.OnNavigatedTo(e.Parameter);
        }

        private void OnNavigationViewItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            Type pageType = args.GetAssociatedPageType();
            if (pageType != null && this.ContentFrame.CurrentSourcePageType != pageType)
            {
                this.ContentFrame.Navigate(pageType);
            }
        }

        private void OnNavigationViewSelectionChanged(
            NavigationView sender,
            NavigationViewSelectionChangedEventArgs args)
        {
            Type pageType = args.GetAssociatedPageType();
            if (pageType != null && this.ContentFrame.CurrentSourcePageType != pageType)
            {
                this.ContentFrame.Navigate(pageType);
            }
        }

        private void OnNavigationViewLoaded(object sender, RoutedEventArgs e)
        {
            this.SetSelectedItemByTag("home");
        }

        private void SetSelectedItemByTag(string tag)
        {
            object menuItem =
                this.NavView.MenuItems.FirstOrDefault(x => x is NavigationViewItem item && item.Tag.ToString() == tag);

            if (menuItem != null)
            {
                this.NavView.SelectedItem = menuItem;
            }
        }
    }
}