//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Runtime.InteropServices;

namespace IliumVR.Bindings.Ftdi.Mpsse
{
	[Flags]
	public enum DeviceInfoFlags : uint
	{
		Opened = 1,
		HiSpeed = 2
	}

	[StructLayout(LayoutKind.Sequential, CharSet = NativeMethods.DllCharSet)]
	public struct DeviceListInfoNode
	{
		public DeviceInfoFlags Flags;
		public uint Type;
		public uint Id;
		public uint LocId;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string SerialNumber;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string Description;

		internal IntPtr Handle;
	}
}
