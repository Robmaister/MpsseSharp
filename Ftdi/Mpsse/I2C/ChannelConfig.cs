//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Runtime.InteropServices;

namespace IliumVR.Bindings.Ftdi.Mpsse.I2C
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ChannelConfig
	{
		private ClockRate clockRate;
		private byte latencyTimer;
		private ChannelOptions options;

		public ChannelConfig(ClockRate clockRate, byte latencyTimer, ChannelOptions options)
		{
			this.clockRate = clockRate;
			this.latencyTimer = latencyTimer;
			this.options = options;
		}
	}
}
