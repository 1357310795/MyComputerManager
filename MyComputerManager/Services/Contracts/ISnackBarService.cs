using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace MyComputerManager.Services.Contracts
{
    public interface ISnackBarService
    {
        void SetSnackbar(Snackbar snackbar);

        void Show(string title, string message, SymbolRegular icon = SymbolRegular.Info20, ControlAppearance appearance = ControlAppearance.Secondary, int timeout = 5000, bool showclosebutton = true);
    }
}
