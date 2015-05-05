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


namespace JGerdesJWiemers.Game
{
    class TestScreen : Screen
    {

        private World _world;
        private FShapes.CircleShape _cs;
        private Body _bc;
        private Body _ground;

        public TestScreen(Window w):base(w)
        {

            

            _world = new World(new Vector2(0, 9.81f ));

            _bc = new Body(_world, new Vector2(4.0f, 2.0f), 0);
            _world.BodyList.Add(_bc);

            _cs = new FShapes.CircleShape(3f, 0.2f);
            _bc.BodyType = BodyType.Dynamic;
            _bc.OnCollision += delegate(Fixture f1, Fixture f2, Contact contact)
            {
                return true;
            };
            _bc.CreateFixture(_cs);

            _ground = new Body(_world);
            PolygonShape box = new PolygonShape(PolygonTools.CreateRectangle(32f, 1.6f), 1f);

            _ground.BodyType = BodyType.Static;
            _ground.Position =  new Vector2(5.0f, 30f);
            _ground.Rotation = 0.1f;
            _ground.CreateFixture(box);

            _world.BodyList.Add(_ground);

            _ground.OnCollision += delegate(Fixture f1, Fixture f2, Contact contact)
            {
                contact.Restitution = 1f;
                //contact.Friction = -0.2f;
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
            circle.Radius = _cs.Radius;
            circle.Origin = new Vector2f(3.2f, 3.2f);
            circle.Position = new Vector2f(_bc.Position.X, _bc.Position.Y);
            circle.Rotation = _bc.Rotation;
            renderTarget.Draw(circle);

            SFML.Graphics.RectangleShape rect = new SFML.Graphics.RectangleShape();
            rect.Size = new Vector2f(64.0f, 3.2f);
            rect.Origin = new Vector2f(32.0f, 1.6f);
            rect.Rotation = _ground.Rotation * 180/ (float)Math.PI;
            rect.Position = new Vector2f(_ground.Position.X, _ground.Position.Y);
            renderTarget.Draw(rect);
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }


        
    }
}
