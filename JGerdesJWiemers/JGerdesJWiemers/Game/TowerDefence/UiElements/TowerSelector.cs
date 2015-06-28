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
            int counter = 0;
            foreach (Tower.Def def in defintions)
            {
                Option option = new Option(def);
                option.Sprite.Position = position + new Vector2f(counter * 96, 0);
                _options.Add(option);
                counter++;
            }
            Select(0);
        }

        public Vector2f Position
        {
            set
            {
                int counter = 0;
                foreach (Option o in _options)
                {
                    o.Sprite.Position = value + new Vector2f(counter * 96, 0);
                    Console.WriteLine(o.Sprite.Position);
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
