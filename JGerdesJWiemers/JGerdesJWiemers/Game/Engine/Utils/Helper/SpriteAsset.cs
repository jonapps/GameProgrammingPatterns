using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils.Helper
{
    class SpriteAsset : Asset
    {
        public class Point{
            public int x { get; set; }
            public int y { get; set; }
        }

        public class ColliderData
        {
            public String Type { get; set; }
            public float Radius { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
        }

        public String ImageTitle { get; set; }
        public Point SpriteSheet { get; set; }
        public ColliderData Collider { get; set; }
        public Point Center { get; set; }

    }
}
