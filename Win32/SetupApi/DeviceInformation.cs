//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.SetupApi
{
	public static class DeviceInformation
	{
		public static List<string> GetAllDevicePaths(Guid guid, GetClassFlags flags)
		{
			List<string> list = new List<string>();

			IntPtr devInfo = NativeMethods.SetupDiGetClassDevs(ref guid, null, IntPtr.Zero, flags);//GetClassFlags.Present | GetClassFlags.DeviceInterface);

			if (devInfo == new IntPtr(-1))
				return list;

			SetupDeviceInterfaceData devIface = new SetupDeviceInterfaceData();
			IntPtr devDetails;

			int index = 0;
			while (true)
			{
				devIface.cbSize = Marshal.SizeOf(typeof(SetupDeviceInterfaceData));

				if (!NativeMethods.SetupDiEnumDeviceInterfaces(devInfo, IntPtr.Zero, ref guid, index, ref devIface))
				{
					NativeMethods.SetupDiDestroyDeviceInfoList(devInfo);
					devInfo = IntPtr.Zero;
					return list;
				}
				else if (Marshal.GetLastWin32Error() == 259) //ERROR_NO_MORE_ITEMS
				{
					break;
				}

				index++;

				int detailsSize = 0;
				NativeMethods.SetupDiGetDeviceInterfaceDetail(devInfo, ref devIface, IntPtr.Zero, 0, ref detailsSize, IntPtr.Zero);

				devDetails = Marshal.AllocHGlobal(detailsSize);
				Marshal.WriteInt32(devDetails, Marshal.OffsetOf<SetupDeviceInterfaceDetailData>(nameof(SetupDeviceInterfaceDetailData.cbSize)).ToInt32(), Marshal.SizeOf<SetupDeviceInterfaceDetailData>());
				NativeMethods.SetupDiGetDeviceInterfaceDetail(devInfo, ref devIface, devDetails, detailsSize, IntPtr.Zero, IntPtr.Zero);

				string val = Marshal.PtrToStringAuto(new IntPtr(devDetails.ToInt64() + Marshal.OffsetOf<SetupDeviceInterfaceDetailData>(nameof(SetupDeviceInterfaceDetailData.DevicePath)).ToInt64()));
				list.Add(val);

				Marshal.FreeHGlobal(devDetails);
			}

			return list;
		}
	}
}
