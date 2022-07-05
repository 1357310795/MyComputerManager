using MyComputerManager.Helpers;
using MyComputerManager.Models;
using MyComputerManager.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;

namespace MyComputerManager.Views
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
