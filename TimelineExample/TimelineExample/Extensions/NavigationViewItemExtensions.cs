namespace TimelineExample.Extensions
{
    using System;

    using TimelineExample.Views;

    using Windows.UI.Xaml.Controls;

    public static class NavigationViewItemExtensions
    {
        public static Type GetAssociatedPageType(this NavigationViewItemInvokedEventArgs args)
        {
            switch (args.InvokedItem)
            {
                case "Home":
                    return typeof(HomePage);
                case "Editor":
                    return typeof(EditorPage);
            }

            return null;
        }

        public static Type GetAssociatedPageType(this NavigationViewSelectionChangedEventArgs args)
        {
            if (!(args.SelectedItem is NavigationViewItem item))
            {
                return null;
            }

            return GetAssociatedPageTypeFromTag(item.Tag.ToString());
        }

        public static Type GetAssociatedPageTypeFromTag(string tag)
        {
            switch (tag)
            {
                case "home":
                    return typeof(HomePage);
                case "editor":
                    return typeof(EditorPage);
            }

            return null;
        }
    }
}