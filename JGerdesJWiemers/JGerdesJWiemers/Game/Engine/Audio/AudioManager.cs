using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Media;
using JGerdesJWiemers.Game.Engine.Utils;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace JGerdesJWiemers.Game.Engine.Audio
{

    //Partly adapted from https://gist.github.com/markheath/8783999
    class AudioManager
    {
        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;

        public AudioManager(int sampleRate = 44100, int channelCount = 2)
        {
            outputDevice = new WaveOutEvent();
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;
            outputDevice.Init(mixer);
            outputDevice.Play();
        }

       
        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
            {
                return input;
            }
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }
            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }

        public void Play(CachedSound sound)
        {
            AddMixerInput(new CachedSoundSampleProvider(sound));
        }

        public void Play(String soundname, float volume = 1)
        {
            AddMixerInput(new CachedSoundSampleProvider(AssetLoader.Instance.GetSound(soundname)), volume);
        }

        private void AddMixerInput(ISampleProvider input)
        {
            mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        private void AddMixerInput(ISampleProvider input, float volume)
        {
            VolumeSampleProvider volumeSampler = new VolumeSampleProvider(input);
            volumeSampler.Volume = volume;
            AddMixerInput(volumeSampler);
        }

        public void Dispose()
        {
            outputDevice.Dispose();
        }

        public static readonly AudioManager Instance = new AudioManager(44100, 2);
    }
}
