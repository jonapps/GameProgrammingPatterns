using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class SpaceShip : PolygonEntity
    {
        public SpaceShip(float x, float y , World w)
            : base(x, y, null, w)
        {
            Vertices sp = new Vertices();

            sp.Add(new Vector2(0, 0));
            sp.Add(new Vector2(-3, 5));
            sp.Add(new Vector2(-6, 10));

            sp.Add(new Vector2(0, 12));

            sp.Add(new Vector2(6, 10));
            sp.Add(new Vector2(3, 5));

            _Create(x, y, sp, w);

            _body.Friction = 1000;

            float force = 1000;


            var ld = new Vector2(0, 0);

            InputManager.Channel[1].OnUp += delegate(float val)
            {
                ld.X = 0;
                ld.Y = -1; 
                var wd = _body.GetWorldVector(ld);
                wd.Normalize();
                wd *= val * force*10;
                _body.ApplyForce(wd);
            };

            InputManager.Channel[1].OnDown += delegate(float val)
            {
                ld.X = 0;
                ld.Y = 1;
                var wd = _body.GetWorldVector(ld);
                wd.Normalize();
                wd *= val * force*10;
                _body.ApplyForce(wd); 
            };

            InputManager.Channel[1].OnLeft += delegate(float val)
            {

                _body.ApplyAngularImpulse(force * -1);
            };

            InputManager.Channel[1].OnRight += delegate(float val)
            {
                _body.ApplyAngularImpulse(force);
            };

        }

        public override void Update()
        {
            base.Update();
        }
    }
}
