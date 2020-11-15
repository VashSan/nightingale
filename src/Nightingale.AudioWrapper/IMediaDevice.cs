namespace Nightingale.AudioWrapper
{
	public interface IMediaDevice
	{
		string Name { get; }
		string DeviceName { get; }
		DevicePresence State { get; }
		DeviceUsage Usage { get; }
		IVolume Volume { get; }
	}
}