using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace JGerdesJWiemers.Game.Engine.Utils
{
    class AssetLoader
    {
        public static readonly String FONT_ROBOTO_LIGHT = "Roboto-Light.ttf";

        private static AssetLoader _instance;
        private readonly String DIR_FONTS = @"Assets\Fonts\";

        private Dictionary<String, Font> _fonts;
        

        private AssetLoader()
        {
            _fonts = new Dictionary<string, Font>();

            LoadFont(FONT_ROBOTO_LIGHT, FONT_ROBOTO_LIGHT);
        }

        public void LoadFont(String name, String filename)
        {
            try
            {
                Font font = new Font(DIR_FONTS+filename);
                _fonts.Add(name, font);
            }
            catch (SFML.LoadingFailedException lfe)
            {
                /// todo
            }
        }

        public Font getFont(String name){
            return _fonts[name];
        }

        public static AssetLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssetLoader();
                }
                return _instance;
            }
        }
    }
}
