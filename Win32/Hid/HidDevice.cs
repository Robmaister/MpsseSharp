//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using IliumVR.Bindings.Win32.Kernel32;

namespace IliumVR.Bindings.Win32.Hid
{
	public class HidDevice : IDisposable
	{
		private bool disposed = false;
		private SafeFileHandle reference;
		private PreparsedData preparsedData;

		private byte[] strBuffer;
		private byte[] inBuffer, outBuffer, featureBuffer;

		private List<ValueCaps> inputValueCaps, outputValueCaps, featureValueCaps;

		private ManualResetEvent fileEvent;
		private SafeOverlapped fileOverlapped;

		private object lockObject;

		public HidDevice(string path)
		{
			//device mutex
			lockObject = new object();

			//static buffers
			strBuffer = new byte[256];

			//open device
			reference = Kernel32.File.Create(path, Kernel32.FileAccess.GenericWrite, Kernel32.FileShare.Read | Kernel32.FileShare.Write, IntPtr.Zero, CreationDisposition.OpenExisting, Kernel32.FileAttributes.Overlapped, IntPtr.Zero);

			if (reference.IsInvalid)
				return;

			//overlapped
			fileEvent = new ManualResetEvent(false);
			fileOverlapped = new SafeOverlapped(fileEvent);

			//get preparsed data
			IntPtr preparsed;
			if (!NativeMethods.HidD_GetPreparsedData(reference, out preparsed))
				return;

			preparsedData = new PreparsedData(preparsed);

			//transfer buffers
			Caps capabilities = preparsedData.Capabilities;
			inBuffer = new byte[capabilities.InputReportByteLength];
			outBuffer = new byte[capabilities.OutputReportByteLength];
			featureBuffer = new byte[capabilities.FeatureReportByteLength];

			//dynamic caps
			inputValueCaps = new List<ValueCaps>();
			outputValueCaps = new List<ValueCaps>();
			featureValueCaps = new List<ValueCaps>();

			ushort numInputCaps = capabilities.NumberInputValueCaps;
			ushort numOutputCaps = capabilities.NumberOutputValueCaps;
			ushort numFeatureCaps = capabilities.NumberFeatureValueCaps;

			ushort maxValueCaps = Math.Max(numInputCaps, numOutputCaps);
			maxValueCaps = Math.Max(maxValueCaps, numFeatureCaps);

			int valueCapSize = Marshal.SizeOf<ValueCaps>();
			IntPtr valueCapPtr = Marshal.AllocHGlobal(maxValueCaps * valueCapSize);

			HidPStatus stat = NativeMethods.HidP_GetValueCaps(ReportType.Input, valueCapPtr, ref numInputCaps, preparsed);

			for (int i = 0; i < numInputCaps; i++)
			{
				ValueCaps val = Marshal.PtrToStructure<ValueCaps>(new IntPtr(valueCapPtr.ToInt64() + (i * valueCapSize)));
				inputValueCaps.Add(val);
			}

			stat = NativeMethods.HidP_GetValueCaps(ReportType.Output, valueCapPtr, ref numOutputCaps, preparsed);

			for (int i = 0; i < numOutputCaps; i++)
			{
				ValueCaps val = Marshal.PtrToStructure<ValueCaps>(new IntPtr(valueCapPtr.ToInt64() + i * valueCapSize));
				outputValueCaps.Add(val);
			}

			stat = NativeMethods.HidP_GetValueCaps(ReportType.Feature, valueCapPtr, ref numFeatureCaps, preparsed);

			for (int i = 0; i < numFeatureCaps; i++)
			{
				ValueCaps val = Marshal.PtrToStructure<ValueCaps>(new IntPtr(valueCapPtr.ToInt64() + i * valueCapSize));
				featureValueCaps.Add(val);
			}

			Marshal.FreeHGlobal(valueCapPtr);
		}

		~HidDevice()
		{
			Dispose(false);
		}

		public static Guid Guid
		{
			get
			{
				Guid ret;
				if (!NativeMethods.HidD_GetHidGuid(out ret))
					throw new Win32Exception();

				return ret;
			}
		}

