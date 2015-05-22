using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
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
    class Moon : SpriteEntity
    {
        private Animation _rotateAnimation;
        private Earth _earth;
        private float _moonImpulse = 10000;
        private bool _forced = false;
        private float _minDistance = 20;

        public Moon(World world, Earth earth, float radius) :
            base(world, AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_MOON), radius / ConvertUnits.ToSimUnits(32), earth.Body.Position.X + 10, earth.Body.Position.Y + 10)
        {
            _earth = earth;
            _body.Mass = 10f;
            
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
            if (_forced)
            {
                _forced = false;
                _ApplySpeed(_moonImpulse);
                
            }
            _ForceOrbit();

            base.Update();
        }

        private void _ApplySpeed(float speed)
        {
            Vector2 force, forceNormal;
            DistanceOutput dout = _CalculateDistnaceToEarth(out force, out forceNormal);
            force = new Vector2(dout.PointA.X - dout.PointB.X, dout.PointA.Y - dout.PointB.Y);
            force.Normalize();
            forceNormal = new Vector2(force.Y * -1, force.X);
            _body.ApplyForce(forceNormal * speed);
        }

        private void _ApplyLinearVelocity(float speed)
        {
            Vector2 force, forceNormal;
            DistanceOutput dout = _CalculateDistnaceToEarth(out force, out forceNormal);
            force = new Vector2(dout.PointA.X - dout.PointB.X, dout.PointA.Y - dout.PointB.Y);
            force.Normalize();
            forceNormal = new Vector2(force.Y * -1, force.X);
            _body.LinearVelocity = forceNormal * speed;
        }

        private void _ForceOrbit()
        {
            float distanceLength, distanceFromOrbit;
            Vector2 distance, normal;
            DistanceOutput dout = _CalculateDistnaceToEarth(out distance, out normal);
            distanceLength = (distance.X * distance.X + distance.Y * distance.Y);
            distanceFromOrbit = distanceLength - (_minDistance * _minDistance);
            distance = distance / distanceLength;

            
        }

        private DistanceOutput _CalculateDistnaceToEarth(out Vector2 distance, out Vector2 normal)
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
            distance = new Vector2(dout.PointA.X - dout.PointB.X, dout.PointA.Y - dout.PointB.Y);
            normal = new Vector2(distance.Y * -1, distance.X);
            return dout;
        }

        public class MoonDef : EntityDef
        {
            public float Radius { get; set; }
            public Earth Earth { get; set; }
            public MoonDef(Earth örz)
                : base()
            {
                Radius = 5;
                Earth = örz;

            }

            public override Engine.Entity Spawn(World world)
            {
                Moon moon = new Moon(world, Earth, Radius);
                return moon;
            }
        }

    }
}
