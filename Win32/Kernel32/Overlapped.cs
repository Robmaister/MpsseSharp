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
	public class SafeOverlapped : IDisposable
	{
		private NativeOverlapped overlapped;
		private SafeWaitHandle waitHandle;
		private GCHandle pinnedHandle;

		private bool disposed = false;

		public SafeOverlapped()
		{
			pinnedHandle = GCHandle.Alloc(overlapped, GCHandleType.Pinned);
		}

		public SafeOverlapped(WaitHandle wait)
			: this()
		{
			this.waitHandle = wait.SafeWaitHandle;
		}

		~SafeOverlapped()
		{
			Dispose(false);
		}

		public bool IsDisposed
		{
			get
			{
				return disposed;
			}
		}

		public IntPtr InternalLow
		{
			get { return overlapped.InternalLow; }
			set { overlapped.InternalLow = value; }
		}

		public IntPtr InternalHigh
		{
			get { return overlapped.InternalHigh; }
			set { overlapped.InternalHigh = value; }
		}

		public int OffsetLow
		{
			get { return overlapped.OffsetLow; }
			set { overlapped.OffsetLow = value; }
		}

		public int OffsetHigh
		{
			get { return overlapped.OffsetHigh; }
			set { overlapped.OffsetHigh = value; }
		}

		public SafeWaitHandle EventHandle
		{
			get { return waitHandle; }
			set { overlapped.EventHandle = value.DangerousGetHandle(); waitHandle = value; }
		}

		public IntPtr PinnedHandle
		{
			get { return pinnedHandle.AddrOfPinnedObject(); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
				}

				pinnedHandle.Free();
				waitHandle = null;
				overlapped.EventHandle = IntPtr.Zero;

				disposed = true;
			}
		}
	}
}
