using MyComputerManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace MyComputerManager.Services.Contracts
{
    public interface IDialogService
    {
        void SetDialog(Dialog dialog);

        Task<bool> ShowDialog(DialogMessage content, double? dialogHeight, ControlAppearance buttonLeftAppearance, string buttonLeftText, ControlAppearance buttonRightAppearance, string buttonRightText);
    }
}
