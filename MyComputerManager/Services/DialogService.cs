using MyComputerManager.Models;
using MyComputerManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;

namespace MyComputerManager.Services
{
    public class DialogService : IDialogService
    {
        private Dialog _dialog;
        private TaskCompletionSource<bool> source;
        private bool buttonresult;
        public void SetDialog(Dialog dialog)
        {
            _dialog = dialog;
            _dialog.Closed += _dialog_Closed;
            _dialog.ButtonLeftClick += _dialog_ButtonLeftClick;
            _dialog.ButtonRightClick += _dialog_ButtonRightClick;
        }

        private void _dialog_ButtonRightClick(object sender, RoutedEventArgs e)
        {
            buttonresult = true;
            _dialog.Hide();
        }

        private void _dialog_ButtonLeftClick(object sender, RoutedEventArgs e)
        {
            buttonresult = false;
            _dialog.Hide();
        }

        private void _dialog_Closed(Dialog sender, RoutedEventArgs e)
        {
            if (source != null)
                source.SetResult(buttonresult);
        }

        public async Task<bool> ShowDialog(DialogMessage content, double? dialogHeight, ControlAppearance buttonLeftAppearance, string buttonLeftText, ControlAppearance buttonRightAppearance, string buttonRightText)
        {
            var res = await App.Current.Dispatcher.Invoke(async () => {
                _dialog.DataContext = content;
                if (dialogHeight != null) _dialog.DialogHeight = dialogHeight.Value;
                _dialog.ButtonLeftAppearance = buttonLeftAppearance;
                _dialog.ButtonRightAppearance = buttonRightAppearance;
                _dialog.ButtonLeftName = buttonLeftText;
                _dialog.ButtonRightName = buttonRightText;
                return await _dialog.ShowAndWaitAsync(); 
            });
            source = new TaskCompletionSource<bool>();
            source.Task.Wait();
            return buttonresult;
        }

    }
}