		public string SerialNumber
		{
			get
			{
				string ret = null;

				lock (lockObject)
				{
					if (reference == null || reference.IsInvalid)
						return ret;

					GCHandle strHandle = GCHandle.Alloc(strBuffer, GCHandleType.Pinned);
					if (!NativeMethods.HidD_GetSerialNumberString(reference, strHandle.AddrOfPinnedObject(), (uint)strBuffer.Length))
					{
						strHandle.Free();
						int err = Marshal.GetLastWin32Error();

						if (err == 87) //device doesn't support serial number
							return ret;
						else
							throw new Win32Exception();
					}

					strHandle.Free();

					ret = Encoding.Unicode.GetString(strBuffer);
				}

				return ret;
			}
		}

		public string Manufacturer
		{
			get
			{
				string ret = null;

				lock (lockObject)
				{
					if (reference == null || reference.IsInvalid)
						return ret;

					GCHandle strHandle = GCHandle.Alloc(strBuffer, GCHandleType.Pinned);
					if (!NativeMethods.HidD_GetManufacturerString(reference, strHandle.AddrOfPinnedObject(), (uint)strBuffer.Length))
					{
						strHandle.Free();
						int err = Marshal.GetLastWin32Error();

						if (err == 87) //device doesn't support serial number
							return ret;
						else
							throw new Win32Exception();
					}

					strHandle.Free();

					ret = Encoding.Unicode.GetString(strBuffer);
				}
				return ret;
			}
		}
		
		public string Product
		{
			get
			{
				string ret = null;

				lock (lockObject)
				{
					if (reference == null || reference.IsInvalid)
						return ret;

					GCHandle strHandle = GCHandle.Alloc(strBuffer, GCHandleType.Pinned);
					if (!NativeMethods.HidD_GetProductString(reference, strHandle.AddrOfPinnedObject(), (uint)strBuffer.Length))
					{
						strHandle.Free();
						int err = Marshal.GetLastWin32Error();

						if (err == 87) //device doesn't support serial number
							return ret;
						else
							throw new Win32Exception();
					}

					strHandle.Free();

					ret = Encoding.Unicode.GetString(strBuffer);
				}
				return ret;
			}
		}

		public Attributes Attributes
		{
			get
			{
				lock (lockObject)
				{
					if (reference == null)
						throw new InvalidOperationException("The HID reference is null.");
					else if (reference.IsInvalid)
						throw new InvalidOperationException("The HID reference is invalid");

					Attributes att;
					att.Size = Marshal.SizeOf<Attributes>();
					if (!NativeMethods.HidD_GetAttributes(reference, out att))
						throw new Win32Exception();

					return att;
				}
			}
		}

		public PreparsedData PreparsedData
		{
			get
			{
				return preparsedData;
			}
		}

		public bool IsInvalid
		{
			get
			{
				return reference.IsInvalid;
			}
		}

		public IReadOnlyCollection<ValueCaps> InputValueCaps
		{
			get
			{
				return inputValueCaps.AsReadOnly();
			}
		}

		public IReadOnlyCollection<ValueCaps> OutputValueCaps
		{
			get
			{
				return outputValueCaps.AsReadOnly();
			}
		}

		public IReadOnlyCollection<ValueCaps> FeatureValueCaps
		{
			get
			{
				return featureValueCaps.AsReadOnly();
			}
		}

		private GCHandle CopyStructToBuffer<T>(byte[] buffer, T data, out int size)
			where T : struct
		{
			size = Marshal.SizeOf(data);

			// >= because inBuffer[0] is the report ID, copying starts at [1].
			if (size >= buffer.Length)
				throw new ArgumentOutOfRangeException(nameof(data), "Struct is too large to fit in the input report buffer");

			GCHandle gch = GCHandle.Alloc(inBuffer, GCHandleType.Pinned);
			Marshal.StructureToPtr(buffer, new IntPtr(gch.AddrOfPinnedObject().ToInt64() + 1), false);

			return gch;
		}

		public int GetReportLength(ReportType type, byte reportId)
		{
			int bitLength = 0;

			List<ValueCaps> caps = null;

			switch (type)
			{
				case ReportType.Input:
					caps = inputValueCaps;
					break;
				case ReportType.Output:
					caps = outputValueCaps;
					break;
				case ReportType.Feature:
					caps = featureValueCaps;
					break;
			}

			if (caps == null)
				return 0;

			for (int i = 0; i < caps.Count; i++)
			{
				ValueCaps c = caps[i];
				if (c.ReportID == reportId)
					bitLength += c.ReportCount * c.BitSize;
			}
			return bitLength / 8;
		}

