namespace TimelineExample.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;

    using TimelineExample.Messaging;
    using TimelineExample.Services.Activity;
    using TimelineExample.Services.Data;

    public class HomePageViewModel : ViewModelBase
    {
        private readonly IDataService dataService;

        private readonly AppShellViewModel appShell;

        private bool hasNoItems = true;

        public HomePageViewModel(IMessenger messenger, IDataService dataService, AppShellViewModel appShell)
        {
            this.MessengerInstance = messenger;
            this.dataService = dataService;
            this.appShell = appShell;

            this.Items = new ObservableCollection<MyTextItem>();
        }

        public ObservableCollection<MyTextItem> Items { get; }

        public bool HasNoItems
        {
            get => this.hasNoItems;
            set => this.Set(() => this.HasNoItems, ref this.hasNoItems, value);
        }

        public void OnNavigatedTo(object parameter)
        {
            this.MessengerInstance.Send(new ShowRefreshButtonMessage(true));

            this.UpdateItems();
        }

        private void UpdateItems()
        {
            IEnumerable<MyTextItem> items = this.dataService.GetItems(x => x.LastModified);

            this.Items.Clear();
            foreach (MyTextItem item in items)
            {
                this.Items.Add(item);
            }

            this.HasNoItems = !this.Items.Any();
        }

        public void OnNavigatingFrom(Func<bool> cancelNavigation)
        {
            this.MessengerInstance.Send(new ShowRefreshButtonMessage(false));
        }

        public void NavigateToItem(MyTextItem item)
        {
            if (item != null)
            {
                this.appShell.NavigateTo("editor", new EditorNavigationMessage(item.Id));
            }
        }
    }
}