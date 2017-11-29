//Copyright (c) 2015-2017 Ilium VR, Inc.
//Licensed under the MIT License - https://raw.github.com/IliumVR/ToolsBindings/master/LICENSE

namespace MpsseSharp
{
	//TODO extract to IliumVR.Bindings.Ftdi if we bind another FTDI library
	public enum FtdiStatus : uint
	{
		Ok,
		InvalidHandle,
		DeviceNotFound,
		DeviceNotOpened,
		IoError,
		InsufficientResources,
		InvalidParameter,
		InvalidBaudRate,
		DeviceNotOpenedForErase,
		DeviceNotOpenedForWrite,
		FailedToWriteDevice,
		EepromReadFailed,
		EepromWriteFailed,
		EepromEraseFailed,
		EepromNotPresent,
		EepromNotProgrammed,
		InvalidArgs,
		NotSupported,
		OtherError,
		DeviceListNotReady
	}
}
