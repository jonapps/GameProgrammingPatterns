using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Interfaces
{
    interface ICoordsConverter
    {
        Vector2f MapPixelToCoords(Vector2i pixelPoint);
        Vector2i MapCoordsToPixel(Vector2f coordsPoint);
    }
}
