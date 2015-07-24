using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Audio
{
    public class VolumeSampleProvider : ISampleProvider
    {
        private ISampleProvider source;
        private float volume;

        public VolumeSampleProvider(ISampleProvider source)
        {
            this.volume = 1.0f;
            this.source = source;
        }

         public WaveFormat WaveFormat
        {
	        get 
            {
                return source.WaveFormat; 
            }
        }

        public int Read(float[] buffer, int offset, int sampleCount)
        {
            int samplesRead = source.Read(buffer, offset, sampleCount);

            for (int n = 0; n < sampleCount; n++)
            {
                buffer[offset + n] *= volume;
            }

            return samplesRead;
        }
        public float Volume
        {
            get { return volume; }
            set { volume = value; }
        }
    }
}
