using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
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
        private Body _bc;
        private Body _ground;

        public TestScreen(Window w):base(w)
        {
            AABB worldBounds = new AABB();
            worldBounds.LowerBound.X = -200f;
            worldBounds.LowerBound.Y = -200f;
            worldBounds.UpperBound.X = 200f;
            worldBounds.UpperBound.Y = 200f;
            
            ConvertUnits.SetDisplayUnitToSimUnitRatio(64f);

            _world = new World(new Vector2(0, 9.81f ));

            _bc = new Body(_world, new Vector2(ConvertUnits.ToSimUnits(40), ConvertUnits.ToSimUnits(20)), 0);
            _world.BodyList.Add(_bc);

            _cs = new CircleShape(0.5f, 2f);
            _bc.BodyType = BodyType.Dynamic;
            _bc.OnCollision += delegate(Fixture f1, Fixture f2, Contact contact)
            {
                return true;
            };
            _bc.CreateFixture(_cs);

            _ground = new Body(_world);
            PolygonShape box = new PolygonShape(PolygonTools.CreateRectangle(10f, 0.5f), 1f);

            _ground.BodyType = BodyType.Static;
            _ground.Position =  new Vector2(ConvertUnits.ToSimUnits(40), ConvertUnits.ToSimUnits(600));
            _ground.Rotation = 0.1f;
            _ground.CreateFixture(box);

            _world.BodyList.Add(_ground);

            _ground.OnCollision += delegate(Fixture f1, Fixture f2, Contact contact)
            {
                contact.Restitution = 0.5f;
                return true;
            };
            _world.BodyList.Add(_ground);
        }

        public override void Update()
        {
            _world.Step(0.03f);
        }

        public override void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            SFML.Graphics.CircleShape circle = new SFML.Graphics.CircleShape();
            circle.Radius = ConvertUnits.ToDisplayUnits(_cs.Radius);
            circle.Position = new Vector2f(ConvertUnits.ToDisplayUnits(_bc.Position.X), ConvertUnits.ToDisplayUnits(_bc.Position.Y));
            circle.Rotation = _bc.Rotation;
            renderTarget.Draw(circle);

            SFML.Graphics.RectangleShape rect = new SFML.Graphics.RectangleShape();
            rect.Size = new Vector2f(ConvertUnits.ToDisplayUnits(10f), ConvertUnits.ToDisplayUnits(0.5f));
            rect.Rotation = ConvertUnits.ToDisplayUnits(_ground.Rotation);
            rect.Position = new Vector2f(ConvertUnits.ToDisplayUnits(_ground.Position.X), ConvertUnits.ToDisplayUnits(_ground.Position.Y));
            renderTarget.Draw(rect);
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }


        
    }
}
