using Microsoft.Win32;
using MyComputerManager.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyComputerManager.Controls
{
    public partial class PathBox : UserControl
    {
        public PathBox()
        {
            InitializeComponent();
        }


        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
            nameof(Path), typeof(string), typeof(PathBox),
            new PropertyMetadata(null));

        public string AllowExt
        {
            get { return (string)GetValue(AllowExtProperty); }
            set { SetValue(AllowExtProperty, value); }
        }

        public static readonly DependencyProperty AllowExtProperty =
            DependencyProperty.Register("AllowExt", typeof(string), typeof(PathBox), new PropertyMetadata(null));

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("explorer", "/select," + Path);
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = StringHelper.BuildFilter(AllowExt);
            if (d.ShowDialog() ?? false)
                Path = d.FileName;
        }
    }
}
