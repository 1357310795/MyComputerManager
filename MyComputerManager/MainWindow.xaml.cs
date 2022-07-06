using MyComputerManager.Helpers;
using MyComputerManager.Services.Contracts;
using MyComputerManager.ViewModels;
using MyComputerManager.Views;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace MyComputerManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INavigationWindow
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IThemeService _themeService;
        public MainWindow(INavigationService navigationService, IPageService pageService, IDataService dataService, IThemeService themeService)
        {
            InitializeComponent();
            Wpf.Ui.Appearance.Background.Apply(this, Wpf.Ui.Appearance.BackgroundType.Mica);

            SetPageService(pageService);
            navigationService.SetNavigation(RootNavigation);
            _dataService = dataService;
            _navigationService = navigationService;
            _themeService = themeService;

            WelcomeGrid.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RootNavigation.Frame = RootFrame;
            RootNavigation.Items.Add(new NavigationItem() { PageType = typeof(MainPage), Cache = false});
            RootNavigation.Items.Add(new NavigationItem() { PageType = typeof(DetailPage), Cache = false});
            //RootNavigation.Navigate("mainpage");
            //RootNavigation.SelectedPageIndex = 0;

            Task.Run(async () =>
            {
                var data = NamespaceHelper.GetItems();
                await Task.Delay(2000);

                await Dispatcher.InvokeAsync(() =>
                {
                    WelcomeGrid.Visibility = Visibility.Collapsed;
                    RootMainGrid.Visibility = Visibility.Visible;

                    _dataService.SetData(data);
                    var res = _navigationService.Navigate(typeof(MainPage));
                    //var res = _navigationService.Navigate(typeof(Input));
                });

                return true;
            });
        }

        private void MenuBack_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(typeof(MainPage));
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        public Frame GetFrame()
        {
            return RootFrame;
        }

        public INavigation GetNavigation()
        {
            return RootNavigation;
        }

        public bool Navigate(Type pageType)
        {
            return RootNavigation.Navigate(pageType);
        }

        public void SetPageService(IPageService pageService)
        {
            RootNavigation.PageService = pageService;
        }

        public void ShowWindow()
        {
            Show();
        }

        public void CloseWindow()
        {
            Close();
        }

        private void MenuTheme_Click(object sender, RoutedEventArgs e)
        {
            _themeService.SetTheme(_themeService.GetTheme() == ThemeType.Dark ? ThemeType.Light : ThemeType.Dark);
        }
    }
}
