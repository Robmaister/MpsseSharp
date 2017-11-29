//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

using System;
using System.Runtime.InteropServices;

namespace IliumVR.Bindings.Ftdi.Mpsse.I2C
{
	/// <summary>
	/// A channel class for communicating with I2C devices over MPSSE.
	/// </summary>
	public class Channel : IDisposable
	{
		private bool disposed;
		private bool duplicate;
		private IntPtr handle;

		/// <summary>
		/// Initializes a new instance of the Channel class from a channel
		/// index.
		/// </summary>
		/// <remarks>
		/// This constructor replaces I2C_OpenChannel.
		/// </remarks>
		/// <param name="index">The index of the channel.</param>
		public Channel(uint index)
		{
			FtdiStatus status = NativeMethods.I2C_OpenChannel(index, out handle);
			if (status != FtdiStatus.Ok)
				Dispose();
		}

		/// <summary>
		/// Initializes a new instance of the Channel class from a channel
		/// index and calls <see cref="Initialize(ref ChannelConfig)"/>.
		/// </summary>
		/// <param name="index">The index of the channel.</param>
		/// <param name="config">The configuration for this channel.</param>
		public Channel(uint index, ref ChannelConfig config)
			: this(index)
		{
			if (!IsDisposed)
				Initialize(ref config);
		}

		/// <summary>
		/// Initializes a new instance of the Channel class directly from a handle.
		/// </summary>
		/// <param name="handle">The channel's handle.</param>
		internal Channel(IntPtr handle)
		{
			this.handle = handle;
			duplicate = true;
		}

		/// <summary>
		/// Destroys an instance of the Channel class by disposing it.
		/// </summary>
		~Channel()
		{
			Dispose(false);
		}

		/// <summary>
		/// Gets a value indicating whether or not the Channel instance has been disposed.
		/// </summary>
		public bool IsDisposed { get { return disposed; } }

		/// <summary>
		/// Creates a Channel instance from a DeviceListInfoNode. This replaces directly using
		/// the handle field from a node.
		/// </summary>
		/// <param name="node">The node containing information about this channel.</param>
		/// <returns>An instance of the Channel class if one already exists.</returns>
		public static Channel FromDeviceListInfoNode(ref DeviceListInfoNode node)
		{
			if (node.Handle == IntPtr.Zero)
				return null;
			else
				return new Channel(node.Handle);
		}

		/// <summary>
		/// Gets the number of I2C channels available on the system.
		/// </summary>
		/// <remarks>
		/// The indices of the channels are 0 up to the returned value.
		/// </remarks>
		/// <returns>The number of channels.</returns>
		public static uint GetNumChannels()
		{
			uint ret;
			NativeMethods.I2C_GetNumChannels(out ret);
			return ret;
		}

		/// <summary>
		/// Gets detailed information about one of the I2C channels available on the system.
		/// </summary>
		/// <param name="index">The channel index.</param>
		/// <returns>Detailed information about the channel.</returns>
		public static DeviceListInfoNode GetChannelInfo(uint index)
		{
			DeviceListInfoNode ret;
			NativeMethods.I2C_GetChannelInfo(index, out ret);
			return ret;
		}

		/// <summary>
		/// Initializes the channel with a provided configuration.
		/// </summary>
		/// <param name="config">The configuration for this channel.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus Initialize(ref ChannelConfig config)
		{
			return NativeMethods.I2C_InitChannel(handle, ref config);
		}

		/// <summary>
		/// Read from an I2C device.
		/// </summary>
		/// <param name="deviceAddress">The address of the device to read from.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to read to.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus Read(byte deviceAddress, uint sizeToTransfer, IntPtr buffer, out uint sizeTransferred, TransferOptions options)
		{
			return NativeMethods.I2C_DeviceRead(handle, deviceAddress, sizeToTransfer, buffer, out sizeTransferred, options);
		}

