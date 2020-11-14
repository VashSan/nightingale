using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Mixer;

namespace Nightingale.Core
{
	public class AudioDeviceObserver
	{
		private readonly MMDeviceEnumerator myDeviceEnumerator = new MMDeviceEnumerator();

		public AudioDeviceObserver()
		{
			RefreshDevices();
		}

		private void RefreshDevices()
		{
			var deviceCollection = myDeviceEnumerator.EnumerateAudioEndPoints( DataFlow.All, DeviceState.All );
			while ( true )
			{
				Console.CursorTop = 0;
				Console.CursorLeft = 0;
				foreach ( var device in deviceCollection )
				{
					Dump( device );
				}
				Thread.Sleep( TimeSpan.FromSeconds( 1 ) );
			}
		}

		private static void Dump( MMDevice device )
		{
			CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
			switch ( device.State )
			{
				case DeviceState.Unplugged: // no cable connected?
				case DeviceState.Disabled: // in windows disabled by user or system
				case DeviceState.Active:
					Console.WriteLine( $"{device.State.ToString().PadRight( 15 )}\t{device.DataFlow.ToString().PadRight( 15 )}\t{device.FriendlyName}" );
					if ( device.State == DeviceState.Active )
					{
						var v = device.AudioEndpointVolume;
						Console.WriteLine($"\tVolume: {v.MasterVolumeLevel} ( {v.VolumeRange.MinDecibels} -  {v.VolumeRange.MaxDecibels}, {v.VolumeRange.IncrementDecibels})");
					}
					break;

				case DeviceState.NotPresent:
					// Console.WriteLine( "device not present" );
					break;

				case DeviceState.All:
				// fall through
				default:
					throw new ArgumentOutOfRangeException( $"Unknown or invalid device state: {device.State}" );
			}
		}
	}
}