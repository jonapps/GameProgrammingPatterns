using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils.Helper.LevelAssets
{
    class LevelAsset
    {
        public String Name { get; set; }
        public MapAsset Map { get; set; }
        public EnemiesAsset Enemies { get; set; }
        public WavesAsset Waves { get; set; }
    }
}
