namespace TimelineExample.ViewModels
{
    using System;
    using System.Windows.Input;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using TimelineExample.Messaging;

    public class AppShellViewModel : ViewModelBase
    {
        private string pageTitle;

        private bool isRefreshItemsVisible;

        private bool isSaveItemVisible;

        public AppShellViewModel(IMessenger messenger)
        {
            this.MessengerInstance = messenger;

            this.CreateNewItemCommand = new RelayCommand(() => this.NavigateTo("editor", null));
            this.RefreshItemsCommand = new RelayCommand(() => this.MessengerInstance.Send(new RefreshMessage()));
            this.SaveItemCommand = new RelayCommand(() => this.MessengerInstance.Send(new SaveDataMessage()));

            this.MessengerInstance.Register<ShowRefreshButtonMessage>(
                this,
                x => this.IsRefreshItemsVisible = x.ShowRefreshButton);

            this.MessengerInstance.Register<ShowSaveDataButtonMessage>(
                this,
                x => this.IsSaveItemVisible = x.ShowSaveDataButton);
        }

        public Action<string, object> NavigateByTag { get; set; }

        public ICommand CreateNewItemCommand { get; }

        public ICommand RefreshItemsCommand { get; }

        public ICommand SaveItemCommand { get; }

        public string PageTitle
        {
            get => this.pageTitle;
            set => this.Set(() => this.PageTitle, ref this.pageTitle, value);
        }

        public bool IsRefreshItemsVisible
        {
            get => this.isRefreshItemsVisible;
            set => this.Set(() => this.IsRefreshItemsVisible, ref this.isRefreshItemsVisible, value);
        }

        public bool IsSaveItemVisible
        {
            get => this.isSaveItemVisible;
            set => this.Set(() => this.IsSaveItemVisible, ref this.isSaveItemVisible, value);
        }

        public void NavigateTo(string tag, object parameter)
        {
            this.NavigateByTag.Invoke(tag, parameter);
        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is EditorNavigationMessage)
            {
                this.NavigateTo("editor", parameter);
            }
        }
    }
}