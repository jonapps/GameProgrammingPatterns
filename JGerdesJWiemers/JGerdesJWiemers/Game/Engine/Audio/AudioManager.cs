using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace JGerdesJWiemers.Game.Engine.Audio
{
    class AudioManager
    {
        private Dictionary<String, Sound> _sounds;
        private List<String> _stoppedLoopSounds;
        private static AudioManager _instance;
        private Sound _sound;

        private List<Sound> _plaingSounds;

        private bool _silent = false;
        

        private AudioManager()
        {
            _plaingSounds = new List<Sound>();
            _sounds = new Dictionary<String, Sound>();
            _sound = new Sound();
            _stoppedLoopSounds = new List<string>();
        }


        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AudioManager();
                }
                return _instance;
            }
        }


        public bool Silent
        {
            get
            {
                return _silent;
            }
            set
            {
                _silent = value;
                if (value)
                {
                    foreach (var sound in _sounds)
                    {
                        if(sound.Value.Loop){
                            _stoppedLoopSounds.Add(sound.Key);
                        }
                        sound.Value.Stop();   
                    }
                }
                else
                {
                    foreach (var sound in _stoppedLoopSounds)
                    {
                        System.Console.WriteLine(sound);
                        Play(sound);
                    }
                    _stoppedLoopSounds.Clear();
                }
                
            }
        }

        /// <summary>
        /// creats a sound from string path and adds it to a hashtable 
        /// </summary>
        /// <param name="key">storing key</param>
        /// <param name="pathToSound">Path to the Soundfile</param>
        public void AddSound(String key, String pathToSound)
        {
            try
            {
                _sounds.Add(key, new Sound(new SoundBuffer(pathToSound)));
            }
            catch (Exception e)
            {
                //todo
                System.Console.WriteLine("SoundError");
            }
            
        }


        /// <summary>
        /// plays a sound from the hashtable
        /// </summary>
        /// <param name="key">hashtable store key</param>
        /// <param name="volume"></param>
        /// <param name="loop"></param>
        public void Play(String key, float volume = 100, bool loop = false)
        {
            try
            {
                if (!_silent)
                {
                    Sound s = _sounds[key];
                    Sound ns = new Sound();
                    ns.SoundBuffer = s.SoundBuffer;
                    ns.Loop = loop;
                    ns.Volume = volume;
                    ns.Play();
                    _plaingSounds.Add(ns);
                    //s.
                    //s.Loop = loop;
                    //s.Volume = volume;
                    //s.Play();
                }
            }
            catch (Exception e)
            {
                // sound not inizialized
            }
        }


        public void Stop(String key)
        {
            try
            {
                Sound s = _sounds[key];
                s.Stop();

            }
            catch (Exception e)
            {
                // sound not inizialized
            }
        }

        /// <summary>
        /// returns a sound from the sound hashtable
        /// </summary>
        /// <param name="key">hashtable store key</param>
        /// <returns>Sound</returns>
        public Sound GetSound(String key)
        {
            return (Sound)_sounds[key];
        }

        


    }
}
