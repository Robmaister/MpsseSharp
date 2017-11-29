//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpsseSharp.I2C
{
	[Flags]
	public enum ChannelOptions
	{
		DisableThreePhaseClocking	= 0x0001,
		EnableDriveOnlyZero			= 0x0002
	}
}
