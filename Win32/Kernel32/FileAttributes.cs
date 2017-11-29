//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.Kernel32
{
	[Flags]
	public enum FileAttributes : uint
	{
		Readonly =			0x00000001,
		Hidden =			0x00000002,
		System =			0x00000004,
		Directory =			0x00000010,
		Archive =			0x00000020,
		Device =			0x00000040,
		Normal =			0x00000080,
		Temporary =			0x00000100,
		SparseFile =		0x00000200,
		ReparsePoint =		0x00000400,
		Compressed =		0x00000800,
		Offline =			0x00001000,
		NotContentIndexed =	0x00002000,
		Encrypted =			0x00004000,
		FirstPipeInstance = 0x00080000,
		OpenNoRecall =		0x00100000,
		OpenReparsePoint =	0x00200000,
		PosixSemantics =	0x01000000,
		BackupSemantics =	0x02000000,
		DeleteOnClose =		0x04000000,
		SequentialScan =	0x08000000,
		RandomAccess =		0x10000000,
		NoBuffering =		0x20000000,
		Overlapped =		0x40000000,
		Write_Through =		0x80000000,
	}
}
