//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.Hid
{
	[StructLayout(LayoutKind.Explicit)]
	//[StructLayout(LayoutKind.Sequential)]
	public struct CapsRangeUnion
	{
		[FieldOffset(0)]
		public CapsRange Range;

		[FieldOffset(0)]
		public CapsNotRange NotRange;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct CapsRange
	{
		public ushort UsageMin;
		public ushort UsageMax;
		public ushort StringMin;
		public ushort StringMax;
		public ushort DesignatorMin;
		public ushort DesignatorMax;
		public ushort DataIndexMin;
		public ushort DataIndexMax;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct CapsNotRange
	{
		public ushort Usage;
		public ushort Reserved1;
		public ushort StringIndex;
		public ushort Reserved2;
		public ushort DesignatorIndex;
		public ushort Reserved3;
		public ushort DataIndex;
		public ushort Reserved4;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Caps
	{
		public ushort Usage;
		public ushort UsagePage;
		public ushort InputReportByteLength;
		public ushort OutputReportByteLength;
		public ushort FeatureReportByteLength;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
		public ushort[] Reserved;

		public ushort NumberLinkCollectionNodes;
		public ushort NumberInputButtonCaps;
		public ushort NumberInputValueCaps;
		public ushort NumberInputDataIndices;
		public ushort NumberOutputButtonCaps;
		public ushort NumberOutputValueCaps;
		public ushort NumberOutputDataIndices;
		public ushort NumberFeatureButtonCaps;
		public ushort NumberFeatureValueCaps;
		public ushort NumberFeatureDataIndices;
	}
}
