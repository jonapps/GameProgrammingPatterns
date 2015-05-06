using FarseerPhysics;
using FarseerPhysics.Collision;
using FShapes = FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using JGerdesJWiemers.Game.Engine.Graphics;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.Engine.Utils;


namespace JGerdesJWiemers.Game
{
    class TestScreen : Screen
    {

        private AnimatedSprite _earth;
        public TestScreen(RenderWindow w):base(w)
        {
            AssetLoader.Instance.LoadTexture("earth", "earth.png");
            _earth = new AnimatedSprite(AssetLoader.Instance.getTexture("earth"), 256, 256);
        }

        public override void Update()
        {
            
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            //renderTarget.Clear(new Color(255, 255, 255));
            _earth.Draw(renderTarget, new RenderStates(BlendMode.Alpha));

            
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }


        
    }
}
