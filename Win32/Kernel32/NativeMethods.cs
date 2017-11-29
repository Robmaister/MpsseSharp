//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.Kernel32
{
	internal static class NativeMethods
	{
		internal const string Kernel32Dll = "Kernel32.dll";

		[DllImport(Kernel32Dll, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeFileHandle CreateFile([MarshalAs(UnmanagedType.LPTStr)] string lpFileName, FileAccess dwDesiredAccess, FileShare dwShareMode, IntPtr lpSecurityAttributes, CreationDisposition dwCreationDisposition, FileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

		[DllImport(Kernel32Dll, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ReadFile(SafeFileHandle hFile, IntPtr lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, IntPtr lpOverlapped);

		[DllImport(Kernel32Dll, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool WriteFile(SafeFileHandle hFile, IntPtr lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, IntPtr lpOverlapped);

		[DllImport(Kernel32Dll, SetLastError = true)]
		internal static extern bool GetOverlappedResult(SafeFileHandle hFile, IntPtr lpOverlapped, out int lpNumberOfBytesTransferred, bool bWait);

		[DllImport(Kernel32Dll, SetLastError = true)]
		internal static extern bool CancelIo(SafeFileHandle hFile);
	}
}
