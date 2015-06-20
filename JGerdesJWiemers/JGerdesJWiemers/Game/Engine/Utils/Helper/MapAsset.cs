using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils.Helper
{
    class MapAsset
    {
        public List<LayerAsset> Layers { get; set; }
        public int Tileheight { get; set; }
        public List<TilesetsAsset> TileSets { get; set; }
        public int Tilewidth { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
