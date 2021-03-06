﻿using SFML.Audio;
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
        public static readonly float GLOBAL_SOUND_VOLUME_MULTIPLYER = 0.2f;
        public static readonly float GLOBAL_MUSIC_VOLUME_MULTIPLYER = 1f;

        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;

        private ISampleProvider _music = null;
        private String _musicName = null;

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

        
        public void PlaySound(String soundname, float volume = 1)
        {
            volume *= GLOBAL_SOUND_VOLUME_MULTIPLYER;
            AddMixerInput(new CachedSoundSampleProvider(AssetLoader.Instance.GetSound(soundname)), volume);
        }

        public void PlayMusic(String musicName, float volume = 1, CachedSound musicData = null)
        {
            if(musicName != _musicName)
            {
                if (_music != null)
                {
                    mixer.RemoveMixerInput(_music);
                }
                if (musicData == null)
                {
                    musicData = AssetLoader.Instance.GetSound(musicName);
                }
                volume *= GLOBAL_MUSIC_VOLUME_MULTIPLYER;
                _music = AddMixerInput(new CachedSoundSampleProvider(musicData), volume);
                _musicName = musicName;
            }
        }


        private ISampleProvider AddMixerInput(ISampleProvider input)
        {
            ISampleProvider source = ConvertToRightChannelCount(input);
            mixer.AddMixerInput(source);
            return source;
        }

        private ISampleProvider AddMixerInput(ISampleProvider input, float volume)
        {
            VolumeSampleProvider volumeSampler = new VolumeSampleProvider(input);
            volumeSampler.Volume = volume;
            return AddMixerInput(volumeSampler);
        }

    

        public void Dispose()
        {
            outputDevice.Dispose();
        }

        public static readonly AudioManager Instance = new AudioManager(44100, 2);
    }
}
