﻿//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.Hid
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Attributes
	{
		internal int Size;
		public ushort VendorId;
		public ushort ProductId;
		public ushort VersionNumber;
	}
}
