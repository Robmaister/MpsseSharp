﻿//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.Kernel32
{
	[Flags]
	public enum FileShare : uint
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0x00000000,
		/// <summary>
		/// Enables subsequent open operations on an object to request read access. 
		/// Otherwise, other processes cannot open the object if they request read access. 
		/// If this flag is not specified, but the object has been opened for read access, the function fails.
		/// </summary>
		Read = 0x00000001,
		/// <summary>
		/// Enables subsequent open operations on an object to request write access. 
		/// Otherwise, other processes cannot open the object if they request write access. 
		/// If this flag is not specified, but the object has been opened for write access, the function fails.
		/// </summary>
		Write = 0x00000002,
		/// <summary>
		/// Enables subsequent open operations on an object to request delete access. 
		/// Otherwise, other processes cannot open the object if they request delete access.
		/// If this flag is not specified, but the object has been opened for delete access, the function fails.
		/// </summary>
		Delete = 0x00000004
	}
}
