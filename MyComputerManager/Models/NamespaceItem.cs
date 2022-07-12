using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyComputerManager.Models
{
    public class NamespaceItem : INotifyPropertyChanged
    {
        public NamespaceItem()
        {

        }
        public NamespaceItem(string name, string desc, string tip, string exePath, string iconPath, RegistryKey key, RegistryKey key1, bool disabled, string cLSID, ItemType type)
        {
            Name = name;
            Desc = desc;
            Tip = tip;
            ExePath = exePath;
            IconPath = iconPath;
            RegKey = key;
            RegKey1 = key1;
            isEnabled = !disabled;
            CLSID = cLSID;
            Type = type;
        }

        public NamespaceItem(string name)
        {
            Name = name;
            RegKey = Registry.CurrentUser;
            RegKey1 = Registry.CurrentUser;
            isEnabled = true;
            CLSID = null;
            Desc = "";
            Tip = "";
            ExePath = "";
            IconPath = "";
            Type = ItemType.MyComputer;
        }

        private string cLSID;
        public string CLSID
        {
            get { return cLSID; }
            set
            {
                cLSID = value;
                this.RaisePropertyChanged("CLSID");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        private string desc;
        public string Desc
        {
            get { return desc; }
            set
            {
                desc = value;
                this.RaisePropertyChanged("Desc");
            }
        }

        private string tip;
        public string Tip
        {
            get { return tip; }
            set
            {
                tip = value;
                this.RaisePropertyChanged("Tip");
            }
        }
        
        private string exePath;
        public string ExePath
        {
            get { return exePath; }
            set
            {
                exePath = value;
                this.RaisePropertyChanged("ExePath");
            }
        }

        private string iconPath;
        public string IconPath
        {
            get { return iconPath; }
            set
            {
                iconPath = value;
                this.RaisePropertyChanged("IconPath");
            }
        }

        private ImageSource icon;
        public ImageSource Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                this.RaisePropertyChanged("Icon");
            }
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { 
                isEnabled = value;
                this.RaisePropertyChanged("IsEnabled");
                //if (value)
                //    SetEnable();
                //else
                //    SetDisable();
            }
        }

        private ItemType type;
        public ItemType Type
        {
            get { return type; }
            set
            {
                type = value;
                this.RaisePropertyChanged("Type");
            }
        }


        public RegistryKey RegKey { get; set; }
        public RegistryKey RegKey1 { get; set; }

        public string RegKey_Namespace
        {
            get {
                return RegKey.Name + @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\" + Type.ToString() + @"\" + (IsEnabled ? "NameSpace" : "NameSpaceDisabled") + @"\" + CLSID;
            }
        }

        public string RegKey_CLSID
        {
            get
            {
                return RegKey1.Name + (RegKey1.View == RegistryView.Default ? @"\SOFTWARE\Classes\CLSID\" : @"\SOFTWARE\Classes\WOW6432Node\CLSID\") + CLSID;
            }
        }

        public NamespaceItem Clone()
        {
            return new NamespaceItem()
            {
                CLSID = CLSID,
                Name = Name,
                Desc = Desc,
                Tip = Tip,
                IconPath = IconPath,
                RegKey = RegKey,
                RegKey1 = RegKey1,
                IsEnabled = IsEnabled,
                Icon = Icon,
                ExePath = ExePath,
                Type = Type
            };
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion

    }
}
