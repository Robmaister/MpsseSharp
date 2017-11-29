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
	[StructLayout(LayoutKind.Sequential)]
	internal struct SetupDevInfoData
	{
		internal int cbSize;
		internal Guid ClassGuid;
		internal int DevInst;
		internal UIntPtr Reserved;
	}
}
