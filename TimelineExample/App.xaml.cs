using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TimelineExample
{
    using Microsoft.Practices.ServiceLocation;

    using TimelineExample.Extensions;
    using TimelineExample.Messaging;
    using TimelineExample.ViewModels;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            this.LoadApplication(e);
        }

        private void LoadApplication(IActivatedEventArgs e)
        {
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += this.OnNavigationFailed;

                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                object parameter = null;

                if (e is ProtocolActivatedEventArgs protocolArgs && protocolArgs.Uri.Host.Equals("editor"))
                {
                    string itemId = protocolArgs.Uri.ExtractQueryValue("id");
                    parameter = new EditorNavigationMessage(Guid.Parse(itemId));
                }

                rootFrame.Navigate(typeof(AppShell), parameter);
            }
            else
            {
                if (e is ProtocolActivatedEventArgs protocolArgs && protocolArgs.Uri.Host.Equals("editor"))
                {
                    string itemId = protocolArgs.Uri.ExtractQueryValue("id");
                    ServiceLocator.Current.GetInstance<AppShellViewModel>().NavigateTo(
                        "editor",
                        new EditorNavigationMessage(Guid.Parse(itemId)));
                }
            }

            Window.Current.Activate();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            this.LoadApplication(args);
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
