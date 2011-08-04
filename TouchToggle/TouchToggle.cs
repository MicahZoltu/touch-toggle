using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TouchToggle
{
	public partial class MainForm : Form
	{
		private NotifyIcon mNotifyIcon;
		private ContextMenu mContextMenu;
		
		private Icon mIconEnabled;
		private Icon mIconDisabled;

		[STAThread]
		public static void Main()
		{
			Application.Run(new MainForm());\\\\
		}

		public MainForm()
		{
			mContextMenu = new ContextMenu();

			// Initialize the context menu
			mContextMenu.MenuItems.Add("E&xit", mMenuExit_Click);

			// Set up how the form should be displayed.
			Visible = false;
			ShowInTaskbar = false;
			WindowState = FormWindowState.Minimized;

			// Create the NotifyIcon.
			mNotifyIcon = new NotifyIcon();

			// The Icon property sets the icon that will appear in the systray for this application.
			mIconEnabled = new Icon(Icons.Green_Hand, 40, 40);
			mIconDisabled = new Icon(Icons.Red_Hand, 40, 40);
			mNotifyIcon.Icon = mIconEnabled;

			// The ContextMenu property sets the menu that will appear when the systray icon is right clicked.
			mNotifyIcon.ContextMenu = mContextMenu;

			// The Text property sets the text that will be displayed, in a tooltip, when the mouse hovers over the systray icon.
			mNotifyIcon.Text = "Touch Toggle";
			mNotifyIcon.Visible = true;

			// Handle the click event.
			mNotifyIcon.Click += new EventHandler(mNotifyIcon_Click);
		}

		protected override void Dispose(bool pDisposing)
		{
			if (pDisposing && mNotifyIcon != null) mNotifyIcon.Dispose();
			base.Dispose(pDisposing);
		}

		private void mMenuExit_Click(Object pSender, EventArgs pEventArgs)
		{
			Close();
		}

		private void mNotifyIcon_Click(Object pSender, EventArgs pEventArgs)
		{
			if (ToggleTouchMode()) mNotifyIcon.Icon = mIconEnabled;
			else mNotifyIcon.Icon = mIconDisabled;
		}

		private Boolean ToggleTouchMode()
		{
			RegistryKey lKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Wisp\Touch", true);
			Int32 lPreviousValue = (Int32)lKey.GetValue("TouchGate");
			if (lPreviousValue == 0)
			{
				lKey.SetValue("TouchGate", 1, RegistryValueKind.DWord);
				User32Utils.Notify_SettingChange();
				lKey.Close();
				return true;
			}
			else
			{
				lKey.SetValue("TouchGate", 0, RegistryValueKind.DWord);
				User32Utils.Notify_SettingChange();
				lKey.Close();
				return false;
			}
		}
	}
}
