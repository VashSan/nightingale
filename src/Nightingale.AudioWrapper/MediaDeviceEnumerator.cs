using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;

namespace Nightingale.AudioWrapper
{
	public class MediaDeviceEnumerator : IMediaDeviceEnumerator
	{
		private readonly MMDeviceEnumerator myEnumerator = new MMDeviceEnumerator();

		public IEnumerator<IMediaDevice> GetEnumerator()
		{
			var allDeviceCollection = myEnumerator.EnumerateAudioEndPoints( DataFlow.All, DeviceState.All );
			return allDeviceCollection.Select( d => new MediaDevice( d ) ).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
