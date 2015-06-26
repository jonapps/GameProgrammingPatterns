using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils.Helper.LevelAssets
{
    class EnemyAsset
    {
        public String Name { get; set; }
        public String Type { get; set; }
        public String Floating { get; set; }
        public int Health { get; set; }
        public int Energy { get; set; }
        public int Speed { get; set; }
        public ShootAsset Shoot { get; set; }
        public ColorAsset Color { get; set; }
    
    
    
    }
}
