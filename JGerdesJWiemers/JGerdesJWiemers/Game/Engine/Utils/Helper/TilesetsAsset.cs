using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils.Helper
{
    class TilesetsAsset
    {
        public List<TileImageAsset> TileImages { get; set; }


        public TilesetsAsset()
        {
            TileImages = new List<TileImageAsset>();
        }
    }
}
