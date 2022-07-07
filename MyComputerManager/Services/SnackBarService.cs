using MyComputerManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace MyComputerManager.Services
{
    public class SnackBarService : ISnackBarService
    {
        private Snackbar _snackbar;
        public void SetSnackbar(Snackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void Show(string title, string message, SymbolRegular icon = SymbolRegular.Info20, ControlAppearance appearance = ControlAppearance.Secondary, int timeout = 5000, bool showclosebutton = true)
        {
            _snackbar.Icon = icon;
            _snackbar.Appearance = appearance;
            _snackbar.Timeout = timeout;
            _snackbar.ShowCloseButton = showclosebutton;
            _snackbar.Show(title, message);
        }
    }
}
