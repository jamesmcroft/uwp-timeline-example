namespace TimelineExample.Messaging
{
    using GalaSoft.MvvmLight.Messaging;

    public class ShowSaveDataButtonMessage : MessageBase
    {
        public ShowSaveDataButtonMessage(bool show)
        {
            this.ShowSaveDataButton = show;
        }

        public bool ShowSaveDataButton { get; }
    }
}