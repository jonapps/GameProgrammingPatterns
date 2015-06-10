using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.TowerDefence.World;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class Game : Screen
    {
        private Map _map;
        public Game(RenderWindow w)
            :base(w)
        {
            _map = new Map(24, 16);
        }

        public override void Update()
        {
            
        }

        public override void PastUpdate()
        {
            
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            _map.Render(renderTarget, extra);
        }

        public override void Exit()
        {
            
        }
    }
}
