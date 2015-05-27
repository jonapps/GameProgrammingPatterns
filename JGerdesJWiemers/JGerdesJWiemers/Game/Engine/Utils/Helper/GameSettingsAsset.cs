using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Engine.Utils.Helper
{
    class GameSettingsAsset : Asset
    {
        public Keyboard.Key OnDown { get; set; }
        public Keyboard.Key OnUp { get; set; }
        public Keyboard.Key OnLeft { get; set; }
        public Keyboard.Key OnRight { get; set; }

    }
}
