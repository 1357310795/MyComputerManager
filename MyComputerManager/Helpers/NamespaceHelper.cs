using Microsoft.Win32;
using MyComputerManager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Wpf.Ui.Controls;

namespace MyComputerManager.Helpers
{
    public static class NamespaceHelper
    {
        public static List<NamespaceItem> GetItems()
        {
            try
            {
                var tmp = NamespaceHelper.GetRawItems();
                foreach (var t in tmp)
                {
                    var p = t.IconPath.LastIndexOf(',');
                    if (p != -1)
                        t.IconPath = t.IconPath.Substring(0, p);

                    t.Icon = IconHelper.ReadIcon(t.IconPath);
                }
                return tmp;
            }
            catch (Exception ex)
            {
                var m = new MessageBox();
                m.Show("读取数据时发生异常", ex.Message);
                return null;
            }
        }

        public static List<NamespaceItem> GetRawItems()
        {
            var l1 = GetItemsInternal(Registry.CurrentUser, false);
            var l2 = GetItemsInternal(Registry.LocalMachine, false);
            var l3 = GetItemsInternal(Registry.CurrentUser, true);
            var l4 = GetItemsInternal(Registry.LocalMachine, true);
            return l1.Concat(l2).Concat(l3).Concat(l4).ToList();
        }

        public static List<NamespaceItem> GetItemsInternal(RegistryKey rootkey, bool disabled)
        {
            var list = new List<NamespaceItem>();
            var LocalMachineNamespace = rootkey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace" + (disabled ? "Disabled" : ""), false);
            if (LocalMachineNamespace == null)
                return new List<NamespaceItem>();
            foreach (var item in LocalMachineNamespace.GetSubKeyNames())
            {
                var clsidkey = rootkey.OpenSubKey(@"SOFTWARE\Classes\CLSID", false);
                var itemkey = clsidkey.OpenSubKey(item, false);
                if (itemkey != null)
                {
                    var iconkey = itemkey.OpenSubKey("DefaultIcon");
                    var dllkey = itemkey.OpenSubKey("InProcServer32");
                    var exekey = itemkey.OpenSubKey(@"Shell\Open\Command");
                    string name = (string)itemkey.GetValue("", "");
                    string desc = (string)itemkey.GetValue("System.ItemAuthors", "");
                    string tip = (string)itemkey.GetValue("InfoTip", "");

                    string iconpath = (string)(iconkey?.GetValue("") ?? "");
                    string exepath = (string)(exekey?.GetValue("") ?? "");

                    if (name == "") continue;
                    list.Add(new NamespaceItem(name, desc, tip, exepath, iconpath, rootkey, disabled, item));
                }
            }
            return list;
        }

        public static CommonResult UpdateItem(NamespaceItem item)
        {
            try
            {
                var namespaceKey = item.RegKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace" + (item.IsEnabled ? "" : "Disabled"), true);
                if (namespaceKey == null)
                    return new CommonResult(false, "找不到Namespace key");
                var namespaceSubKey = namespaceKey.CreateSubKey(item.CLSID, true);
                if (namespaceSubKey == null)
                    return new CommonResult(false, "找不到Namespace下的" + item.CLSID);
                var clsidKey = item.RegKey.CreateSubKey(@"SOFTWARE\Classes\CLSID", true);
                if (clsidKey == null)
                    return new CommonResult(false, "找不到CLSID key");
                var clsidSubKey = clsidKey.CreateSubKey(item.CLSID, true);
                if (clsidSubKey == null)
                    return new CommonResult(false, "找不到Classes下的" + item.CLSID);

                if (item.Name == "")
                    return new CommonResult(false, "名称不能为空");
                clsidSubKey.SetValue("", item.Name);
                if (clsidSubKey.GetValue("LocalizedString", true) != null)
                    clsidSubKey.SetValue("LocalizedString", item.Name);

                if (item.Desc == "")
                    clsidSubKey.DeleteValue("System.ItemAuthors", false);
                else
                    clsidSubKey.SetValue("System.ItemAuthors", item.Desc);
                clsidSubKey.SetValue("TileInfo", "prop:System.ItemAuthors");

                if (item.Tip != "")
                    clsidSubKey.SetValue("InfoTip", item.Tip);
                else
                    clsidSubKey.DeleteValue("InfoTip", false);

                if (item.IconPath != null)
                {
                    var iconKey = clsidSubKey.CreateSubKey("DefaultIcon", true);
                    if (iconKey == null)
                        return new CommonResult(false, "创建Key：DefaultIcon失败");

                    iconKey.SetValue("", item.IconPath + ",0", RegistryValueKind.ExpandString);
                }
                
                if (item.ExePath == "")
                {
                    clsidSubKey.DeleteSubKeyTree(@"Shell\Open", false);
                }
                else
                {
                    var exeKey = clsidSubKey.CreateSubKey(@"Shell\Open\Command", true);
                    if (exeKey == null)
                        return new CommonResult(false, @"创建Key：Shell\Open\Command失败");

                    exeKey.SetValue("", item.ExePath);
                }

                return new CommonResult(true, "成功");
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message);
            }
        }

        public static CommonResult SetEnabled(NamespaceItem item, bool isEnabled)
        {
            try
            {
                if (isEnabled)
                {
                    var namespacekey = item.RegKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace");
                    var namespacekey1 = item.RegKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpaceDisabled");
                    var newkey = namespacekey.CreateSubKey(item.CLSID);
                    namespacekey1.DeleteSubKey(item.CLSID);
                    newkey.SetValue("", item.Name, RegistryValueKind.String);
                }
                else
                {
                    var namespacekey = item.RegKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpaceDisabled");
                    var namespacekey1 = item.RegKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace");
                    var newkey = namespacekey.CreateSubKey(item.CLSID);
                    namespacekey1.DeleteSubKey(item.CLSID);
                    newkey.SetValue("", item.Name, RegistryValueKind.String);
                }
                return new CommonResult(true, "成功");
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message);
            }
        }

        public static CommonResult DeleteItem(NamespaceItem item)
        {
            try
            {
                var namespaceKey = item.RegKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace" + (item.IsEnabled ? "" : "Disabled"), true);
                if (namespaceKey == null)
                    return new CommonResult(false, "找不到Namespace key");
                namespaceKey.DeleteSubKeyTree(item.CLSID, false);

                var clsidKey = item.RegKey.OpenSubKey(@"SOFTWARE\Classes\CLSID", true);
                if (clsidKey == null)
                    return new CommonResult(false, "找不到CLSID key");
                clsidKey.DeleteSubKeyTree(item.CLSID, false);

                return new CommonResult(true, "成功");
            }
            catch (Exception ex)
            {
                return new CommonResult(false, ex.Message);
            }
        }
    }
}
