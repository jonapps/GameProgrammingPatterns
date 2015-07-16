using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Input;
using JGerdesJWiemers.Game.Engine.Interfaces;
using JGerdesJWiemers.Game.TowerDefence.Entities;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGerdesJWiemers.Game.TowerDefence.Tiles;
using SFML.System;
using FarseerPhysics;
using JGerdesJWiemers.Game.Engine.EventSystem;

namespace JGerdesJWiemers.Game.TowerDefence.Logic
{
    class Builder : IDrawable
    {
        private Tower.Def _currentTower;
        private Sprite _towerBase;
        private AnimatedSprite _towerTop;
        private Map _map;
        private ICoordsConverter _converter;

        private CircleShape _radiusCircle;

        private Color _notBuildable;
        private bool _canBuild;

        public Tower.Def Selection
        {
            get
            {
                return _currentTower;
            }
            set{
                _currentTower = value;
                if (_currentTower != null)
                {
                    _currentTower.Base.A = 190;
                    _currentTower.TopActive.A = 190;
                    _towerBase.Color = _currentTower.Base;
                    _towerTop.Color = _currentTower.TopActive;

                    float convertedRadius = ConvertUnits.ToDisplayUnits(_currentTower.Radius);
                    Color radiusColor = _currentTower.TopActive;
                    radiusColor.A = 100;
                    _radiusCircle.Radius = convertedRadius;
                    _radiusCircle.FillColor = radiusColor;
                    _radiusCircle.OutlineColor = new Color(radiusColor.R, radiusColor.G, radiusColor.B, 180);
                    _radiusCircle.OutlineThickness = 2;
                }
               
            }
        }

        public Builder(Map map, ICoordsConverter converter)
        {
            _map = map;
            _converter = converter;
            _radiusCircle = new CircleShape(1, 36);
            _radiusCircle.Scale = new Vector2f(1, 0.5f);
           
            TextureContainer baseContainer = AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_TOWER_BASE);
            TextureContainer topContainer = AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_TOWER_TOP);

            _towerBase = new Sprite(baseContainer.Texture);
            _towerBase.Origin = new Vector2f(baseContainer.Width / 2f, baseContainer.Height / 2f);
            _towerTop = new AnimatedSprite(topContainer.Texture, topContainer.Width, topContainer.Height);
            _towerTop.SetAnimation(new Animation(0, 31, 20, true, false));
            _towerTop.Origin = new Vector2f(topContainer.Width / 2f, topContainer.Height / 2f);

            _notBuildable = new Color(255, 0, 0, 100);


        }


        public void Build()
        {
            if (_currentTower != null && _canBuild && ScoreManager.Instance.Energy >= _currentTower.Price)
            {
                _currentTower.Base.A = 255;
                _currentTower.TopActive.A = 255;
                EventStream.Instance.Emit(Tower.EVENT_BUILD, new Engine.EventSystem.Events.EngineEvent(_currentTower));
            }
                
        }



        public void Update()
        {
            
        }

        public void PastUpdate()
        {
            
        }

        public void PreDraw(float extra)
        {
            _towerTop.Update();
            if (_currentTower != null)
            {
                Tile tile = _map.GetTileAtScreenPoint(_converter.MapPixelToCoords(InputManager.Instance.MousePosition));
                Vector2f pos = _converter.MapPixelToCoords(InputManager.Instance.MousePosition);
                if (tile != null)
                {
                    _currentTower.Position = tile.getCenter();

                    Vector2f tileCenter = tile.getCenter().ToVector2f();
                    Vector2f positionOnView = Map.MapToScreen(tileCenter);
                    Vector2i positionOnScreen = _converter.MapCoordsToPixel(positionOnView);

                    _towerBase.Position = positionOnScreen.ToVector2f();
                    _towerTop.Position = _towerBase.Position;

                    _radiusCircle.Position = _towerBase.Position - new Vector2f(_radiusCircle.Radius, _radiusCircle.Radius / 2f);

                    if (!tile.IsOccupied && tile.GetType() == TileType.BuildTile && !(_currentTower.Price > ScoreManager.Instance.Energy))
                    {
                        _canBuild = true;
                        _towerBase.Color = _currentTower.Base;
                        _towerTop.Color = _currentTower.TopActive;
                    }
                    else
                    {
                        _canBuild = false;
                        _towerBase.Color = _notBuildable;
                        _towerTop.Color = _notBuildable;
                    }
                }
   
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_currentTower != null)
            {
                target.Draw(_radiusCircle, states);
                target.Draw(_towerBase, states);
                target.Draw(_towerTop, states);
            }
            
        }
    }
}
