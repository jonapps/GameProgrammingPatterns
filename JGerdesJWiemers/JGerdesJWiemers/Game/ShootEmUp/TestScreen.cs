using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Graphics;
using Microsoft.Xna.Framework;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JGerdesJWiemers.Game
{
    class TestScreen : Screen
    {

        private World _world;
        private CircleShape _cs;
        private Body _b;

        public TestScreen(Window w):base(w)
        {
            AABB worldBounds = new AABB();
            worldBounds.LowerBound.X = -200f;
            worldBounds.LowerBound.Y = -100f;
            worldBounds.UpperBound.X = 200f;
            worldBounds.UpperBound.Y = 200f;
            
            ConvertUnits.SetDisplayUnitToSimUnitRatio(64f);

            _world = new World(new Vector2(0, 9.81f ), worldBounds);

            _b = new Body(_world, new Vector2(ConvertUnits.ToSimUnits(20), ConvertUnits.ToSimUnits(20)), 0);
            _world.BodyList.Add(_b);

            _cs = new CircleShape(0.5f, 2f);
            _b.CreateFixture(_cs);
            _b.BodyType = BodyType.Dynamic;

            


        }

        public override void Update()
        {
            _world.Step(0.03f);
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            SFML.Graphics.CircleShape circle = new SFML.Graphics.CircleShape();
            circle.Radius = ConvertUnits.ToDisplayUnits(_cs.Radius);
            circle.Position = new Vector2f(ConvertUnits.ToDisplayUnits(_b.Position.X), ConvertUnits.ToDisplayUnits(_b.Position.Y));
            circle.Rotation = _b.Rotation;
            renderTarget.Draw(circle);
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }


        
    }
}
