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
        public static readonly String TEXTURE_BACKGROUND = "background.png";
        public static readonly String TEXTURE_TITLE_BACKGROUND = "title.png";

        private static AssetLoader _instance;
        private readonly String DIR_FONTS = @"Assets\Fonts\";
        private readonly String DIR_TEXTURES = @"Assets\Graphics\";

        private Dictionary<String, Font> _fonts;
        private Dictionary<String, Texture> _textures;
        

        private AssetLoader()
        {
            _fonts = new Dictionary<string, Font>();
            _textures = new Dictionary<string, Texture>();

            LoadFont(FONT_ROBOTO_LIGHT, FONT_ROBOTO_LIGHT);
            LoadTexture(TEXTURE_BACKGROUND, TEXTURE_BACKGROUND);
            LoadTexture(TEXTURE_TITLE_BACKGROUND, TEXTURE_TITLE_BACKGROUND);
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

        public void LoadTexture(String name, String filename)
        {
            Texture texture = new Texture(DIR_TEXTURES+filename);
            _textures.Add(name, texture);

        }

        public Texture getTexture(String name)
        {
            return _textures[name];
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
