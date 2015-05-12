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
        public static readonly String TEXTURE_MOON = "moon.png";
        public static readonly String TEXTURE_SPACESHIP = "spaceshuttle.png";
        public static readonly String TEXTURE_SPACE1 = "space/space1.jpg";
        public static readonly String TEXTURE_SPACE2 = "space/space2.png";
        public static readonly String TEXTURE_SPACE3 = "space/space3.png";
        public static readonly String TEXTURE_ASTEROID1 = "asteroids/asteroid1.png";
        public static readonly String TEXTURE_ASTEROID2 = "asteroids/asteroid2.png";
        public static readonly String TEXTURE_ASTEROID3 = "asteroids/asteroid3.png";

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
            LoadTexture(TEXTURE_MOON, TEXTURE_MOON);
            LoadTexture(TEXTURE_SPACE1, TEXTURE_SPACE1);
            LoadTexture(TEXTURE_SPACE2, TEXTURE_SPACE2);
            LoadTexture(TEXTURE_SPACE3, TEXTURE_SPACE3);
            LoadTexture(TEXTURE_ASTEROID1, TEXTURE_ASTEROID1);
            LoadTexture(TEXTURE_ASTEROID2, TEXTURE_ASTEROID2);
            LoadTexture(TEXTURE_ASTEROID3, TEXTURE_ASTEROID3);
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

        public Texture LoadTexture(String name)
        {
            return LoadTexture(name, name);
        }

        public Texture LoadTexture(String name, String filename)
        {
            if (!_textures.ContainsKey(name))
            {
                Texture texture = new Texture(DIR_TEXTURES + filename);
                _textures.Add(name, texture);
            }
            return _textures[name];

        }

        public Texture getTexture(String name)
        {
            return _textures[name];
        }

        public List<Texture> getAllTextures()
        {
            return _textures.Values.ToList();
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
