using DevTools.RegistryJump;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class RegBox : UserControl
    {
        public RegBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty RegPathProperty = DependencyProperty.Register(nameof(RegPath), typeof(string), typeof(RegBox),
            new PropertyMetadata(null));

        public string RegPath
        {
            get { return (string)GetValue(RegPathProperty); }
            set { SetValue(RegPathProperty, value); }
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            RegistryEditor.OpenRegistryEditor(RegPath);
        }
    }
}
