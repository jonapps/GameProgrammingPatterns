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
using JGerdesJWiemers.Game.Engine.EventSystem;
using JGerdesJWiemers.Game.Engine.EventSystem.Events;
using JGerdesJWiemers.Game.Engine.Utils.Helper.LevelAssets;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class UiScreen : Screen
    {

        public static readonly int DRAWER_WIDTH = 192;

        private Builder _builder;
        private TowerSelector _selector;
        private Shape _drawer;
        private Color _drawerColor;

        private Label _energyLabel;
        private Label _missedLabel;
        private Label _missedLabelMax;
        private Label _wavesLabel;

        public UiScreen(RenderWindow w, Map map, ICoordsConverter converter, LevelAsset level)
            :base(w)
        {
            _builder = new Builder(map, converter);
            _drawerColor = level.Info.DrawerColor;
            _drawer = new RectangleShape(new Vector2f(DRAWER_WIDTH, _window.Size.Y));
            _drawer.FillColor = _drawerColor;
            _drawer.Position = new Vector2f(0, 0);
            _selector = new TowerSelector( level.Tower, new Vector2f(DRAWER_WIDTH, _window.Size.Y / 2f));

            _energyLabel = new Label("0", AssetLoader.FONT_ROBOTO_THIN, 24, AssetLoader.TEXTURE_UI_ICON_ENEGRY);
            _energyLabel.Position = new Vector2f(30, 50);

            _missedLabel = new Label("0", AssetLoader.FONT_ROBOTO_THIN, 24, AssetLoader.TEXTURE_UI_ICON_MISSED);
            _missedLabel.Position = new Vector2f(30, 90);

            _missedLabelMax = new Label("/ "+level.Info.Lives.ToString(), AssetLoader.FONT_ROBOTO_THIN, 24);
            _missedLabelMax.Position = new Vector2f(100, 90);

            _wavesLabel = new Label("No Wave", AssetLoader.FONT_ROBOTO_THIN, 24, AssetLoader.TEXTURE_UI_ICON_WARN);
            _wavesLabel.Position = new Vector2f(30, _window.Size.Y - 30);


            _window.KeyPressed += _window_KeyPressed;
            _window.MouseButtonPressed += _window_MouseButtonPressed;

            _selector.SelectionChanged += OnSelectionChanged;

            EventStream.Instance.On(ScoreManager.EVENT_ENERGY_CHANGED, onEnergyChanged);
            EventStream.Instance.On(ScoreManager.EVENT_MISSED_CHANGED, onMissedChanged);
            EventStream.Instance.On(WaveManager.EVENT_WAVE_STARTED, onWaveStarted);
        }

        private void onWaveStarted(EngineEvent eventData)
        {
            WaveManager.WaveData data = eventData.Data as WaveManager.WaveData;
            _wavesLabel.DisplayedString = (data.Current+1) + "/" + data.Total;
        }

        private void onMissedChanged(EngineEvent eventData)
        {
            _missedLabel.DisplayedString = "" + (int)(eventData.Data);
        }

        private void onEnergyChanged(EngineEvent eventData)
        {
            _energyLabel.DisplayedString = "" + (int)(eventData.Data);
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
            else if (e.Code == SFML.Window.Keyboard.Key.Q)
            {
                _selector.Select(-1);
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
            target.Draw(_drawer);

            target.Draw(_energyLabel);
            target.Draw(_missedLabel);
            target.Draw(_missedLabelMax);
            target.Draw(_wavesLabel);

            target.Draw(_selector, states);
        }

        protected override void _Resize(Engine.EventSystem.Events.EngineEvent eventData)
        {
            base._Resize(eventData);
            Vector2f size = (Vector2f)eventData.Data;

            _drawer = new RectangleShape(new Vector2f(DRAWER_WIDTH, _window.Size.Y));
            _drawer.FillColor = _drawerColor;
            _drawer.Position = new Vector2f(0, 0);

            _selector.Position = new Vector2f(DRAWER_WIDTH, size.Y / 2f);

            _wavesLabel.Position = new Vector2f(30, size.Y - 30);
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
