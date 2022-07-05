using System.Runtime.InteropServices;

namespace System.Drawing.Structs
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}
}
