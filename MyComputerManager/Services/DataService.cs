using MyComputerManager.Services.Contracts;
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
    }
}
