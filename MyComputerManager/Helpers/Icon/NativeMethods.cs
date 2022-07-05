using System.Drawing.Interfaces;
using System.Drawing.Structs;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing
{
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		public const uint SHGFI_SYSICONINDEX = 0x4000;
		public const uint ILD_TRANSPARENT = 0x1;
		public const uint ILD_IMAGE = 0x20;
		public const uint FILE_ATTRIBUTE_NORMAL = 0x80;

		public const int RT_ICON = 3;
		public const int RT_GROUP_ICON = 14;

		[DllImport("shell32.dll", EntryPoint = "#727")]
		public static extern int SHGetImageList(int iImageList, ref Guid riid, ref IImageList ppv);

		[DllImport("shell32.dll", EntryPoint = "SHGetFileInfoW", CallingConvention = CallingConvention.StdCall)]
		public static extern int SHGetFileInfoW([MarshalAs(UnmanagedType.LPWStr)] string pszPath, uint dwFileAttributes, ref SHFILEINFOW psfi, int cbFileInfo, uint uFlags);

		[DllImport("user32.dll", EntryPoint = "DestroyIcon")]
		public static extern bool DestroyIcon(IntPtr hIcon);

		[DllImport("kernel32")]
		public static extern IntPtr BeginUpdateResource(string fileName, [MarshalAs(UnmanagedType.Bool)] bool deleteExistingResources);

		[DllImport("kernel32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UpdateResource(IntPtr hUpdate, IntPtr type, IntPtr name, short language, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] data, int dataSize);

		[DllImport("kernel32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EndUpdateResource(IntPtr hUpdate, [MarshalAs(UnmanagedType.Bool)] bool discard);

		[DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
	}
}
