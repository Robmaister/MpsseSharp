//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;
using System.Threading;

namespace IliumVR.Bindings.Win32.Hid
{
	internal static class NativeMethods
	{
		internal const string HidDll = "hid.dll";

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_FlushQueue(SafeFileHandle HidDeviceObject);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_FreePreparsedData(IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetAttributes(SafeFileHandle HidDeviceObject, out Attributes Attributes);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetFeature(SafeFileHandle HidDeviceObject, IntPtr ReportBuffer, uint ReportBufferLength);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetHidGuid(out Guid Guid);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetIndexedString(SafeFileHandle HidDeviceObject, uint StringIndex, IntPtr Buffer, uint BufferLength);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetInputReport(SafeFileHandle HidDeviceObject, IntPtr ReportBuffer, uint ReportBufferLength);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetManufacturerString(SafeFileHandle HidDeviceObject, IntPtr Buffer, uint BufferLength);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetNumInputBuffers(SafeFileHandle HidDeviceObject, out uint NumberBuffers);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetPhysicalDescriptor(SafeFileHandle HidDeviceObject, IntPtr Buffer, uint BufferLength);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetPreparsedData(SafeFileHandle HidDeviceObject, out IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetProductString(SafeFileHandle HidDeviceObject, IntPtr Buffer, uint BufferLength);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_GetSerialNumberString(SafeFileHandle HidDeviceObject, IntPtr Buffer, uint BufferLength);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_SetFeature(SafeFileHandle HidDeviceObject, IntPtr ReportBuffer, uint ReportBufferLength);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_SetNumInputBuffers(SafeFileHandle HidDeviceObject, uint NumberBuffers);

		[DllImport(HidDll, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		internal static extern bool HidD_SetOutputReport(SafeFileHandle HidDeviceObject, IntPtr ReportBuffer, uint ReportBufferLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetButtonCaps(ReportType ReportType, ButtonCaps[] ButtonCaps, ref ushort ButtonCapsLength, IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetCaps(IntPtr PreparsedData, out Caps Capabilities);

		//TODO support later if necessary
		//[DllImport(DllName, SetLastError = true)]
		//internal static extern HidPStatus HidP_GetCollectionDescription(IntPtr ReportDesc, uint DescLength, PoolType PoolType, out DeviceDesc DeviceDescription);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetData(ReportType ReportType, Data[] DataList, ref uint DataLength, IntPtr PreparsedData, byte Report, uint ReportLength);

		
		//[DllImport(DllName, SetLastError = true)]
		//internal static extern HidPStatus HidP_GetExtendedAttributes(ReportType ReportType, ushort DataIndex, IntPtr PreparsedData, out ExtendedAttributes Attributes, out uint LengthAttributes);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetLinkCollectionNodes(LinkCollectionNode[] LinkCollectionNodes, ref uint LinkCollectionNodesLength, IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetScaledUsageValue(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort Usage, out int UsageValue, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetSpecificButtonCaps(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort Usage, ButtonCaps[] ButtonCaps, ref ushort ButtonCapsLength, IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetSpecificValueCaps(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort Usage, ValueCaps[] ButtonCaps, ref ushort ButtonCapsLength, IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetUsages(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort[] UsageList, ref uint UsageLength, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetUsagesEx(ReportType ReportType, ushort LinkCollection, UsageAndPage[] ButtonList, ref uint UsageLength, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetUsageValue(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort Usage, out uint UsageValue, ref uint UsageLength, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetUsageValueArray(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort Usage, IntPtr UsageValue, ref ushort UsageValueByteLength, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetUsageValueArray(ReportType ReportType, ValueCaps[] caps, ref ushort ValueCapsLength, IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_GetValueCaps(ReportType ReportType, IntPtr caps, ref ushort ValueCapsLength, IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_InitializeReportForID(ReportType ReportType, byte ReportID, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_IsSameUsageAndPage(UsageAndPage u1, UsageAndPage u2);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_MaxDataListLength(ReportType ReportType, IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_MaxUsageListLength(ReportType ReportType, ushort UsagePage, IntPtr PreparsedData);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_SetData(ReportType ReportType, Data[] DataList, ref uint DataLength, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_SetScaledUsageValue(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort Usage, int UsageValue, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_SetUsages(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort[] UsageList, ref uint UsageLength, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_SetUsageValue(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort Usage, uint UsageValue, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_SetUsageValueArray(ReportType ReportType, ushort UsagePage, ushort LinkCollection, IntPtr UsageValue, ushort UsageValueByteLength, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		//[DllImport(DllName, SetLastError = true)]
		//internal static extern HidPStatus HidP_TranslateUsagesToI8042ScanCodes(ushort[] ChangedUsageList, uint UsageListLength, KeyboardDirection KeyAction, KeyboardModifierState ModifierState, IntPtr InsertCodesProcedure, IntPtr InsertCodesContext);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_UnsetUsages(ReportType ReportType, ushort UsagePage, ushort LinkCollection, ushort[] UsageList, ref uint UsageLength, IntPtr PreparsedData, IntPtr Report, uint ReportLength);

		[DllImport(HidDll, SetLastError = true)]
		internal static extern HidPStatus HidP_UsageListDifference(ushort[] PreviousUsageList, ushort[] CurrentUsageList, ushort[] BreakUsageList, ushort[] MakeUsageList, uint UsageListLength);
	}
}
