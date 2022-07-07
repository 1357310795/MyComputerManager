﻿using MyComputerManager.Extensions;
using MyComputerManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace MyComputerManager.Views
{
    /// <summary>
    /// DetailPage.xaml 的交互逻辑
    /// </summary>
    public partial class DetailPage
    {
        public DetailPage(DetailPageViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
            ClickExtensions.AddClickHandler(BorderIcon, new RoutedEventHandler(vm.ButtonOpenIcon_Click));
        }
    }
}
