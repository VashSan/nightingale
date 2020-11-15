using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nightingale.AudioWrapper;


namespace Nightingale.Core
{
	public class AudioDeviceObserver
	{
		private readonly MediaDeviceEnumerator myDeviceEnumerator = new MediaDeviceEnumerator();

		public AudioDeviceObserver()
		{
			RefreshDevices();
		}

		private void RefreshDevices()
		{
			while ( true )
			{
				Console.CursorTop = 0;
				Console.CursorLeft = 0;
				foreach ( var device in myDeviceEnumerator.Where( DeviceStateAvailableFunc() ) )
				{
					Dump( device );
				}
				Thread.Sleep( TimeSpan.FromSeconds( 1 ) );
			}
		}

		private static Func<IMediaDevice, bool> DeviceStateAvailableFunc()
		{
			return d => d.State == DevicePresence.Active;
		}

		private static void Dump( IMediaDevice device )
		{
			Console.WriteLine( $"{device.State.ToString().PadRight( 15 )}\t{device.Usage.ToString().PadRight( 15 )}\t{device.Name}" );
			var v = device.Volume;
			Console.WriteLine($"\tVolume: {v.Level} ( {v.MinLevel} -  {v.MaxLevel}, {v.Increment})");
		}
	}
}