namespace TimelineExample.ViewModels
{
    using System;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Messaging;

    using TimelineExample.Messaging;
    using TimelineExample.Services.Activity;
    using TimelineExample.Services.Data;

    public class EditorPageViewModel : ViewModelBase
    {
        private readonly IDataService dataService;

        private Guid itemId;

        private string content;

        public EditorPageViewModel(IMessenger messenger, IDataService dataService)
        {
            this.MessengerInstance = messenger;
            this.dataService = dataService;
        }

        public string Content
        {
            get => this.content;
            set => this.Set(() => this.Content, ref this.content, value);
        }

        public void OnNavigatedTo(object parameter)
        {
            MyTextItem item = null;

            switch (parameter)
            {
                case EditorNavigationMessage message:
                    item = this.dataService.GetItem(message.ItemId) ?? this.dataService.CreateItem();
                    break;
            }

            if (item != null)
            {
                this.itemId = item.Id;
                this.Content = item.Content;
            }
            else
            {
                this.itemId = Guid.Empty;
                this.Content = string.Empty;
            }

            this.MessengerInstance.Send(new ShowSaveDataButtonMessage(true));
            this.MessengerInstance.Register<SaveDataMessage>(this, x => this.Save());
        }

        public void OnNavigatingFrom(Func<bool> cancelNavigation)
        {
            this.MessengerInstance.Send(new ShowSaveDataButtonMessage(false));
            this.MessengerInstance.Unregister<SaveDataMessage>(this);

            this.itemId = Guid.Empty;
            this.Content = string.Empty;
        }

        private void Save()
        {
            if (this.itemId == Guid.Empty)
            {
                MyTextItem item = this.dataService.CreateItem();
                this.itemId = item.Id;
            }

            this.dataService.SaveItemAsync(this.itemId, this.Content);
        }
    }
}