namespace TimelineExample.Messaging
{
    using GalaSoft.MvvmLight.Messaging;

    public class ShowRefreshButtonMessage : MessageBase
    {
        public ShowRefreshButtonMessage(bool show)
        {
            this.ShowRefreshButton = show;
        }

        public bool ShowRefreshButton { get; }
    }
}