		public bool FlushQueue()
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				return NativeMethods.HidD_FlushQueue(reference);
			}
		}

		public bool Read(out byte reportId, byte[] data, int timeout = 0)
		{
			return Read(out reportId, data, 0, data.Length, timeout);
		}

		public bool Read(out byte reportId, byte[] data, int offset, int count, int timeout = 0)
		{
			reportId = 0;

			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				GCHandle gch = GCHandle.Alloc(inBuffer, GCHandleType.Pinned);

				fileEvent.Reset();

				int bytesRead = 0;
				if (!Kernel32.File.ReadAsync(reference, gch.AddrOfPinnedObject(), inBuffer.Length, ref bytesRead, fileOverlapped))
				{
					if (Marshal.GetLastWin32Error() != 997) // ERROR_IO_PENDING
						throw new Win32Exception();

					if (!fileEvent.WaitOne(timeout))
					{
						Kernel32.File.CancelIo(reference);
						//fileEvent.Reset();
						gch.Free();
						return false;
					}
				}

				if (!Kernel32.File.GetOverlappedResult(reference, fileOverlapped, out bytesRead, false))
					throw new Win32Exception();

				gch.Free();

				reportId = inBuffer[0];
				Buffer.BlockCopy(inBuffer, 1, data, offset, bytesRead - 1);
			}

			return true;
		}

		public bool Read(byte[] rawBuffer, int timeout = 0)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				GCHandle gch = GCHandle.Alloc(rawBuffer, GCHandleType.Pinned);

				fileEvent.Reset();

				int bytesRead = 0;
				if (!Kernel32.File.ReadAsync(reference, gch.AddrOfPinnedObject(), rawBuffer.Length, ref bytesRead, fileOverlapped))
				{
					if (Marshal.GetLastWin32Error() != 997) // ERROR_IO_PENDING
						throw new Win32Exception();

					if (!fileEvent.WaitOne(timeout))
					{
						Kernel32.File.CancelIo(reference);
						fileEvent.Reset();
						gch.Free();
						return false;
					}
				}

				if (!Kernel32.File.GetOverlappedResult(reference, fileOverlapped, out bytesRead, false))
					throw new Win32Exception();

				gch.Free();
			}

			return true;
		}

		public T Read<T>(out byte reportId, int timeout = 0)
			where T : struct
		{
			reportId = 0;

			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				GCHandle gch = GCHandle.Alloc(inBuffer, GCHandleType.Pinned);

				fileEvent.Reset();

				int bytesRead = 0;
				if (!Kernel32.File.ReadAsync(reference, gch.AddrOfPinnedObject(), inBuffer.Length, ref bytesRead, fileOverlapped))
				{
					if (Marshal.GetLastWin32Error() != 997) // ERROR_IO_PENDING
						throw new Win32Exception();

					if (!fileEvent.WaitOne(timeout))
					{
						Kernel32.File.CancelIo(reference);
						//fileEvent.Reset();
						gch.Free();
						return default(T); //TODO better return
					}
				}

				if (!Kernel32.File.GetOverlappedResult(reference, fileOverlapped, out bytesRead, false))
					throw new Win32Exception();

				reportId = inBuffer[0];
				T val = Marshal.PtrToStructure<T>(new IntPtr(gch.AddrOfPinnedObject().ToInt64() + 1));

				gch.Free();

				return val;
			}
		}

		public bool Write(byte reportId, byte[] data, int timeout = 0)
		{
			return Write(reportId, data, 0, data.Length, timeout);
		}

		public bool Write(byte reportId, byte[] data, int count, int timeout = 0)
		{
			return Write(reportId, data, 0, count, timeout);
		}

		public bool Write(byte reportId, byte[] data, int offset, int count, int timeout = 0)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				outBuffer[0] = reportId;

				Buffer.BlockCopy(data, offset, outBuffer, 1, count);

				GCHandle gch = GCHandle.Alloc(outBuffer, GCHandleType.Pinned);

				fileEvent.Reset();

				int bytesWritten = 0;
				if (!Kernel32.File.WriteAsync(reference, gch.AddrOfPinnedObject(), outBuffer.Length, ref bytesWritten, fileOverlapped))
				{
					if (Marshal.GetLastWin32Error() != 997) // ERROR_IO_PENDING
						throw new Win32Exception();

					if (!fileEvent.WaitOne(timeout))
					{
						Kernel32.File.CancelIo(reference);
						//fileEvent.Reset();
						gch.Free();
						return false;
					}
				}

				if (!Kernel32.File.GetOverlappedResult(reference, fileOverlapped, out bytesWritten, false))
					throw new Win32Exception();

				gch.Free();

				return true;
			}
		}

		public bool Write(byte[] rawBuffer, int timeout = 0)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				GCHandle gch = GCHandle.Alloc(rawBuffer, GCHandleType.Pinned);

				fileEvent.Reset();

				int bytesWritten = 0;
				if (!Kernel32.File.WriteAsync(reference, gch.AddrOfPinnedObject(), rawBuffer.Length, ref bytesWritten, fileOverlapped))
				{
					if (Marshal.GetLastWin32Error() != 997) // ERROR_IO_PENDING
						throw new Win32Exception();

					if (!fileEvent.WaitOne(timeout))
					{
						Kernel32.File.CancelIo(reference);
						//fileEvent.Reset();
						gch.Free();
						return false;
					}
				}

				if (!Kernel32.File.GetOverlappedResult(reference, fileOverlapped, out bytesWritten, false))
					throw new Win32Exception();

				gch.Free();

				return true;
			}
		}

		public bool Write<T>(byte reportId, T buffer, int timeout = 0)
			where T : struct
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				outBuffer[0] = reportId;

				int size;
				GCHandle gch = CopyStructToBuffer(outBuffer, buffer, out size);

				fileEvent.Reset();

				int bytesWritten = 0;
				if (!Kernel32.File.WriteAsync(reference, gch.AddrOfPinnedObject(), outBuffer.Length, ref bytesWritten, fileOverlapped))
				{
					if (Marshal.GetLastWin32Error() != 997) // ERROR_IO_PENDING
						throw new Win32Exception();

					if (!fileEvent.WaitOne(timeout))
					{
						Kernel32.File.CancelIo(reference);
						//fileEvent.Reset();
						gch.Free();
						return false;
					}
				}

				if (!Kernel32.File.GetOverlappedResult(reference, fileOverlapped, out bytesWritten, false))
					throw new Win32Exception();

				gch.Free();

				return true;
			}
		}

		public bool GetInputReport(byte reportId, byte[] data)
		{
			return GetInputReport(reportId, data, 0, data.Length);
		}

		public bool GetInputReport(byte reportId, byte[] data, int count)
		{
			return GetInputReport(reportId, data, 0, count);
		}

		public bool GetInputReport(byte reportId, byte[] data, int offset, int count)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				inBuffer[0] = reportId;

				GCHandle gch = GCHandle.Alloc(inBuffer, GCHandleType.Pinned);

				bool ret = NativeMethods.HidD_GetInputReport(reference, gch.AddrOfPinnedObject(), (uint)(count + 1));

				gch.Free();

				if (ret)
					Buffer.BlockCopy(inBuffer, 1, data, offset, count);

				return ret;
			}
		}

		public bool GetInputReport(byte[] rawBuffer)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				GCHandle gch = GCHandle.Alloc(rawBuffer, GCHandleType.Pinned);

				bool ret = NativeMethods.HidD_GetInputReport(reference, gch.AddrOfPinnedObject(), (uint)rawBuffer.Length);

				gch.Free();

				return ret;
			}
		}

		public bool SetOutputReport(byte reportId, byte[] data)
		{
			return SetOutputReport(reportId, data, 0, data.Length);
		}

		public bool SetOutputReport(byte reportId, byte[] data, int count)
		{
			return SetOutputReport(reportId, data, 0, count);
		}

		public bool SetOutputReport(byte reportId, byte[] data, int offset, int count)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				outBuffer[0] = reportId;

				GCHandle gch = GCHandle.Alloc(outBuffer, GCHandleType.Pinned);

				bool ret = NativeMethods.HidD_SetOutputReport(reference, gch.AddrOfPinnedObject(), (uint)(count + 1));

				gch.Free();

				if (ret)
					Buffer.BlockCopy(outBuffer, 1, data, offset, count);

				return ret;
			}
		}
		public bool SetOutputReport(byte[] rawBuffer)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				GCHandle gch = GCHandle.Alloc(rawBuffer, GCHandleType.Pinned);

				bool ret = NativeMethods.HidD_SetOutputReport(reference, gch.AddrOfPinnedObject(), (uint)rawBuffer.Length);

				gch.Free();

				return ret;
			}
		}

		public bool GetFeature(byte reportId, byte[] data)
		{
			return GetFeature(reportId, data, 0, data.Length);
		}

		public bool GetFeature(byte reportId, byte[] data, int count)
		{
			return GetFeature(reportId, data, 0, count);
		}

		public bool GetFeature(byte reportId, byte[] data, int offset, int count)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				featureBuffer[0] = reportId;

				GCHandle gch = GCHandle.Alloc(featureBuffer, GCHandleType.Pinned);

				bool ret = NativeMethods.HidD_GetFeature(reference, gch.AddrOfPinnedObject(), (uint)(count + 1));

				gch.Free();

				if (ret)
					Buffer.BlockCopy(featureBuffer, 1, data, offset, count);

				return ret;
			}
		}

		public bool GetFeature<T>(byte reportId, T data)
			where T : struct
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				featureBuffer[0] = reportId;

				GCHandle gch = GCHandle.Alloc(featureBuffer, GCHandleType.Pinned);

				bool ret = NativeMethods.HidD_GetFeature(reference, gch.AddrOfPinnedObject(), (uint)Marshal.SizeOf<T>());

				if (ret)
					data = Marshal.PtrToStructure<T>(gch.AddrOfPinnedObject());

				gch.Free();

				return ret;
			}
		}

		public bool GetFeature(byte[] rawBuffer, int count)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				GCHandle gch = GCHandle.Alloc(rawBuffer, GCHandleType.Pinned);

				bool ret = NativeMethods.HidD_GetFeature(reference, gch.AddrOfPinnedObject(), (uint)count);

				gch.Free();

				return ret;
			}
		}

		public bool SetFeature(byte reportId, byte[] data)
		{
			return SetFeature(reportId, data, 0, data.Length);
		}

		public bool SetFeature(byte reportId, byte[] data, int count)
		{
			return SetFeature(reportId, data, 0, count);
		}

		public bool SetFeature(byte reportId, byte[] data, int offset, int count)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				featureBuffer[0] = reportId;

				Buffer.BlockCopy(featureBuffer, 1, data, offset, count);

				GCHandle gch = GCHandle.Alloc(featureBuffer, GCHandleType.Pinned);

				bool ret = NativeMethods.HidD_SetFeature(reference, gch.AddrOfPinnedObject(), (uint)(count + 1));

				gch.Free();

				return ret;
			}
		}

		public bool SetFeature<T>(byte reportId, T buffer)
			where T : struct
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				featureBuffer[0] = reportId;

				int size;
				GCHandle gch = CopyStructToBuffer(featureBuffer, buffer, out size);

				bool ret = NativeMethods.HidD_SetFeature(reference, gch.AddrOfPinnedObject(), (uint)(size + 1));

				gch.Free();

				return ret;
			}
		}

		public bool SetFeature(byte[] rawBuffer, int count)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				GCHandle gch = GCHandle.Alloc(rawBuffer, GCHandleType.Pinned);

				bool ret = NativeMethods.HidD_SetFeature(reference, gch.AddrOfPinnedObject(), (uint)count);

				gch.Free();

				return ret;
			}
		}

		public uint GetNumberInputBuffers()
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				uint ret;
				if (!NativeMethods.HidD_GetNumInputBuffers(reference, out ret))
					throw new Win32Exception();

				return ret;
			}
		}

		public bool SetNumberInputBuffers(uint buffers)
		{
			lock (lockObject)
			{
				if (reference == null)
					throw new InvalidOperationException("The HID reference is null.");
				else if (reference.IsInvalid)
					throw new InvalidOperationException("The HID reference is invalid");

				return NativeMethods.HidD_SetNumInputBuffers(reference, buffers);
			}
		}

		public string GetIndexedString(uint index)
		{
			string ret = null;

			lock (lockObject)
			{
				if (reference == null || reference.IsInvalid)
					return ret;

				GCHandle strHandle = GCHandle.Alloc(strBuffer, GCHandleType.Pinned);
				if (!NativeMethods.HidD_GetIndexedString(reference, index, strHandle.AddrOfPinnedObject(), (uint)strBuffer.Length))
				{
					strHandle.Free();
					return ret;
				}

				strHandle.Free();

				ret = Encoding.Unicode.GetString(strBuffer);
			}

			return ret;
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
				disposed = true;

				if (disposing)
				{

				}

				if (reference != null)
					reference.Dispose();

				if (fileOverlapped != null)
					fileOverlapped.Dispose();

				if (fileEvent != null)
					fileEvent.Dispose();

				if (preparsedData != null)
					preparsedData.Dispose();
			}
		}

		
	}
}
