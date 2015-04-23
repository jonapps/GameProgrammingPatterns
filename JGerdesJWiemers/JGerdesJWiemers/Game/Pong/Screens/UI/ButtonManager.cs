using JGerdesJWiemers.Game.Engine;
using JGerdesJWiemers.Game.Engine.Graphics;
using JGerdesJWiemers.Game.Engine.Input;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGerdesJWiemers.Game.Pong.Screens.UI
{
  
    class ButtonManager : IUpdateable, IRenderable
    {
        private List<Button> _buttons;
        private bool _controllReleased = true;
        private int _currentSelection;

        public ButtonManager()
        {
            _buttons = new List<Button>();
            InputManager.Channel[0].OnUp += delegate(float val)
            {
                if (val >= 0.3 && _controllReleased)
                {
                    ChangeSelection(-1);
                    _controllReleased = false;
                }
                else
                {
                    if (val == 0)
                    {
                        _controllReleased = true;
                    }
                }
            };
            InputManager.Channel[0].OnDown += delegate(float val)
            {
                if (val >= 0.3 && _controllReleased) 
                {
                    ChangeSelection(1);
                    _controllReleased = false;
                }
                else
                {
                    if (val == 0)
                    {
                        _controllReleased = true;
                    }
                }
                
            };
            InputManager.Channel[0].OnAction1 += delegate(bool pressed)
            {
                if (pressed)
                {
                  _buttons[_currentSelection].Select();
                }
            };
        }

        private void ChangeSelection(int delta)
        {
            _buttons[_currentSelection].Blur();
            _currentSelection += delta;
            if (_currentSelection < 0)
            {
                _currentSelection = _buttons.Count - 1;
            }
            if (_currentSelection >= _buttons.Count)
            {
                _currentSelection = 0;
            }

            _buttons[_currentSelection].Focus();
        }



        public void Update()
        {
            foreach (Button b in _buttons)
            {
                b.Update();
            }
        }

        public void Render(SFML.Graphics.RenderTarget renderTarget, float extra)
        {
            foreach (Button b in _buttons)
            {
                b.Render(renderTarget, extra);
            }
        }

        public void AddButton(Button b)
        {
            _buttons.Add(b);
            if (_buttons.Count == 1)
            {
                b.Focus();
            }
        }
    }
}
