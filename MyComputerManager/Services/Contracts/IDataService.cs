using MyComputerManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComputerManager.Services.Contracts
{
    public interface IDataService
    {
        void SetData(object data);

        object GetData();

        MainPageViewModel GetVM();

        void SetVM(MainPageViewModel data);
    }
}
