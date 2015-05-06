using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Graphics
{
    class Animation
    {
        public int[] Frames;
        public bool Loop;
        public int Duration;

        public Animation()
        {
            Frames = new int[] { 0 };
            Loop = false;
            Duration = 1;
        }

        public Animation(int from, int to, int duration, bool loop, bool pingpong)
        {
            int length = to - from + 1;
            Frames = new int[length + (pingpong ? length - 2 : 0)];

            int current = from;
            for (int i = 0; i < length; i++)
            {
                Frames[i] = current;
                current++;
            }

            if (pingpong)
            {
                current -= 2;
                for (int i = 0; i < length - 2; i++)
                {
                    Frames[length + i] = current;
                    current--;
                }
            }

            Loop = loop;
            Duration = duration;
        }

        public Animation(int[] frames, int duration, bool loop)
        {
            Frames = frames;
            Duration = duration;
            Loop = loop;
        }
    }
}
