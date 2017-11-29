//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IliumVR.Bindings.Win32.Hid
{
	public class PreparsedData : IDisposable
	{
		private bool disposed = false;
		private IntPtr reference;

		internal PreparsedData(IntPtr ptr)
		{
			this.reference = ptr;
		}

		~PreparsedData()
		{
			Dispose(false);
		}

		public Caps Capabilities
		{
			get
			{
				Caps caps;
				if (NativeMethods.HidP_GetCaps(reference, out caps) != HidPStatus.Success)
					throw new Win32Exception();

				return caps;
			}
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

				disposed = true;

				NativeMethods.HidD_FreePreparsedData(reference);
			}
		}

		

		
	}
}
