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
            public Tower.Def TowerDef;

            public Option(Tower.Def def)
            {
                TextureContainer container = AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_TOWER_SELECTION); ;
                TowerDef = def;
                Sprite = new AnimatedSprite(container.Texture, container.Width, container.Height);
                Sprite.Color = def.Base;
                Sprite.Origin = new Vector2f(container.Width / 2f, container.Height / 2f);
                Sprite.SetAnimation(new Animation());

            }

        }

        public delegate void OnSelectioChanged(Tower.Def selection);
        public event OnSelectioChanged SelectionChanged;
        private List<Option> _options;
        private int _selected = -1;

        public TowerSelector(List<Tower.Def> defintions, Vector2f position)
        {
            _options = new List<Option>();
            foreach (Tower.Def def in defintions)
            {
                _options.Add(new Option(def));
            }
            Select(0);
            Position = position;
        }

        public Vector2f Position
        {
            set
            {
                int counter = 0;
                float width = _options[0].Sprite.TextureRect.Width;
                float offset = (_options.Count * width * 1.5f - width) / 2f;
                foreach (Option o in _options)
                {
                    float x = counter * width * 1.5f;
                    x -= offset;
                    o.Sprite.Position = value + new Vector2f(x, 0);
                    counter++;
                }
            }
        }

        public void Select(int option)
        {
            if (option >= 0 && option < _options.Count && option != _selected)
            {
                if (_selected != -1)
                {
                    AnimatedSprite oldS = _options[_selected].Sprite;
                    oldS.SetAnimation(new Animation(16, 31, 30, false, false));
                    oldS.EnqueueAnimation(new Animation());
                }
                
                AnimatedSprite newS = _options[option].Sprite;
                newS.SetAnimation(new Animation(0, 15, 30, false, false));
                newS.EnqueueAnimation(new Animation(new int[]{15}, 1000, true));

                _selected = option;

                if (SelectionChanged != null)
                {
                    SelectionChanged(_options[_selected].TowerDef);
                }
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
