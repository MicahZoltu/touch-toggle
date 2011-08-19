using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TouchToggle
{
	internal class User32Utils
	{
		#region USER32 Options
		public static IntPtr HWND_BROADCAST = new IntPtr(0xffff);

		/// <summary>
		/// Windows Messages
		/// Defined in winuser.h from Windows SDK v6.1
		/// Documentation pulled from MSDN.
		/// </summary>
		public enum WM : int
		{
			/// <summary>
			/// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
			/// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
			/// </summary>
			WININICHANGE = 0x001A,
			/// <summary>
			/// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
			/// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
			/// </summary>
			SETTINGCHANGE = WM.WININICHANGE,
		}
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
		static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);
		#endregion

		internal static void Notify_SettingChange()
		{
			SendNotifyMessage(HWND_BROADCAST, (uint)WM.SETTINGCHANGE, UIntPtr.Zero, IntPtr.Zero);
		}
	}
}
