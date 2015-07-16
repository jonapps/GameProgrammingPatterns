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
using JGerdesJWiemers.Game.Engine.Utils;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class UiScreen : Screen
    {

        public static readonly int DRAWER_WIDTH = 192;

        private Builder _builder;
        private TowerSelector _selector;
        private Shape _drawer;
        private Color _drawerColor;

        private Label _energy;
        private Label _missed;

        public UiScreen(RenderWindow w, Map map, List<Tower.Def> towers, ICoordsConverter converter, Color drawerColor)
            :base(w)
        {

            _builder = new Builder(map, converter);
            _drawerColor = drawerColor;
            _drawer = new RectangleShape(new Vector2f(DRAWER_WIDTH, _window.Size.Y));
            _drawer.FillColor = _drawerColor;
            _drawer.Position = new Vector2f(0, 0);
            _selector = new TowerSelector(towers, new Vector2f(DRAWER_WIDTH - 64, _window.Size.Y / 2f));

            _energy = new Label("123", AssetLoader.FONT_ROBOTO_THIN, 24, AssetLoader.TEXTURE_UI_ICON_ENEGRY);
            _energy.Position = new Vector2f(30, 50);

            _missed = new Label("2", AssetLoader.FONT_ROBOTO_THIN, 24, AssetLoader.TEXTURE_UI_ICON_MISSED);
            _missed.Position = new Vector2f(30, 90);


            _window.KeyPressed += _window_KeyPressed;
            _window.MouseButtonPressed += _window_MouseButtonPressed;

            _selector.SelectionChanged += OnSelectionChanged;
            OnSelectionChanged(towers[0]);
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
            target.Draw(_drawer);

            target.Draw(_energy);
            target.Draw(_missed);

            target.Draw(_builder, states);
            target.Draw(_selector, states);
        }

        protected override void _Resize(Engine.EventSystem.Events.EngineEvent eventData)
        {
            base._Resize(eventData);
            Vector2f size = (Vector2f)eventData.Data;

            _drawer = new RectangleShape(new Vector2f(DRAWER_WIDTH, _window.Size.Y));
            _drawer.FillColor = _drawerColor;
            _drawer.Position = new Vector2f(0, 0);

            _selector.Position = new Vector2f(DRAWER_WIDTH - 64, size.Y / 2f);
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
