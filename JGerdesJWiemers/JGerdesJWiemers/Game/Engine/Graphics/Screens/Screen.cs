﻿using JGerdesJWiemers.Game.Engine.Audio;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Graphics.Screens
{
    abstract class Screen
    {
        protected RenderWindow _window;
        protected ScreenManager _screenManager;


        public Screen(RenderWindow window)
        {
            _window = window;
        }

        public ScreenManager Manager
        {
            set
            {
                _screenManager = value;
            }
        }

        /// <summary>
        /// Updates screen 
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// gets called after the regular update
        /// </summary>
        public abstract void PastUpdate();

        /// <summary>
        /// Renders content of screen on provided render target
        /// </summary>
        /// <param name="renderTarget">target to render content of screen to</param>
        /// <param name="extra">value between 0 and 1 for extrapolation</param>
        public abstract void Render(RenderTarget renderTarget, float extra);


        public abstract void Exit();
    }
}
