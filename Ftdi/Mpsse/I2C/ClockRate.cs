//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Ftdi.Mpsse.I2C
{
	public enum ClockRate
	{
		StandardMode	= 100000,
		FastMode		= 400000,
		FastModePlus	= 1000000,
		HighSpeedMode	= 3400000
	}
}
