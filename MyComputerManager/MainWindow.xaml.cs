using MyComputerManager.Helpers;
using MyComputerManager.Models;
using MyComputerManager.Services.Contracts;
using MyComputerManager.ViewModels;
using MyComputerManager.Views;
using System;
using System.Collections.ObjectModel;
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
    public partial class MainWindow : INavigationWindow
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IThemeService _themeService;
        private readonly ISnackBarService _snackBarService;
        private readonly IDialogService _dialogService;
        public MainWindow(INavigationService navigationService, IPageService pageService, IDataService dataService, IThemeService themeService, ISnackBarService snackBarService, IDialogService dialogService)
        {
            InitializeComponent();
            Wpf.Ui.Appearance.Background.Apply(this, Wpf.Ui.Appearance.BackgroundType.Mica);

            SetPageService(pageService);
            navigationService.SetNavigation(RootNavigation);
            snackBarService.SetSnackbar(RootSnackbar);
            dialogService.SetDialog(RootDialog);
            _dataService = dataService;
            _navigationService = navigationService;
            _themeService = themeService;
            _snackBarService = snackBarService;
            _dialogService = dialogService;

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
                await Task.Delay(1000);

                await Dispatcher.InvokeAsync(() =>
                {
                    WelcomeGrid.Visibility = Visibility.Collapsed;
                    RootMainGrid.Visibility = Visibility.Visible;

                    var o = new ObservableCollection<NamespaceItem>();
                    foreach (var item in data)
                        o.Add(item);
                    _dataService.SetData(o);
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
            var item = new NamespaceItem("新建项目");
            _dataService.SetData(item);
            _navigationService.Navigate(typeof(DetailPage));
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