		/// <summary>
		/// Read from an I2C device.
		/// </summary>
		/// <typeparam name="T">The type of data to transfer in buffer.</typeparam>
		/// <param name="deviceAddress">The address of the device to read from.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to read to.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus Read<T>(byte deviceAddress, uint sizeToTransfer, T[] buffer, out uint sizeTransferred, TransferOptions options)
			where T : struct
		{
			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			FtdiStatus status = Read(deviceAddress, sizeToTransfer, handle.AddrOfPinnedObject(), out sizeTransferred, options);
			handle.Free();

			return status;
		}

		/// <summary>
		/// Read from an I2C device.
		/// </summary>
		/// <typeparam name="T">The type of data to transfer in buffer.</typeparam>
		/// <param name="deviceAddress">The address of the device to read from.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to read to.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus Read<T>(byte deviceAddress, uint sizeToTransfer, T[,] buffer, out uint sizeTransferred, TransferOptions options)
			where T : struct
		{
			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			FtdiStatus status = Read(deviceAddress, sizeToTransfer, handle.AddrOfPinnedObject(), out sizeTransferred, options);
			handle.Free();

			return status;
		}

		/// <summary>
		/// Writes to an I2C device.
		/// </summary>
		/// <typeparam name="T">The type of data to transfer in buffer.</typeparam>
		/// <param name="deviceAddress">The address of the device to write to.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to write from.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus Write(byte deviceAddress, uint sizeToTransfer, IntPtr buffer, out uint sizeTransferred, TransferOptions options)
		{
			return NativeMethods.I2C_DeviceWrite(handle, deviceAddress, sizeToTransfer, buffer, out sizeTransferred, options);
		}

		/// <summary>
		/// Writes to an I2C device.
		/// </summary>
		/// <typeparam name="T">The type of data to transfer in buffer.</typeparam>
		/// <param name="deviceAddress">The address of the device to write to.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to write from.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus Write<T>(byte deviceAddress, uint sizeToTransfer, T buffer, out uint sizeTransferred, TransferOptions options)
			where T : struct
		{
			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			FtdiStatus status = Write(deviceAddress, sizeToTransfer, handle.AddrOfPinnedObject(), out sizeTransferred, options);
			handle.Free();

			return status;
		}

		/// <summary>
		/// Writes to an I2C device.
		/// </summary>
		/// <typeparam name="T">The type of data to transfer in buffer.</typeparam>
		/// <param name="deviceAddress">The address of the device to write to.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to write from.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus Write<T>(byte deviceAddress, uint sizeToTransfer, T[] buffer, out uint sizeTransferred, TransferOptions options)
			where T : struct
		{
			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			FtdiStatus status = Write(deviceAddress, sizeToTransfer, handle.AddrOfPinnedObject(), out sizeTransferred, options);
			handle.Free();

			return status;
		}

		/// <summary>
		/// Writes to an I2C device.
		/// </summary>
		/// <typeparam name="T">The type of data to transfer in buffer.</typeparam>
		/// <param name="deviceAddress">The address of the device to write to.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to write from.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus Write<T>(byte deviceAddress, uint sizeToTransfer, T[,] buffer, out uint sizeTransferred, TransferOptions options)
			where T : struct
		{
			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			FtdiStatus status = Write(deviceAddress, sizeToTransfer, handle.AddrOfPinnedObject(), out sizeTransferred, options);
			handle.Free();

			return status;
		}

		/// <summary>
		/// Reads from the 8 GPIO lines associated with the high byte of the channel.
		/// </summary>
		/// <param name="value">
		/// As input, each bit represents the direction of the GPIO lines, 0 for input and 1 for output.
		/// As output, each bit represents the logic state of the GPIO lines, 0 for low and 1 for high.
		/// </param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus ReadGPIO(ref byte value)
		{
			return NativeMethods.FT_ReadGPIO(handle, out value);
		}

		/// <summary>
		/// Writes to the 8 GPIO lines associated with the high byte o the channel.
		/// </summary>
		/// <param name="dir">Each bit represents the direction of the GPIO lines, 0 for input and 1 for output.</param>
		/// <param name="value">Each bit represents the logic state of the output lines, 0 for low and 1 for high.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus WriteGPIO(byte dir, byte value)
		{
			return NativeMethods.FT_WriteGPIO(handle, dir, value);
		}

