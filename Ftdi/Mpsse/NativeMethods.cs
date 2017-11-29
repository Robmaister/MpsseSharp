//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Runtime.InteropServices;

namespace IliumVR.Bindings.Ftdi.Mpsse
{
	internal static class NativeMethods
	{
		internal const string DllName = "libMPSSE.dll";

		internal const CallingConvention DllCallConv = CallingConvention.Cdecl;
		internal const CharSet DllCharSet = CharSet.Ansi;

		#region I2C Functions

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus I2C_GetNumChannels(out uint numChannels);

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus I2C_GetChannelInfo(uint index, out DeviceListInfoNode chanInfo);

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus I2C_OpenChannel(uint index, out IntPtr handle);

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus I2C_InitChannel(IntPtr handle, ref I2C.ChannelConfig config);

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus I2C_CloseChannel(IntPtr handle);

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus I2C_DeviceRead(IntPtr handle, uint deviceAddress, uint sizeToTransfer, IntPtr buffer, out uint sizeTransferred, I2C.TransferOptions options);

		/*[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus I2C_DeviceRead<T>(IntPtr handle, uint deviceAddress, uint sizeToTransfer, ref T buffer, out uint sizeTransferred, I2C.TransferOptions options)
			where T : struct;*/

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus I2C_DeviceWrite(IntPtr handle, uint deviceAddress, uint sizeToTransfer, IntPtr buffer, out uint sizeTransferred, I2C.TransferOptions options);

		#endregion

		#region libMPSSE

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern void Init_libMPSSE();

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern void Cleanup_libMPSSE();

		#endregion

		#region GPIO

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus FT_WriteGPIO(IntPtr handle, byte dir, byte value);

		[DllImport(DllName, CallingConvention = DllCallConv)]
		internal static extern FtdiStatus FT_ReadGPIO(IntPtr handle, out byte value);

		#endregion

	}
}
