using System;
using System.Collections.Generic;
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

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register(
            nameof(Path), typeof(string), typeof(PathBox),
            new PropertyMetadata(null));

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }
    }
}