		/// <summary>
		/// Reads from an I2C Register.
		/// </summary>
		/// <remarks>
		/// This function calls <see cref="Write(byte, uint, IntPtr, out uint, TransferOptions)"/> with the register address
		/// and then calls <see cref="Read(byte, uint, IntPtr, out uint, TransferOptions)"/> with the given buffer.
		/// </remarks>
		/// <param name="deviceAddress">The address of the device to write to.</param>
		/// <param name="registerAddress">The address of the register to read from.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to write from.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus ReadFromRegister(byte deviceAddress, byte registerAddress, uint sizeToTransfer, IntPtr buffer, out uint sizeTransferred, TransferOptions writeOptions, TransferOptions readOptions)
		{
			FtdiStatus status = Write(deviceAddress, 1, registerAddress, out sizeTransferred, writeOptions);
			if (status != FtdiStatus.Ok)
				return status;

			status = Read(deviceAddress, sizeToTransfer, buffer, out sizeTransferred, readOptions);

			/*if (sizeToTransfer > 1)
				return Read(deviceAddress, sizeToTransfer, buffer + 1, out sizeTransferred, readOptions);
			else*/
				return status;
		}

		/// <summary>
		/// Reads from an I2C Register.
		/// </summary>
		/// <remarks>
		/// This function calls <see cref="Write{T}(byte, uint, T[], out uint, TransferOptions)"/> with the register address
		/// and then calls <see cref="Read{T}(byte, uint, T[], out uint, TransferOptions)"/> with the given buffer.
		/// </remarks>
		/// <param name="deviceAddress">The address of the device to write to.</param>
		/// <param name="registerAddress">The address of the register to read from.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to write from.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus ReadFromRegister<T>(byte deviceAddress, byte registerAddress, uint sizeToTransfer, T[] buffer, out uint sizeTransferred, TransferOptions writeOptions, TransferOptions readOptions)
			where T : struct
		{
			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			FtdiStatus status = ReadFromRegister(deviceAddress, registerAddress, sizeToTransfer, handle.AddrOfPinnedObject(), out sizeTransferred, writeOptions, readOptions);
			handle.Free();

			return status;
		}

		/// <summary>
		/// Reads from an I2C Register.
		/// </summary>
		/// <remarks>
		/// This function calls <see cref="Write{T}(byte, uint, T[,], out uint, TransferOptions)"/> with the register address
		/// and then calls <see cref="Read{T}(byte, uint, T[,], out uint, TransferOptions)"/> with the given buffer.
		/// </remarks>
		/// <param name="deviceAddress">The address of the device to write to.</param>
		/// <param name="registerAddress">The address of the register to read from.</param>
		/// <param name="sizeToTransfer">The size, in bytes, to transfer.</param>
		/// <param name="buffer">The buffer to write from.</param>
		/// <param name="sizeTransferred">The size, in bytes, that were transferred.</param>
		/// <param name="options">The options to transfer with.</param>
		/// <returns>The status after calling this method.</returns>
		public FtdiStatus ReadFromRegister<T>(byte deviceAddress, byte registerAddress, uint sizeToTransfer, T[,] buffer, out uint sizeTransferred, TransferOptions writeOptions, TransferOptions readOptions)
			where T : struct
		{
			GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			FtdiStatus status = ReadFromRegister(deviceAddress, registerAddress, sizeToTransfer, handle.AddrOfPinnedObject(), out sizeTransferred, writeOptions, readOptions);
			handle.Free();

			return status;
		}

		/// <summary>
		/// Disposes of all the resources this instance of Channel is using.
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Dispose(true);
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;
				
				if (handle != IntPtr.Zero && !duplicate)
				{
					NativeMethods.I2C_CloseChannel(handle);
					handle = IntPtr.Zero;
				}
			}
		}
	}
}
