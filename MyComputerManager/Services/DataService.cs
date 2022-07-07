using MyComputerManager.Services.Contracts;
using MyComputerManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComputerManager.Services
{
    public class DataService : IDataService
    {
        private object _data;

        public object GetData()
        {
            return _data;
        }

        public void SetData(object data)
        {
            _data = data;
        }

        private MainPageViewModel vm;

        public MainPageViewModel GetVM()
        {
            return vm;
        }

        public void SetVM(MainPageViewModel data)
        {
            vm = data;
        }
    }
}
