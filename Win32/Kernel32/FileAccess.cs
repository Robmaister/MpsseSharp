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
	public enum FileAccess : uint
	{
		ReadData = 0x0001,        // file & pipe
		ListDirectory = 0x0001,       // directory
		WriteData = 0x0002,       // file & pipe
		AddFile = 0x0002,         // directory
		AppendData = 0x0004,      // file
		AddSubdirectory = 0x0004,     // directory
		CreatePipeInstance = 0x0004, // named pipe
		ReadExtendedAttributes = 0x0008,          // file & directory
		WriteExtendedAttributes = 0x0010,         // file & directory
		Execute = 0x0020,          // file
		Traverse = 0x0020,         // directory
		DeleteChild = 0x0040,     // directory
		ReadAttributes = 0x0080,      // all
		WriteAttributes = 0x0100,     // all

		Delete = 0x10000,
		ReadControl = 0x20000,
		WriteDAC = 0x40000,
		WriteOwner = 0x80000,
		Synchronize = 0x100000,

		StandardRightsRequired = 0xF0000,
		StandardRightsRead = ReadControl,
		StandardRightsWrite = ReadControl,
		StandardRightsExecute = ReadControl,
		StandardRightsAll = 0x1F0000,
		SpecificRightsAll = 0xFFFF,

		AccessSystemSecurity = 0x1000000,   // AccessSystemAcl access type
		MaximumAllowed = 0x2000000,     // MaximumAllowed access type

		AllAccess =
		StandardRightsRequired |
		Synchronize |
		0x1FF,

		FileGenericRead =
		StandardRightsRead |
		ReadData |
		ReadAttributes |
		ReadExtendedAttributes |
		Synchronize,

		FileGenericWrite =
		StandardRightsWrite |
		WriteData |
		WriteAttributes |
		WriteExtendedAttributes |
		AppendData |
		Synchronize,

		FileGenericExecute =
		StandardRightsExecute |
		  ReadAttributes |
		  Execute |
		  Synchronize,

		//
		// Generic Section
		//

		GenericRead = 0x80000000,
		GenericWrite = 0x40000000,
		GenericExecute = 0x20000000,
		GenericAll = 0x10000000,
	}
}
