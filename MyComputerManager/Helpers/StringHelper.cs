using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyComputerManager.Helpers
{
    public static class StringHelper
    {
        public static string BuildFilter(string ext)
        {
            var exts = ext.Split(',');
            var res = new List<string>();
            var all = new List<string>();
            foreach (var i in exts)
            {
                res.Add($"{i} File|*.{i}");
                all.Add($"*.{i}");
            }
            res.Insert(0, $"All Supported Files|{string.Join(";", all)}");
            return string.Join("|", res);
        }
    }
}
