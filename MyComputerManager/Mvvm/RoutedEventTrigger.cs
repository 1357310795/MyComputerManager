using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyComputerManager
{
    public class RoutedEventTrigger : EventTriggerBase<FrameworkElement>
    {
        RoutedEvent _routedEvent;
        public RoutedEvent RoutedEvent
        {
            get { return _routedEvent; }
            set { _routedEvent = value; }
        }

        public RoutedEventTrigger() { }

        protected override void OnAttached()
        {
            if (RoutedEvent != null)
            {
                Source.AddHandler(RoutedEvent, new RoutedEventHandler(this.OnRoutedEvent));
            }
        }

        void OnRoutedEvent(object sender, RoutedEventArgs args)
        {
            base.OnEvent(args);
        }
        protected override string GetEventName()
        {
            return RoutedEvent.Name;
        }
    }
}
