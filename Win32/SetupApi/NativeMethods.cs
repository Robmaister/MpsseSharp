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
	internal static class NativeMethods
	{
		internal const string SetupApiDll = "SetupAPI.dll";

		[DllImport(SetupApiDll, SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern IntPtr SetupDiGetClassDevs(IntPtr ClassGuid, [MarshalAs(UnmanagedType.LPTStr)] string Enumerator, IntPtr hwndParent, GetClassFlags Flags);

		[DllImport(SetupApiDll, SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, [MarshalAs(UnmanagedType.LPTStr)] string Enumerator, IntPtr hwndParent, GetClassFlags Flags);

		[DllImport(SetupApiDll, SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern bool SetupDiEnumDeviceInterfaces(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, ref Guid InterfaceClassGuid, int MemberIndex, ref SetupDeviceInterfaceData DeviceInterfaceData);

		[DllImport(SetupApiDll, SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SetupDeviceInterfaceData DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, ref int RequiredSize, IntPtr DeviceInfoData);

		[DllImport(SetupApiDll, SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr DeviceInfoSet, ref SetupDeviceInterfaceData DeviceInterfaceData, IntPtr DeviceInterfaceDetailData, int DeviceInterfaceDetailDataSize, IntPtr RequiredSize, IntPtr DeviceInfoData);

		[DllImport(SetupApiDll, SetLastError = true)]
		internal static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);
	}
}
