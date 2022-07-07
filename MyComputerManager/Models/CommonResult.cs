using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComputerManager.Models
{
    public class CommonResult
    {
        public bool success;
        public string result;

        public CommonResult() { }
        public CommonResult(bool success, string result)
        {
            this.success = success;
            this.result = result;
        }
    }
}
