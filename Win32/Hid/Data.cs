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
	public struct DataValueUnion
	{
		[FieldOffset(0)]
		public uint RawValue;

		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.U1)]
		public bool On;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Data
	{
		public ushort DataIndex;
		public ushort Reserved;

		public DataValueUnion Value;
	}
}
