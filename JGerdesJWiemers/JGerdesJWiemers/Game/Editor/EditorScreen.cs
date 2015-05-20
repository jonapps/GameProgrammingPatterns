using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Graphics.Screens;
using JGerdesJWiemers.Game.Engine.Shapes;
using JGerdesJWiemers.Game.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SMath = System.Math;

namespace JGerdesJWiemers.Game.Editor
{
    class EditorScreen : Screen
    {

        private AnimatedSprite _sprite;
        private PolygonShape _shape;
        private RenderStates _renderStates;
        private View _view;
        private Vector2f _scale;
        private bool _doUpdate;
        private int _movingPointIndex = -1;
        private int _currentTexture = 0;

        private EditorWindow _ew;

        public EditorScreen(RenderWindow w): base(w)
        {
            _ew= new EditorWindow(this);
            _ew.Show();
            _shape = new PolygonShape(new List<Vector2f>());
            _shape.FillColor = new Color(0, 0, 0, 0);
            _shape.OutlineColor = new Color(255, 255, 255, 255);
            _shape.OutlineThickness = 1f;

            TextureContainer tex = AssetLoader.Instance.getTexture(AssetLoader.TEXTURE_ASTEROID1);
            _sprite = new AnimatedSprite(tex.Texture, tex.Width, tex.Height);
            _sprite.Position = new Vector2f(0, 0);
            _sprite.SetAnimation(new Animation());

            _renderStates = new RenderStates(BlendMode.Alpha);
            _view = new View(new Vector2f(w.Size.X / 2f, w.Size.Y / 2f), new Vector2f(w.Size.X, w.Size.Y));
            _scale = new Vector2f(1, 1);
            w.SetView(_view);
            _window.MouseButtonReleased += _window_MouseButtonReleased;
            _window.MouseWheelMoved += _window_MouseWheelMoved;
            _window.MouseButtonPressed += _window_MouseButtonPressed;
            _window.MouseMoved += _window_MouseMoved;
        }

        public void LoadSprite(String path, int width, int height)
        {
            TextureContainer tex = AssetLoader.Instance.LoadTexture(path, path);
            _sprite = new AnimatedSprite(tex.Texture, tex.Width, tex.Height);
            _sprite.SetAnimation(new Animation());
            ResetShape();

        }

        void _window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            Vector2f mousePos = new Vector2f(e.X * _scale.X, e.Y * _scale.Y);
            if(_movingPointIndex != -1){
                _shape.setPoint(_movingPointIndex, mousePos);
                saveShape();
            }
            
        }


        void _window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(e.Button);
            if (e.Button == Mouse.Button.Middle)
            {
                Vector2f mousePos = new Vector2f(e.X * _scale.X, e.Y * _scale.Y);
                _movingPointIndex = findNearestPoint(mousePos);
            }
        }

        void _window_MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            _scale += new Vector2f(0.1f, 0.1f) * SMath.Sign(-1*e.Delta);
            if (_scale.X < 0)
            {
                _scale = new Vector2f(0, 0);
            }
            _view.Size = new Vector2f(1280 * _scale.X, 720 * _scale.Y);
            _view.Center = new Vector2f(640 * _scale.X, 360 * _scale.Y);
            _doUpdate = true;
        }

        void _window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                _shape.addPoint(new Vector2f(e.X * _scale.X, e.Y * _scale.Y));
            }
            else if (e.Button == Mouse.Button.Middle)
            {
                _movingPointIndex = -1;
            }
            else if (e.Button == Mouse.Button.Right)
            {
                
                Vector2f mousePos = new Vector2f(e.X * _scale.X, e.Y * _scale.Y);
                int index = findNearestPoint(mousePos);
                if (index != -1)
                {
                    _shape.DeletePoint(index);
                }
                   
            }
            saveShape();
            
        }

        private int findNearestPoint(Vector2f mousePos)
        {
            float nearestDistance = -1;
            int nearestIndex = -1;
            for (int i = 0; i < _shape.GetPointCount(); i++)
             {
                float dist = mousePos.DistanceTo(_shape.GetPoint((uint)i));
                if (nearestDistance == -1 || dist < nearestDistance)
                {
                    nearestDistance = dist;
                    nearestIndex = i;
                }
            }
            return nearestIndex;
        }

        public override void Render(RenderTarget renderTarget, float extra)
        {
            if (_doUpdate)
            {
                renderTarget.SetView(_view);
                _doUpdate = false;
            }
            _sprite.Draw(renderTarget, _renderStates);
            _shape.Draw(renderTarget, _renderStates);
           
        }

        public void saveShape()
        {
            String data = "List<Vector2f> points = new List<Vector2f>();\r\n";
            for (int i = 0; i < _shape.GetPointCount(); i++)
            {
                Vector2f point = _shape.GetPoint((uint)i);
                data += "points.Add(new Vector2f("+point.X+", "+point.Y+"));\r\n";
            }
            _ew.SetResult(data);
        }

        public void ResetShape()
        {
            _shape = new PolygonShape(new List<Vector2f>());
            _shape.FillColor = new Color(0, 0, 0, 0);
            _shape.OutlineColor = new Color(255, 255, 255, 255);
            _shape.OutlineThickness = 1f;
        }

        public void ToggleAnimation(bool animate)
        {
            if (animate)
            {
                _sprite.SetAnimation(new Animation(0, _sprite.GetFrameCount()-1, 20, true, false));
            }
            else
            {
                _sprite.SetAnimation(new Animation());
            }
        }

        public override void Update()
        {
           
        }

        public override void Exit()
        {
            
        }
    }
}




