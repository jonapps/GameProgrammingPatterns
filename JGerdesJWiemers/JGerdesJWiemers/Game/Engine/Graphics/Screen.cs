﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Graphics
{
    abstract class Screen
    {
        protected Window _window;
        protected ScreenManager _screenManager;

        public Screen(Window window)
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
        /// Renders content of screen on provided render target
        /// </summary>
        /// <param name="renderTarget">target to render content of screen to</param>
        /// <param name="extra">value between 0 and 1 for extrapolation</param>
        public abstract void Render(RenderTarget renderTarget, float extra);
    }
}