﻿using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.TowerDefence.Screens
{
    class LoadingScreen : Screen
    {
        private enum Mode {GRAPHICS, AUDIO, LEVEL};
        private static int BAR_HEIGHT = 16;
        private static int DESCRIPTION_PADDING = 4;

        private List<String> _textures;
        private List<String> _sounds;
        private String[] _levels;
        private Mode _mode = Mode.GRAPHICS;
        private int _currentIndex = 0;

        private RectangleShape _bar;
        private Text _description;
        private float _pixels_per_asset;

        public LoadingScreen(RenderWindow w)
        :base(w){
            _clearColor = LevelSelector.AWSM_GREY;
            _textures = new List<string>(){
                AssetLoader.TEXTURE_SHADOW,
                AssetLoader.TEXTURE_TOWER_BASE,
                AssetLoader.TEXTURE_TOWER_TOP,
                AssetLoader.TEXTURE_BULLET,
                AssetLoader.TEXTURE_UI_TOWER_SELECTION_BUTTON,
                AssetLoader.TEXTURE_UI_TOWER_SELECTION_TOP,
                AssetLoader.TEXTURE_UI_ICON_ENEGRY,
                AssetLoader.TEXTURE_UI_ICON_MISSED,
                AssetLoader.TEXTURE_UI_ICON_MONEY,
                AssetLoader.TEXTURE_UI_ICON_WARN,
                AssetLoader.TEXTURE_SPLAH_AWSM
            };
            _sounds = new List<string>(){
                AssetLoader.AUDIO_MUSIC_1,
                AssetLoader.AUDIO_MUSIC_WIN,
                AssetLoader.AUDIO_MUSIC_LOSE,
                AssetLoader.AUDIO_SHOT_1,
                AssetLoader.AUDIO_SHOT_2,
                AssetLoader.AUDIO_SHOT_3,
                AssetLoader.AUDIO_SHOT_4,
                AssetLoader.AUDIO_SHOT_5,
                AssetLoader.AUDIO_BUILD,
                AssetLoader.AUDIO_BUILD_NOT,
                AssetLoader.AUDIO_MISSED,
                AssetLoader.AUDIO_START_LEVEL,
                AssetLoader.AUDIO_SELECT_LEVEL,
                AssetLoader.AUDIO_SELECT_TOWER
            };

            _levels = new String[0];
            _pixels_per_asset = _window.Size.X / (_textures.Count + _sounds.Count + _levels.Length);
            _bar = new RectangleShape(new Vector2f(0, BAR_HEIGHT));
            _bar.Position = new Vector2f(0, _window.Size.Y - BAR_HEIGHT);
            _bar.FillColor = LevelSelector.AWSM_ORANGE;
            _description = new Text("Loading graphics...", AssetLoader.Instance.getFont(AssetLoader.FONT_ROBOTO_THIN));
            _description.CharacterSize = 18;
            _description.Origin = new Vector2f(0, _description.GetLocalBounds().Height);
            _description.Position = _bar.Position + new Vector2f(DESCRIPTION_PADDING, -DESCRIPTION_PADDING);
        }
        public override void Exit()
        {
            
        }

        public override void Update()
        {
            int loaded = -1;

            if (_levels.Length == 0)
            {
                _levels = AssetLoader.Instance.GetLevelDirectories();
            }
            switch (_mode)
            {
                case Mode.GRAPHICS:
                    if (_currentIndex >= _textures.Count)
                    {
                        _currentIndex = 0;
                        _mode = Mode.AUDIO;
                        _description.DisplayedString = "Loading audio...";
                    }
                    else
                    {
                        loaded = _currentIndex;
                        AssetLoader.Instance.LoadTexture(_textures[_currentIndex], _textures[_currentIndex]);
                        _currentIndex++;
                    }
                break;
                case Mode.AUDIO:
                    if (_currentIndex >= _sounds.Count)
                    {
                        _currentIndex = 0;
                        _mode = Mode.LEVEL;
                        _description.DisplayedString = "Loading levels...";
                    }
                    else
                    {
                        loaded = _currentIndex + _textures.Count;
                        AssetLoader.Instance.LoadSound(_sounds[_currentIndex], _sounds[_currentIndex]);
                        _currentIndex++;
                    }
                break;
                case Mode.LEVEL:
                if (_currentIndex >= _levels.Length)
                {
                    _screenManager.Switch(new SplashScreen(_window));
                }
                else
                {
                    loaded = _currentIndex + _textures.Count + _sounds.Count;
                    _bar.Size = _bar.Size + new Vector2f(_pixels_per_asset, 0);
                    AssetLoader.Instance.LoadLevel(_levels[_currentIndex]);
                    _currentIndex++;
                }
                break;
            }

            if(loaded != -1)
                _bar.Size = new Vector2f(_pixels_per_asset * loaded, BAR_HEIGHT);
        }

        protected override void _Resize(Engine.EventSystem.Events.EngineEvent eventData)
        {
            base._Resize(eventData);
            Vector2f size = (Vector2f)eventData.Data;
            _pixels_per_asset = size.X / (_textures.Count + _sounds.Count + _levels.Length);
            _bar.Position = new Vector2f(0, size.Y - BAR_HEIGHT);
            _description.Position = _bar.Position + new Vector2f(DESCRIPTION_PADDING, -DESCRIPTION_PADDING);
        }

        public override void PastUpdate()
        {
            
        }

        public override void PreDraw(float extra)
        {
            
        }

        public override void Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            target.Draw(_bar);
            target.Draw(_description);
        }
    }
}
