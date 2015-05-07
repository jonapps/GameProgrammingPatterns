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

            InputManager.Channel[0].OnUp += delegate(float val)
            {
                _body.LinearVelocity = new Vector2(_body.LinearVelocity.X, val*-10);
            };

            InputManager.Channel[0].OnDown += delegate(float val)
            {
                _body.LinearVelocity = new Vector2(_body.LinearVelocity.X, val*10);
            };

            InputManager.Channel[0].OnLeft += delegate(float val)
            {
                _body.Rotation += val/100f;
            };

            InputManager.Channel[0].OnRight += delegate(float val)
            {
                _body.Rotation += val / -100f;
            };

        }

        public override void Update()
        {
            base.Update();
        }
    }
}
