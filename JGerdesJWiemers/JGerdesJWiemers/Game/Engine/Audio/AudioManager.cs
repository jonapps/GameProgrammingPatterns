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
        private Hashtable _sounds;
        private static AudioManager _instance;
        private Sound _sound;
        

        private AudioManager()
        {
            _sounds = new Hashtable();
            _sound = new Sound();
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
                Sound s = ((Sound)_sounds[key]);
                s.Play();
                s.Loop = loop;
                s.Volume = volume;

            }
            catch (Exception e)
            {
                // sound not inizialized
            }
        }

        /// <summary>
        /// Plays a looped sound
        /// </summary>
        /// <param name="key">hashtable store key</param>
        public void PlayLoopSound(String key)
        {
            ((Sound)_sounds[key]).Loop = true;
            ((Sound)_sounds[key]).Play();
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
