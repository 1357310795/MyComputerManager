using GalaSoft.MvvmLight;
using MyComputerManager.Helpers;
using MyComputerManager.Models;
using MyComputerManager.Services.Contracts;
using MyComputerManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;

namespace MyComputerManager.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly ISnackBarService _snackBarService;
        public MainPageViewModel(INavigationService navigationService, IDataService dataService, ISnackBarService snackBarService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _snackBarService = snackBarService;
            Items = (ObservableCollection<NamespaceItem>)_dataService.GetData();
            dataService.SetVM(this);
            GoDetailCommand = new RelayCommand(GoDetail);
            ToggleCommand = new RelayCommand(ToggleEnabled);
        }

        private ObservableCollection<NamespaceItem> items;

        public ObservableCollection<NamespaceItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                this.RaisePropertyChanged("Items");
            }
        }

        public void GoDetail(object item)
        {
            _dataService.SetData(item);
            _navigationService.Navigate(typeof(DetailPage));
            //_navigationService.Navigate(typeof(Input));
        }

        public RelayCommand GoDetailCommand { get; set; }
        public RelayCommand ToggleCommand { get; set; }

        public void ToggleEnabled(object obj)
        {
            NamespaceItem item = (NamespaceItem)obj;
            var res = NamespaceHelper.SetEnabled(item, item.IsEnabled);
            if (!res.success)
            {
                _snackBarService.Show("操作失败", res.result, SymbolRegular.ShieldError16);
                item.IsEnabled = !item.IsEnabled;
            }
        }

        public void DeleteItem(NamespaceItem item)
        {
            if (item != null)
                if (Items.Contains(item))
                    Items.Remove(item);
        }

        public void AddItem(NamespaceItem item)
        {
            Items.Add(item);
        }
    }
}
