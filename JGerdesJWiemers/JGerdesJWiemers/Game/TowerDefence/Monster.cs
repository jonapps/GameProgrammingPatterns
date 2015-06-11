﻿using JGerdesJWiemers.Game.Engine.Entities;
using JGerdesJWiemers.Game.Engine.Graphics;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence
{
    class Monster : SpriteEntity
    {
        public Monster(FarseerPhysics.Dynamics.World w)
            : base(w, new CircleTextureContainer(new Texture(@"Assets/Graphics/guy.png"), 39, 69, 16), 1)
        {
            _sprite.SetAnimation(new Animation(0, 7, 20, true, false));
            _sprite.Scale = new Vector2f(1, 1);
            //_body.Position = new Vector2(20, 20);
            _body.LinearVelocity = new Vector2(15, 15);
        }

        public override void Update()
        {
            
        }
    }
}