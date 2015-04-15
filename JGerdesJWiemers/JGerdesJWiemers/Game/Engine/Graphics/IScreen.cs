using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Graphics
{
    interface IScreen
    {
        /// <summary>
        /// Updates screen 
        /// </summary>
        void Update();

        /// <summary>
        /// Renders content of screen on provided render target
        /// </summary>
        /// <param name="renderTarget">target to render content of screen to</param>
        /// <param name="extra">value between 0 and 1 for extrapolation</param>
        void Render(RenderTarget renderTarget, float extra);
    }
}
