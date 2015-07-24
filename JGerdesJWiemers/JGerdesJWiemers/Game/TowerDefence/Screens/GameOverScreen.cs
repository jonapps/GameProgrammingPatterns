using JGerdesJWiemers.Game.Engine.Audio;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Utils;
using JGerdesJWiemers.Game.Engine.Utils.Helper.LevelAssets;
using JGerdesJWiemers.Game.TowerDefence.Logic;
using JGerdesJWiemers.Game.TowerDefence.UiElements;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class GameOverScreen : Screen
    {
        private static readonly float BLUR_MAX_RADIUS = 0.02f;
        private static readonly Color BLACK = new Color(20, 20, 20);
        private static readonly Color WHITE = new Color(220, 220, 220);

        public enum Status { WIN, LOSE };

        private float _blur = 0;
        private Shader _blurShader;
        private LevelAsset _levelAsset;

        private RectangleShape _popup;
        private Vector2f _popupDestination;

        private Text _title;
        private Text _subTitle;
        private Text _actionLeft;
        private Text _actionRight;

        private int _selectedAction;

        public GameOverScreen(RenderWindow w, Status status, Shader blurShader, LevelAsset levelAsset)
        :base(w){
            _clearColor = new Color(0, 0, 0, 200);
            _blurShader = blurShader;
            _levelAsset = levelAsset;

            String subTitleText = "";
            switch (status)
            {
                case Status.LOSE:
                    subTitleText = levelAsset.Info.Lives + " made it to their goal :(\nTry harder next time!";
                    AudioManager.Instance.PlayMusic(AssetLoader.AUDIO_MUSIC_LOSE);
                    break;
                case Status.WIN:
                    subTitleText = "You won!\nEnergy left: " + ScoreManager.Instance.Energy;
                    AudioManager.Instance.PlayMusic(AssetLoader.AUDIO_MUSIC_WIN);
                    break;
            }

            _popup = new RectangleShape(new Vector2f(640, 480));
            _popup.Origin = _popup.Size / 2f;
            _popup.FillColor = WHITE;
            _popup.Position = new Vector2f(w.Size.X / 2f, w.Size.Y + _popup.Size.Y);
            _popupDestination = new Vector2f(w.Size.X / 2f, w.Size.Y / 2f);

            Font roboto = AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_THIN);

            _title = new Text("Game Over!", roboto);
            _title.CharacterSize = 48;
            _title.Origin = new Vector2f(_title.GetGlobalBounds().Width / 2f, _title.GetGlobalBounds().Height / 2f);
            _title.Color = new Color(20, 20, 20);

            _subTitle = new Text(subTitleText, roboto);
            _subTitle.CharacterSize = 32;
            _subTitle.Origin = new Vector2f(_subTitle.GetGlobalBounds().Width / 2f, _subTitle.GetGlobalBounds().Height / 2f);
            _subTitle.Color = new Color(20, 20, 20);


            _actionLeft = new Text("PLAY AGAIN", roboto);
            _actionRight = new Text("SELECT LEVEL", roboto);
            _actionRight.Origin = new Vector2f(_actionRight.GetLocalBounds().Width, 0);

            _actionLeft.Color = BLACK;
            _actionRight.Color = BLACK;

            _window.KeyPressed += _window_KeyPressed;

            _SelectAction(0);
        }

        void _window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Return:
                    if (_selectedAction == 0)
                    {
                        _screenManager.Pop();
                        _screenManager.Switch(new Game(_window, _levelAsset));
                    }
                    else
                    {
                        _screenManager.Pop();
                        _screenManager.Switch(new LevelSelector(_window));
                    }
                    break;
                case Keyboard.Key.Up:
                case Keyboard.Key.Right:
                case Keyboard.Key.W:
                case Keyboard.Key.D:
                    _SelectAction(1);
                    break;
                case Keyboard.Key.Down:
                case Keyboard.Key.Left:
                case Keyboard.Key.S:
                case Keyboard.Key.A:
                    _SelectAction(0);
                    break;
            }
        }


        public override void Exit()
        {
            _window.KeyPressed -= _window_KeyPressed;
        }

        private void _SelectAction(int selection)
        {
            _selectedAction = selection;
            switch (selection)
            {
                case 0:
                    _actionLeft.Color = LevelSelector.AWSM_ORANGE;
                    _actionRight.Color = BLACK;
                    break;
                case 1:
                    _actionRight.Color = LevelSelector.AWSM_ORANGE;
                    _actionLeft.Color = BLACK;
                    break;
            }
            AudioManager.Instance.PlaySound(AssetLoader.AUDIO_SELECT_LEVEL);
        }

        public override void Update()
        {
            _blurShader.SetParameter("blur_radius", _blur);
            if(_blur < BLUR_MAX_RADIUS){
                _blur += (BLUR_MAX_RADIUS -_blur) / 20;
            }

            _popup.Position += (_popupDestination - _popup.Position) / 10f;
            _title.Position = _popup.Position + new Vector2f(0, -_popup.Origin.Y + 40);
            _subTitle.Position = _popup.Position + new Vector2f(0, -_popup.Origin.Y + 200);
            _actionLeft.Position = _popup.Position + new Vector2f(-_popup.Origin.X + 40, _popup.Origin.Y - 80);
            _actionRight.Position = _popup.Position + new Vector2f(_popup.Origin.X - 40, _popup.Origin.Y - 80);
        }

        public override void PastUpdate()
        {
            
        }

        public override void PreDraw(float extra)
        {
            
        }

        protected override void _Resize(Engine.EventSystem.Events.EngineEvent eventData)
        {
            base._Resize(eventData);
            Vector2f size = (Vector2f)eventData.Data;
            _popupDestination = new Vector2f(size.X / 2f, size.Y / 2f);
        }

        public override void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            target.Draw(_popup);
            target.Draw(_title);
            target.Draw(_subTitle);
            target.Draw(_actionLeft);
            target.Draw(_actionRight);
        }

        public override bool DoRenderBelow()
        {
            return true;
        }

        public override bool DoUpdateBelow()
        {
            return false;
        }
    }
}
