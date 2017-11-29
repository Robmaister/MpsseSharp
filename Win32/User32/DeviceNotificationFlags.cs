//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.User32
{
	[Flags]
	public enum DeviceNotificationFlags
	{
		WindowHandle	= 0x00,
		ServiceHandle	= 0x01,
		AllInterfaces	= 0x04
	}
}
