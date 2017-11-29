//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Runtime.InteropServices;

namespace IliumVR.Bindings.Win32.User32
{
	internal static class NativeMethods
	{
		internal const string User32Dll = "user32.dll";

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, DeviceNotificationFlags flags);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool UnregisterDeviceNotification(IntPtr handle);
	}
}
