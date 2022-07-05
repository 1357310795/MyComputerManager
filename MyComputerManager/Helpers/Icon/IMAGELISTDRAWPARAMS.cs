using System.Runtime.InteropServices;

namespace System.Drawing.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct IMAGELISTDRAWPARAMS
	{
		public int cbSize;
		public IntPtr himl;
		public int i;
		public IntPtr hdcDst;
		public int x;
		public int y;
		public int cx;
		public int cy;
		public int xBitmap;
		public int yBitmap;
		public int rgbBk;
		public int rgbFg;
		public int fStyle;
		public int dwRop;
		public int fState;
		public int Frame;
		public int crEffect;
	}
}
