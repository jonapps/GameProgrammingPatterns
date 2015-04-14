using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine
{
    abstract class Entity : IUpdateable, IRenderable
    {
        public abstract void Update();

        public abstract void Render(SFML.Graphics.RenderTarget renderTarget, float extra);
    }
}
