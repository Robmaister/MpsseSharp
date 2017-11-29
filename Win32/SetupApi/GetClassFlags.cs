//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.SetupApi
{
	[Flags]
	public enum GetClassFlags : uint
	{
		Default				= 0x01,
		Present				= 0x02,
		AllClasses			= 0x04,
		Profile				= 0x08,
		DeviceInterface		= 0x10
	}
}
