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

        void PreDraw(float extra);
    }
}
