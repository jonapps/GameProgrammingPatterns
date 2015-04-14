using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine
{
    interface IRenderable
    {
        /// <summary>
        /// Renders on provided render target
        /// </summary>
        /// <param name="renderTarget">target to render to</param>
        /// <param name="extra">value between 0 and 1 for extrapolation</param>
        void Render(RenderTarget renderTarget, float extra);
    }
}
