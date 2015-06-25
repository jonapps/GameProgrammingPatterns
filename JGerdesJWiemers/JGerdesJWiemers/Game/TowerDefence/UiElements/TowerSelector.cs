using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.TowerDefence.Entities;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.UiElements
{
    class TowerSelector : IDrawable
    {
        private class Option
        {
            public AnimatedSprite Sprite;
            private Tower.Def _towerDef;

            public Option(Tower.Def def)
            {
                TextureContainer container = AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_TOWER_SELECTION); ;
                _towerDef = def;
                Sprite = new AnimatedSprite(container.Texture, container.Width, container.Height);
                Sprite.Color = def.Base;
                Sprite.Origin = new Vector2f(container.Width / 2f, container.Height / 2f);
                Sprite.SetAnimation(new Animation());

            }

        }

        private List<Option> _options;
        private int _selected = 0;

        public TowerSelector(List<Tower.Def> defintions)
        {
            _options = new List<Option>();

            foreach (Tower.Def def in defintions)
            {
                Option option = new Option(def);
            }
        }

        public void Select(int option)
        {
            if (option > 0 && option < _options.Count)
            {
                _options[_selected].Sprite.SetAnimation(new Animation(16, 31, 30, false, false));
                _options[_selected].Sprite.EnqueueAnimation(new Animation());

                _options[option].Sprite.SetAnimation(new Animation(0, 15, 30, false, false));
                _options[option].Sprite.EnqueueAnimation(new Animation(new int[]{15}, 1000, true));
                _selected = option;
            }
        }

        public void Update()
        {
            foreach (Option option in _options)
            {
                option.Sprite.Update();
            }
        }

        public void PastUpdate()
        {
            
        }

        public void PreDraw(float extra)
        {
            
        }

        public void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            foreach (Option option in _options)
            {
                target.Draw(option.Sprite, states);
            }
        }
    }
}
