using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils.Helper
{
    class SpriteAsset : Asset
    {

        public String ImageTitle { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public String Type { get; set; }
        public SpriteVectorAsset[] Vertices { get; set; }
        public float Radius { get; set; }

    }
}
