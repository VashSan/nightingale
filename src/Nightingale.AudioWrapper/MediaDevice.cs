using System;
using NAudio.CoreAudioApi;

namespace Nightingale.AudioWrapper
{
	public enum DevicePresence
	{
		Active,
		Unavailable
	}

	public enum DeviceUsage
	{
		Input,
		Output,
		InputAndOutput
	}

	public class MediaDevice : IMediaDevice
    {
	    private readonly MMDevice myDevice;

	    internal MediaDevice( MMDevice device )
	    {
		    myDevice = device;
	    }

	    public string Name => myDevice.FriendlyName;
	    public string DeviceName => myDevice.DeviceFriendlyName;
	    public DevicePresence State
	    {
		    get
		    {
			    if ( myDevice.State == DeviceState.All )
			    {
				    throw new ArgumentOutOfRangeException();
			    }
			    return myDevice.State == DeviceState.Active ? DevicePresence.Active : DevicePresence.Unavailable;
		    }
	    }
		public DeviceUsage Usage
	    {
		    get
		    {
			    switch ( myDevice.DataFlow )
			    {
				    case DataFlow.Render:
					    return DeviceUsage.Output;
					    
				    case DataFlow.Capture:
					    return DeviceUsage.Input;

				    case DataFlow.All:
					    return DeviceUsage.InputAndOutput;

				    default:
					    throw new ArgumentOutOfRangeException();
			    }
		    }
	    }


		public IVolume Volume => new Volume( myDevice );
    }
}
