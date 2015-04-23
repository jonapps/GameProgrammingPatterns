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


        public void AddSound(String key, String pathToSound)
        {

            _sounds.Add(key, new Sound(new SoundBuffer(pathToSound)));
        }

        public void Play(String key)
        {
            ((Sound)_sounds[key]).Play();
            System.Console.WriteLine(((Sound)_sounds[key]).ToString());
        }

        public Sound GetSound(String key)
        {
            return (Sound)_sounds[key];
        }


    }
}
