using System;
using System.Runtime.InteropServices;

namespace LC3_VM
{
    static class External
    {
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetStdHandle(int nStdHandle);

		[DllImport("kernel32.dll")]
		public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

		[DllImport("kernel32.dll")]
		public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

		[DllImport("kernel32.dll")]
		public static extern bool FlushConsoleInputBuffer(IntPtr hConsoleInput);

		[DllImport("kernel32.dll")]
		public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
	}
}