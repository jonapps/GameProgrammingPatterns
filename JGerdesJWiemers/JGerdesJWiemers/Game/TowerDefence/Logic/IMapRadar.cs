using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.TowerDefence.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Logic
{
    interface IMapRadar
    {
        Tile GetTileAtPosition(float x, float y);
        List<Entity> FindEntitiesAround(Vector2 position, float radius);
        List<Entity> FindEntitiesAround(float x, float y, float radius);
        Entity FindEntityAround(Vector2 position, float radius);
    }
}
