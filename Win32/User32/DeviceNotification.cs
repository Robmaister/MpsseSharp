//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.User32
{
	public static class DeviceNotification
	{
		public static IntPtr Register(IntPtr hWnd, Guid classGuid, DeviceNotificationFlags flags)
		{
			DevBroadcastDeviceInterface dbi = new DevBroadcastDeviceInterface();
			dbi.dbcc_size = Marshal.SizeOf<DevBroadcastDeviceInterface>();
			dbi.dbcc_devicetype = DeviceType.DeviceInterface;
			dbi.dbcc_classguid = classGuid;

			GCHandle gch = GCHandle.Alloc(dbi, GCHandleType.Pinned);

			IntPtr handle = NativeMethods.RegisterDeviceNotification(hWnd, gch.AddrOfPinnedObject(), flags);

			gch.Free();

			return handle;
		}

		public static string GetDeviceName(IntPtr lParam)
		{
			return Marshal.PtrToStringAuto(new IntPtr(lParam.ToInt64() + Marshal.OffsetOf<DevBroadcastDeviceInterface>(nameof(DevBroadcastDeviceInterface.dbcc_name)).ToInt64()));
		}

		public static bool Unregister(IntPtr handle)
		{
			return NativeMethods.UnregisterDeviceNotification(handle);
		}


	}
}
