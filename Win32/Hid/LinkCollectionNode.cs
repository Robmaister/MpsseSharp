//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.Hid
{
	[StructLayout(LayoutKind.Sequential)]
	public struct LinkCollectionNode
	{
		public ushort LinkUsage;
		public ushort LinkUsagePage;
		public ushort Parent;
		public ushort NumberOfChildren;
		public ushort NextSibling;
		public ushort FirstChild;
		public uint BitfieldTypeAliasReserved;
		public IntPtr UserContext;

		public uint CollectionType { get { return BitfieldTypeAliasReserved & 0xFF; } }

		public bool IsAlias { get { return ((BitfieldTypeAliasReserved >> 8) & 0x1) != 0x0; } }

		public uint Reserved { get { return BitfieldTypeAliasReserved >> 9; } }
	}
}
