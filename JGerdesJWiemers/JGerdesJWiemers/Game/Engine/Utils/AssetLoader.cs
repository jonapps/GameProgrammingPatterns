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
        public static readonly String FONT_ROBOTO_THIN = "Roboto-Thin.ttf";
        public static readonly String FONT_ROBOTO_LIGHT = "Roboto-Light.ttf";
        public static readonly String FONT_ROBOTO_REGULAR = "Roboto-Regular.ttf";
        public static readonly String FONT_ROBOTO_MEDIUM = "Roboto-Medium.ttf";

        public static readonly String TEXTURE_ASTRONAUT = "astronaut.png";
        public static readonly String TEXTURE_EARTH = "earth.png";
        public static readonly String TEXTURE_SPACE1 = "space1.jpg";
        public static readonly String TEXTURE_SPACE2 = "space2.png";

        private static AssetLoader _instance;
        private readonly String DIR_FONTS = @"Assets\Fonts\";
        private readonly String DIR_TEXTURES = @"Assets\Graphics\";

        private Dictionary<String, Font> _fonts;
        private Dictionary<String, Texture> _textures;
        

        private AssetLoader()
        {
            _fonts = new Dictionary<string, Font>();
            _textures = new Dictionary<string, Texture>();

            LoadFont(FONT_ROBOTO_THIN, FONT_ROBOTO_THIN);
            LoadFont(FONT_ROBOTO_LIGHT, FONT_ROBOTO_LIGHT);
            LoadFont(FONT_ROBOTO_REGULAR, FONT_ROBOTO_REGULAR);
            LoadFont(FONT_ROBOTO_MEDIUM, FONT_ROBOTO_MEDIUM);

            LoadTexture(TEXTURE_ASTRONAUT, TEXTURE_ASTRONAUT);
            LoadTexture(TEXTURE_EARTH, TEXTURE_EARTH);
            LoadTexture(TEXTURE_SPACE1, TEXTURE_SPACE1);
            LoadTexture(TEXTURE_SPACE2, TEXTURE_SPACE2);
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
