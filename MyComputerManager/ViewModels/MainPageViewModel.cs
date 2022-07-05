using MyComputerManager.Models;
using MyComputerManager.Services.Contracts;
using MyComputerManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common;
using Wpf.Ui.Mvvm.Contracts;

namespace MyComputerManager.ViewModels
{
    public class MainPageViewModel : Wpf.Ui.Mvvm.ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        public MainPageViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            Items = (List<NamespaceItem>)_dataService.GetData();
            GoDetailCommand = new RelayCommand(GoDetail);
        }

        private List<NamespaceItem> items;

        public List<NamespaceItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                this.OnPropertyChanged("Items");
            }
        }

        public void GoDetail(object item)
        {
            _dataService.SetData(item);
            _navigationService.Navigate(typeof(DetailPage));
            //_navigationService.Navigate(typeof(Input));
        }

        public RelayCommand GoDetailCommand { get; set; }
    }
}
