using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Utils;
using Microsoft.Xna.Framework;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.ShootEmUp.Entities
{
    class Moon : SpriteCircleEntity
    {
        private Animation _rotateAnimation;
        private Earth _earth;
        private static float MIN_DISTANCE = 10;
        private float _distance;

        public Moon(Earth earth, World world, float radius) :
            base(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_EARTH), 128, 128, earth.Body.Position.X+10, earth.Body.Position.Y+10, radius, world)
        {
            _earth = earth;            
            _rotateAnimation = new Animation(0, 319, 20, true, false);
            _sprite.SetAnimation(_rotateAnimation);
        }

        public override void Update()
        {
            Transform ta, tb;
            DistanceProxy dpa, dpb;
            dpa = new DistanceProxy();
            dpb = new DistanceProxy();


            _body.GetTransform(out ta);
            _earth.Body.GetTransform(out tb);

            dpa.Set(_fixture.Shape, 0);
            dpb.Set(_earth.Fixture.Shape, 0);

            DistanceOutput dout;
            SimplexCache ccache;

            Distance.ComputeDistance(out dout, out ccache, new DistanceInput()
            {
                ProxyA = dpa,
                ProxyB = dpb,
                TransformA = ta,
                TransformB = tb
            });

            Vector2 force, forceNormal;
            force = new Vector2(dout.PointA.X - dout.PointB.X, dout.PointA.Y - dout.PointB.Y);
            force.Normalize();
            forceNormal = new Vector2(force.Y * -1, force.X);
            if (dout.Distance > MIN_DISTANCE)
            {
                force = force * dout.Distance * -1 * 20;
                _body.ApplyForce(force);
            }
            else
            {
                force = force * dout.Distance * 20;
                _body.ApplyForce(force);
            }
            //_body.ApplyForce(forceNormal*10);
            //_body.ApplyLinearImpulse(forceNormal);
            _body.LinearVelocity = forceNormal * 10;
            System.Console.WriteLine(dout.Distance);

            base.Update();
        }

    }
}
