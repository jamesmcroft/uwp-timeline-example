namespace TimelineExample.Messaging
{
    using System;

    using GalaSoft.MvvmLight.Messaging;

    public class EditorNavigationMessage : MessageBase
    {
        public EditorNavigationMessage(Guid itemId)
        {
            this.ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}