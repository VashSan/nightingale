using NAudio.CoreAudioApi;

namespace Nightingale.AudioWrapper
{
	public interface IVolume
	{
		float Level { get; }
		float MaxLevel { get; }
		float MinLevel { get; }
		float Increment { get; }
	}

	public class Volume : IVolume
	{
		private readonly MMDevice myDevice;

		internal Volume( MMDevice device )
		{
			myDevice = device;
		}

		public float Level => IsActive() ? myDevice.AudioEndpointVolume.MasterVolumeLevel : 0;
		public float MaxLevel => IsActive() ? myDevice.AudioEndpointVolume.VolumeRange.MaxDecibels : 0;
		public float MinLevel => IsActive() ? myDevice.AudioEndpointVolume.VolumeRange.MinDecibels : 0;
		public float Increment => IsActive() ? myDevice.AudioEndpointVolume.VolumeRange.IncrementDecibels : 0;

		private bool IsActive()
		{
			return myDevice.State == DeviceState.Active;
		}
	}
}
