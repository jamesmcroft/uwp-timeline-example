namespace TimelineExample
{
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;

    using Microsoft.Practices.ServiceLocation;

    using TimelineExample.Services.Activity;
    using TimelineExample.Services.Caching;
    using TimelineExample.Services.Caching.FileSystem;
    using TimelineExample.Services.Data;
    using TimelineExample.ViewModels;

    /// <summary>
    /// Defines the application's service locator.
    /// </summary>
    public class AppServiceLocator
    {
        private static HomePageViewModel homePageViewModel;

        private static AppShellViewModel appShellViewModel;

        private static EditorPageViewModel editorPageViewModel;

        static AppServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            RegisterServices();
            RegisterViewModels();
        }

        /// <summary>
        /// Gets the view model associated with the main page.
        /// </summary>
        public static HomePageViewModel HomePageViewModel =>
            homePageViewModel ?? (homePageViewModel = ServiceLocator.Current.GetInstance<HomePageViewModel>());

        public static AppShellViewModel AppShellViewModel =>
            appShellViewModel ?? (appShellViewModel = ServiceLocator.Current.GetInstance<AppShellViewModel>());

        public static EditorPageViewModel EditorPageViewModel =>
            editorPageViewModel ?? (editorPageViewModel = ServiceLocator.Current.GetInstance<EditorPageViewModel>());


        private static void RegisterServices()
        {
            SimpleIoc.Default.Register<IMessenger, Messenger>();

            SimpleIoc.Default.Register<IDataCacheProvider>(() => new FileDataCacheProvider());
            SimpleIoc.Default.Register<IDataService, DataService>();
        }

        private static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<AppShellViewModel>();
            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<EditorPageViewModel>();
        }
    }
}