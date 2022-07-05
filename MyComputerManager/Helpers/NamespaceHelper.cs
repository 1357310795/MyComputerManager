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

namespace MyComputerManager.Helpers
{
    public static class NamespaceHelper
    {
        public static List<NamespaceItem> GetItems()
        {
            var tmp = NamespaceHelper.GetRawItems();
            foreach (var t in tmp)
            {
                var p = t.IconPath.LastIndexOf(',');
                if (p != -1)
                    t.IconPath = t.IconPath.Substring(0, p);

                if (File.Exists(t.IconPath))
                {
                    var f = new FileInfo(t.IconPath);
                    if (f.Extension.ToLower() == ".ico")
                    {
                        Stream iconStream = new FileStream(t.IconPath, FileMode.Open);
                        IconBitmapDecoder decoder = new IconBitmapDecoder(
                                iconStream,
                                BitmapCreateOptions.PreservePixelFormat,
                                BitmapCacheOption.None);

                        foreach (var item in decoder.Frames)
                        {
                            if (item.PixelWidth == 128 && item.Format == PixelFormats.Bgra32)
                            {
                                t.ExeIcon = item;
                                break;
                            }
                        }
                    }
                    else if (f.Extension.ToLower() == ".exe")
                    {
                        Icon i = IconHelper.ExtractIcon(t.IconPath, IconSize.ExtraLarge);
                        t.ExeIcon = BitmapHelper.ToBitmapSource(i.ToBitmap());
                    }
                }
            }
            return tmp;
        }

        public static List<NamespaceItem> GetRawItems()
        {
            var l1 = GetItemsInternal(Registry.CurrentUser, true);
            var l2 = GetItemsInternal(Registry.CurrentUser, false);
            var l3 = GetItemsInternal(Registry.LocalMachine, true);
            var l4 = GetItemsInternal(Registry.LocalMachine, false);
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

                    string iconpath = (string)(iconkey?.GetValue("") ?? "");
                    string exepath = (string)(exekey?.GetValue("") ?? "");

                    if (name == "") continue;
                    list.Add(new NamespaceItem(name, exepath, iconpath, rootkey, disabled, item));
                }
            }
            return list;
        }
    }
}
