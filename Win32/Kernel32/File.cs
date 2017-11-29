//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.Kernel32
{
	public static class File
	{
		public static SafeFileHandle Create(string path, Kernel32.FileAccess access, FileShare shareMode, IntPtr securityAttributes, CreationDisposition disposition, FileAttributes flagsAndAttributes, IntPtr templateFile)
		{
			return NativeMethods.CreateFile(path, access, shareMode, securityAttributes, disposition, flagsAndAttributes, templateFile);
		}

		public static bool Read(SafeFileHandle file, IntPtr buffer, int numberOfBytesToRead)
		{
			int unused = 0;
			return Read(file, buffer, numberOfBytesToRead, ref unused);
		}

		public static bool Read(SafeFileHandle file, IntPtr buffer, int numberOfBytesToRead, ref int numberOfBytesRead)
		{
			return NativeMethods.ReadFile(file, buffer, numberOfBytesToRead, ref numberOfBytesRead, IntPtr.Zero);
		}

		public static bool ReadAsync(SafeFileHandle file, IntPtr buffer, int numberOfBytesToRead, SafeOverlapped overlapped)
		{
			int unused = 0;
			return ReadAsync(file, buffer, numberOfBytesToRead, ref unused, overlapped);
		}

		public static bool ReadAsync(SafeFileHandle file, IntPtr buffer, int numberOfBytesToRead, ref int numberOfBytesRead, SafeOverlapped overlapped)
		{
			return NativeMethods.ReadFile(file, buffer, numberOfBytesToRead, ref numberOfBytesRead, overlapped.PinnedHandle);
		}

		public static bool Write(SafeFileHandle file, IntPtr buffer, int numberOfBytesToWrite)
		{
			int unused = 0;
			return Write(file, buffer, numberOfBytesToWrite, ref unused);
		}

		public static bool Write(SafeFileHandle file, IntPtr buffer, int numberOfBytesToWrite, ref int numberOfBytesWritten)
		{
			return NativeMethods.WriteFile(file, buffer, numberOfBytesToWrite, ref numberOfBytesWritten, IntPtr.Zero);
		}

		public static bool WriteAsync(SafeFileHandle file, IntPtr buffer, int numberOfBytesToWrite, SafeOverlapped overlapped)
		{
			int unused = 0;
			return WriteAsync(file, buffer, numberOfBytesToWrite, ref unused, overlapped);
		}

		public static bool WriteAsync(SafeFileHandle file, IntPtr buffer, int numberOfBytesToWrite, ref int numberOfBytesWritten, SafeOverlapped overlapped)
		{
			return NativeMethods.WriteFile(file, buffer, numberOfBytesToWrite, ref numberOfBytesWritten, overlapped.PinnedHandle);
		}

		public static bool GetOverlappedResult(SafeFileHandle file, SafeOverlapped overlapped, out int numberOfBytesTransferred, bool wait)
		{
			return NativeMethods.GetOverlappedResult(file, overlapped.PinnedHandle, out numberOfBytesTransferred, wait);
		}

		public static bool CancelIo(SafeFileHandle file)
		{
			return NativeMethods.CancelIo(file);
		}
	}
}
