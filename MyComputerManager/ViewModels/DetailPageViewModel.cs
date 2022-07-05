using MyComputerManager.Models;
using MyComputerManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Mvvm.Contracts;

namespace MyComputerManager.ViewModels
{
    public class DetailPageViewModel : Wpf.Ui.Mvvm.ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        public DetailPageViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            Item = (NamespaceItem)_dataService.GetData();
        }

        private NamespaceItem item;
        public NamespaceItem Item
        {
            get { return item; }
            set
            {
                item = value;
                this.OnPropertyChanged("Item");
            }
        }
    }
}
