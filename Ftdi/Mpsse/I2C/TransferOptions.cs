//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Ftdi.Mpsse.I2C
{
	[Flags]
	public enum TransferOptions
	{
		StartBit			= 0x00000001,
		StopBit				= 0x00000002,
		BreakOnNack			= 0x00000004,
		NackLastByte		= 0x00000008,
		FastTransferBytes	= 0x00000010,
		FastTransferBits	= 0x00000020,
		FastTransfer		= FastTransferBytes | FastTransferBits,
		NoAddress			= 0x00000040
	}
}
