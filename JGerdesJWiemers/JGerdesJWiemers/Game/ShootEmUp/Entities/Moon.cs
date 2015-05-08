using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Utils;
using Microsoft.Xna.Framework;
using SFML.System;
using SFML.Window;
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
        private float _moonImpulse = 10000;
        private bool _forced = false;



        public Moon(Earth earth, World world, float radius) :
            base(AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_MOON), 64, 64, earth.Body.Position.X+10, earth.Body.Position.Y+10, radius, world)
        {
            _earth = earth;
            
            InputManager.Channel[0].OnAction1 += delegate(bool press)
            {
                if (press)
                {
                    _forced = true;
                }
            };


            _ApplyLinearVelocity(18f);
        }

        public override void Update()
        {
            //_ApplyLinearForce(10);
            if (_forced)
            {
                _forced = false;
                _ApplySpeed(_moonImpulse);
            }
            base.Update();
        }

        private void _ApplySpeed(float speed)
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
            _body.ApplyForce(forceNormal * speed);
        }

        private void _ApplyLinearVelocity(float speed)
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
            _body.LinearVelocity = forceNormal * speed;
        }

    }
}
