using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyComputerManager.Models
{
    public class NamespaceItem
    {
        public NamespaceItem(string name, string exePath, string iconPath, RegistryKey key, bool disabled, string cLSID)
        {
            Name = name;
            ExePath = exePath;
            IconPath = iconPath;
            RegKey = key;
            isEnabled = !disabled;
            CLSID = cLSID;
        }
        public string CLSID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ExePath { get; set; }
        public string IconPath { get; set; }
        public ImageSource ExeIcon { get; set; }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { 
                isEnabled = value;
                if (value)
                    SetEnable();
                else
                    SetDisable();
            }
        }

        public RegistryKey RegKey { get; set; }

        public void SetEnable()
        {
            try
            {
                var namespacekey = RegKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace");
                var namespacekey1 = RegKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpaceDisabled");
                namespacekey1.DeleteSubKey(CLSID);
                var newkey = namespacekey.CreateSubKey(CLSID);
                newkey.SetValue("", Name, RegistryValueKind.String);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void SetDisable()
        {
            try
            {
                var namespacekey = RegKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpaceDisabled");
                var namespacekey1 = RegKey.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\NameSpace");
                namespacekey1.DeleteSubKey(CLSID);
                var newkey = namespacekey.CreateSubKey(CLSID);
                newkey.SetValue("", Name, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
