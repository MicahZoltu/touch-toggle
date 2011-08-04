using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TouchToggle
{
	internal class User32Utils
	{
		#region USER32 Options
		static IntPtr HWND_BROADCAST = new IntPtr(0xffffL);
		static IntPtr WM_SETTINGCHANGE = new IntPtr(0x1a);
		#endregion

		#region STRUCT
		enum SendMessageTimeoutFlags : uint
		{
			SMTO_NORMAL = 0x0000,
			SMTO_BLOCK = 0x0001,
			SMTO_ABORTIFHUNG = 0x2,
			SMTO_NOTIMEOUTIFNOTHUNG = 0x0008
		}
		#endregion

		#region Interop

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, UIntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);
		#endregion

		internal static void Notify_SettingChange()
		{
			UIntPtr result;
			SendMessageTimeout(HWND_BROADCAST, (uint)WM_SETTINGCHANGE, UIntPtr.Zero, UIntPtr.Zero, SendMessageTimeoutFlags.SMTO_NORMAL, 1000, out result);
		}
	}
}
