using MyComputerManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Interfaces;
using System.Drawing.Structs;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace System.Drawing
{
    public static class IconHelper
    {
        [DllImport("User32.dll")]
        internal static extern uint PrivateExtractIcons(string szFileName, int nIconIndex, int cxIcon, int cyIcon, IntPtr[] phicon, uint[] piconid, uint nIcons, uint flags);
        public static Icon ReadIconFromExe(string filePath, IconSize size)
        {
            Icon icon = null;
            var fileInfo = new SHFILEINFOW();

            if (NativeMethods.SHGetFileInfoW(filePath, NativeMethods.FILE_ATTRIBUTE_NORMAL, ref fileInfo, Marshal.SizeOf(fileInfo), NativeMethods.SHGFI_SYSICONINDEX) == 0)
                throw new FileNotFoundException();

            var iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");
            IImageList imageList = null;

            NativeMethods.SHGetImageList((int)size, ref iidImageList, ref imageList);

            if (imageList != null)
            {
                var hIcon = IntPtr.Zero;
                //imageList.GetIcon(fileInfo.iIcon, (int)NativeMethods.ILD_TRANSPARENT, ref hIcon);
                imageList.GetIcon(fileInfo.iIcon, (int)NativeMethods.ILD_IMAGE, ref hIcon);
                icon = Icon.FromHandle(hIcon).Clone() as Icon;

                NativeMethods.DestroyIcon(hIcon);

                if (icon.ToBitmap().PixelFormat != Imaging.PixelFormat.Format32bppArgb)
                {
                    icon.Dispose();

                    imageList.GetIcon(fileInfo.iIcon, (int)NativeMethods.ILD_TRANSPARENT, ref hIcon);
                    icon = Icon.FromHandle(hIcon).Clone() as Icon;

                    NativeMethods.DestroyIcon(hIcon);
                }
            }

            return icon;
        }

        public static Bitmap ReadIconFromDll(string filePath)
        {
            uint _nIcons = PrivateExtractIcons(filePath, 0, 0, 0, null, null, 0, 0);
            IntPtr[] phicon = new IntPtr[_nIcons];
            uint[] piconid = new uint[_nIcons];
            uint nIcons = PrivateExtractIcons(filePath, 0, 48, 48, phicon, piconid, _nIcons, 0);

            for (int i = 0; i < nIcons; i++)
            {
                Icon icon = Icon.FromHandle(phicon[i]);
                Bitmap bitmap = icon.ToBitmap();
                icon.Dispose();
                NativeMethods.DestroyIcon(phicon[i]);

                if (bitmap.PixelFormat == Imaging.PixelFormat.Format32bppArgb)
                    return bitmap;
            }
            return null;
        }

        public static ImageSource ReadIcon(string iconPath)
        {
            if (File.Exists(iconPath))
            {
                var f = new FileInfo(iconPath);
                if (f.Extension.ToLower() == ".ico")
                {
                    Stream iconStream = new FileStream(iconPath, FileMode.Open, FileAccess.Read);
                    IconBitmapDecoder decoder = new IconBitmapDecoder(
                            iconStream,
                            BitmapCreateOptions.PreservePixelFormat,
                            BitmapCacheOption.None);
                    var reslist = decoder.Frames.ToList();

                    if (reslist.Count > 0)
                    {
                        return reslist.OrderBy(x => (x.Format == PixelFormats.Bgra32 ? 1 : 2)).ThenByDescending(x => x.PixelHeight).First();
                    }
                }
                else if (f.Extension.ToLower() == ".exe")
                {
                    Icon i = IconHelper.ReadIconFromExe(iconPath, IconSize.ExtraLarge);
                    return BitmapHelper.ToBitmapSource(i.ToBitmap());
                }
                else if (f.Extension.ToLower() == ".dll")
                {
                    Bitmap i = IconHelper.ReadIconFromDll(iconPath);
                    return BitmapHelper.ToBitmapSource(i);
                }
            }
            return null;
        }
    }
}
