using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.TowerDefence.Entities;
using JGerdesJWiemers.Game.TowerDefence.Logic;
using SFML.Graphics;
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
            public AnimatedSprite Button;
            public AnimatedSprite Top;
            public Tower.Def TowerDef;
            public Label PriceTag;

            public Option(Tower.Def def)
            {
                TextureContainer container = AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_TOWER_SELECTION_BUTTON);
                TowerDef = def;
                Button = new AnimatedSprite(container.Texture, container.Width, container.Height);
                Button.Color = def.Base;
                Button.SetAnimation(new Animation());

                container = AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_UI_TOWER_SELECTION_TOP);
                Top = new AnimatedSprite(container.Texture, container.Width, container.Height);
                Top.Color = def.TopActive;
                Top.SetAnimation(new Animation());

                PriceTag = new Label("" + def.Price, AssetLoader.FONT_ROBOTO_THIN, 24, AssetLoader.TEXTURE_UI_ICON_MONEY);

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

            EventStream.Instance.On(ScoreManager.EVENT_ENERGY_CHANGED, onEnergyChanged);
        }

        private void onEnergyChanged(Engine.EventSystem.Events.EngineEvent eventData)
        {
            int enegry = (int)eventData.Data;
            foreach(Option o in _options){
                Color top, button;
                if (o.TowerDef.Price > enegry)
                {
                    o.Button.Color = new Color((byte)(o.TowerDef.Base.R / 2),
                                                (byte)(o.TowerDef.Base.G / 2),
                                                (byte)(o.TowerDef.Base.B / 2),
                                                (byte)(o.TowerDef.Base.A / 2));
                    o.Top.Color = new Color((byte)(o.TowerDef.TopActive.R / 2),
                                            (byte)(o.TowerDef.TopActive.G / 2),
                                            (byte)(o.TowerDef.TopActive.B / 2),
                                            (byte)(o.TowerDef.TopActive.A / 2));
                }
                else
                {
                    o.Button.Color = o.TowerDef.Base;
                    o.Top.Color = o.TowerDef.TopActive;
                }
            }

        }

        public Vector2f Position
        {
            set
            {
                int counter = 0;
                float height = _options[0].Button.TextureRect.Height;
                float offset = (_options.Count * height - height) / 2f;
                foreach (Option o in _options)
                {
                    float y = counter * height;
                    y -= offset;
                    o.Button.Position = value + new Vector2f(-128, y);
                    o.Top.Position = value + new Vector2f(-128, y);
                    o.PriceTag.Position = value + new Vector2f(-160, y + 72);
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
                    AnimatedSprite oldS = _options[_selected].Button;
                    oldS.SetAnimation(new Animation(new int[]{14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1} ,10, false));
                    oldS.EnqueueAnimation(new Animation());
                    oldS = _options[_selected].Top;
                    oldS.SetAnimation(new Animation(new int[] { 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, 10, false));
                    oldS.EnqueueAnimation(new Animation());
                }
                
                AnimatedSprite newS = _options[option].Button;
                newS.SetAnimation(new Animation(0, 15, 10, false, false));
                newS.EnqueueAnimation(new Animation(new int[]{15}, 1000, true));
                newS = _options[option].Top;
                newS.SetAnimation(new Animation(0, 15, 10, false, false));
                newS.EnqueueAnimation(new Animation(new int[] { 15 }, 1000, true));

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
                option.Button.Update();
                option.Top.Update();
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
                target.Draw(option.PriceTag, states);
                target.Draw(option.Button, states);
                target.Draw(option.Top, states);
            }
        }
    }
}
