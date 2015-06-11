using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Interfaces
{
    interface IDrawable : Drawable
    {
        void Update();

        void PastUpdate();

        /// <summary>
        /// Renders on provided render target
        /// </summary>
        /// <param name="renderTarget">target to render to</param>
        /// <param name="extra">value between 0 and 1 for extrapolation</param>
        void PreRender(float extra);


        void Render();
    }
}
