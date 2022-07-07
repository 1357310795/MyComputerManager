using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace MyComputerManager.Extensions
{
    public class ClickExtensions
    {
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent
         ("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ClickExtensions));

        // 为界面元素添加路由事件侦听
        public static void AddClickHandler(DependencyObject d, RoutedEventHandler h)
        {
            UIElement e = d as UIElement;
            if (e != null)
            {
                e.AddHandler(ClickExtensions.ClickEvent, h);
            }
        }

        // 移除侦听
        public static void RemoveClickHandler(DependencyObject d, RoutedEventHandler h)
        {
            UIElement e = d as UIElement;
            if (e != null)
            {
                e.RemoveHandler(ClickExtensions.ClickEvent, h);
            }
        }
    }

    public class ClickBehavior : Behavior<Border>
    {
        int status = 0;
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (status > 0)
            {
                RoutedEventArgs arg = new RoutedEventArgs(ClickExtensions.ClickEvent, AssociatedObject);
                AssociatedObject.RaiseEvent(arg);
            }
            status = 0;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            status = 1;
        }

        private void AssociatedObject_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            status = 0;
        }

        private void AssociatedObject_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (status == 0) 
                status = -1;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
            AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }
    }
}
