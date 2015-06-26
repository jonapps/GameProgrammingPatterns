using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.TowerDefence.Entities;
using JGerdesJWiemers.Game.TowerDefence.Logic;
using JGerdesJWiemers.Game.Engine.Interfaces;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.TowerDefence.UiElements;
using SFML.System;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class UiScreen : Screen
    {

        private Builder _builder;
        private TowerSelector _selector;

        public UiScreen(RenderWindow w, Map map, ICoordsConverter converter)
            :base(w)
        {

            List<Tower.Def> defs = new List<Tower.Def>();
            defs.Add(new Tower.Def());
            defs.Add(new Tower.Def
            {
                Base = new Color(100, 100, 100),
                TopActive = new Color(240, 30, 220),
                TopWaiting = new Color(180, 10, 160),
                Radius = 2,
                FireFrequency = 100
            });
            defs.Add(new Tower.Def
            {
                Base = new Color(0, 150, 160),
                Radius = 5,
                FireFrequency = 1000
            });


            _builder = new Builder(map, converter);
            _selector = new TowerSelector(defs, new Vector2f(400, 680));
            _window.KeyPressed += _window_KeyPressed;
            _window.MouseButtonPressed += _window_MouseButtonPressed;

            _selector.SelectionChanged += OnSelectionChanged;
            OnSelectionChanged(defs[0]);
        }

        void OnSelectionChanged(Tower.Def selection)
        {
            _builder.Selection = selection;
        }

        void _window_MouseButtonPressed(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            if (e.Button == SFML.Window.Mouse.Button.Left)
            {
                _builder.Build();
            }
        }

        void _window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if ((int)e.Code > 26 && (int)e.Code < 35)
            {
                _selector.Select((int) e.Code - 27);
            }
                

        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            _builder.Update();
            _selector.Update();
        }

        public override void PastUpdate()
        {
            _builder.PastUpdate();
        }

        public override void PreDraw(float extra)
        {
            _builder.PreDraw(extra);
        }

        public override void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            target.Draw(_builder, states);
            target.Draw(_selector, states);
        }

        public override bool DoRenderBelow()
        {
            return true;
        }

        public override bool DoUpdateBelow()
        {
            return true;
        }
    }
}
