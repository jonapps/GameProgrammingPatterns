using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using JGerdesJWiemers.Game.Engine.Graphics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using JGerdesJWiemers.Game.Engine.Utils.Helper;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using JGerdesJWiemers.Game.Engine.Audio;

namespace JGerdesJWiemers.Game.Engine.Utils
{
    class AssetLoader
    {
        public static readonly String DATA_FILE_ENDING = "json";

        public static readonly String FONT_ROBOTO_THIN = "Roboto-Thin.ttf";
        public static readonly String FONT_ROBOTO_LIGHT = "Roboto-Light.ttf";
        public static readonly String FONT_ROBOTO_REGULAR = "Roboto-Regular.ttf";
        public static readonly String FONT_ROBOTO_MEDIUM = "Roboto-Medium.ttf";
        public static readonly String FONT_DIGITAL = "digital-7-mono.ttf";

        public static readonly String TEXTURE_UI = "UI";
        public static readonly String TEXTURE_UI_EARTH = "ui_earth";
        public static readonly String TEXTURE_UI_SHUTTLE = "ui_spaceshuttle";
        public static readonly String TEXTURE_UI_SINGLE = @"ui\single";
        public static readonly String TEXTURE_UI_DOUBLE = @"ui\double";
        public static readonly String TEXTURE_UI_MISSILE = @"ui\missile";

        public static readonly String TEXTURE_ASTRONAUT = "astronaut";
        public static readonly String TEXTURE_EARTH = "earth";
        public static readonly String TEXTURE_EARTH_BIG = "earth_big";
        public static readonly String TEXTURE_EARTH_TOP = "earth_top";
        public static readonly String TEXTURE_EARTH_FULL = "earth_full";
        public static readonly String TEXTURE_MOON_FULL = "moon_full";
        public static readonly String TEXTURE_MOON = "moon";
        public static readonly String TEXTURE_SPACESHIP = "spaceshuttle";
        public static readonly String TEXTURE_SPACESHIP_LARGE = "spaceshuttle_large";
        public static readonly String GATLINGUN_BULLET = "gatlinGunBullet";
        public static readonly String TEXTURE_MISSILE= "missile";
        public static readonly String TEXTURE_SPACE1 = @"space\space1";
        public static readonly String TEXTURE_SPACE2 = @"space\space2";
        public static readonly String TEXTURE_SPACE3 = @"space\space3";
        public static readonly String TEXTURE_ASTEROID1 = @"asteroids\asteroid1";
        public static readonly String TEXTURE_ASTEROID2 = @"asteroids\asteroid2";
        public static readonly String TEXTURE_ASTEROID3 = @"asteroids\asteroid3";
        public static readonly String TEXTURE_EXPLOSION1 = @"explosions\explosion1";

        public static readonly String AUDIO_ASTRONAUT1 = @"Assets\Audio\astronaut1.wav";
        public static readonly String AUDIO_ASTRONAUT2 = @"Assets\Audio\astronaut2.wav";
        public static readonly String AUDIO_ASTRONAUT3 = @"Assets\Audio\astronaut3.wav";
        public static readonly String AUDIO_HOUSTON = @"Assets\Audio\houston-problem.wav";

        public static readonly String CONFIG_INPUT = "input.json";

        private static AssetLoader _instance;
        private readonly String DIR_FONTS = @"Assets\Fonts\";
        private readonly String DIR_TEXTURES = @"Assets\Graphics\";
        private readonly String DIR_SETTINGS = @"Assets\Configuration";

        private Dictionary<String, Font> _fonts;
        private Dictionary<String, TextureContainer> _textures;
        

        private AssetLoader()
        {
            _fonts = new Dictionary<string, Font>();
            _textures = new Dictionary<string, TextureContainer>();

            LoadFont(FONT_ROBOTO_THIN, FONT_ROBOTO_THIN);
            LoadFont(FONT_ROBOTO_LIGHT, FONT_ROBOTO_LIGHT);
            LoadFont(FONT_ROBOTO_REGULAR, FONT_ROBOTO_REGULAR);
            LoadFont(FONT_ROBOTO_MEDIUM, FONT_ROBOTO_MEDIUM);
            LoadFont(FONT_DIGITAL, FONT_DIGITAL);

            LoadTexture(TEXTURE_UI, TEXTURE_UI);
            LoadTexture(TEXTURE_UI_EARTH, TEXTURE_UI_EARTH);
            LoadTexture(TEXTURE_UI_SHUTTLE, TEXTURE_UI_SHUTTLE);
            LoadTexture(TEXTURE_UI_SINGLE, TEXTURE_UI_SINGLE);
            LoadTexture(TEXTURE_UI_DOUBLE, TEXTURE_UI_DOUBLE);
            LoadTexture(TEXTURE_UI_MISSILE, TEXTURE_UI_MISSILE);

            LoadTexture(TEXTURE_ASTRONAUT, TEXTURE_ASTRONAUT);
            LoadTexture(TEXTURE_EARTH, TEXTURE_EARTH);
            LoadTexture(TEXTURE_EARTH_BIG, TEXTURE_EARTH_BIG);
            LoadTexture(TEXTURE_EARTH_TOP, TEXTURE_EARTH_TOP);
            LoadTexture(TEXTURE_EARTH_FULL, TEXTURE_EARTH_FULL);
            LoadTexture(TEXTURE_MOON_FULL, TEXTURE_MOON_FULL);
            LoadTexture(TEXTURE_MOON, TEXTURE_MOON);
            LoadTexture(TEXTURE_SPACE1, TEXTURE_SPACE1);
            LoadTexture(TEXTURE_SPACE2, TEXTURE_SPACE2);
            LoadTexture(TEXTURE_SPACE3, TEXTURE_SPACE3);
            LoadTexture(TEXTURE_SPACESHIP, TEXTURE_SPACESHIP);
            LoadTexture(TEXTURE_SPACESHIP_LARGE, TEXTURE_SPACESHIP_LARGE);
            LoadTexture(TEXTURE_MISSILE, TEXTURE_MISSILE);
            LoadTexture(TEXTURE_ASTEROID1, TEXTURE_ASTEROID1);
            LoadTexture(TEXTURE_ASTEROID2, TEXTURE_ASTEROID2);
            LoadTexture(TEXTURE_ASTEROID3, TEXTURE_ASTEROID3);
            LoadTexture(TEXTURE_EXPLOSION1, TEXTURE_EXPLOSION1);

            AudioManager.Instance.AddSound(AUDIO_ASTRONAUT1, AUDIO_ASTRONAUT1);
            AudioManager.Instance.AddSound(AUDIO_ASTRONAUT2, AUDIO_ASTRONAUT2);
            AudioManager.Instance.AddSound(AUDIO_ASTRONAUT3, AUDIO_ASTRONAUT3);
            AudioManager.Instance.AddSound(AUDIO_HOUSTON, AUDIO_HOUSTON);
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

        public String ReadConfig(String name)
        {
            String filepath = DIR_SETTINGS + "\\" +name;
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            String content = file.ReadToEnd();
            file.Close();
            return content;
        }

        public void WriteConfig(String name, String content)
        {
            String filepath = DIR_SETTINGS + "\\" + name;
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath);
            file.Write(content);
            file.Close();
        }

        public TextureContainer LoadTexture(String name)
        {
            return LoadTexture(name, name);
        }

        public TextureContainer LoadTexture(String name, String filename)
        {
            if (!_textures.ContainsKey(name))
            {
                int width = 0, height = 0;
                Texture texture = null;
                TextureContainer container = null;
                string type = "undefined";

                string filepath = DIR_TEXTURES + filename + "." + DATA_FILE_ENDING;
                System.IO.StreamReader file = new System.IO.StreamReader(filepath);
                string directory = filepath.Substring(0, filepath.LastIndexOf('\\'));
                string completeFile = file.ReadToEnd();
                file.Close();

                SpriteAsset spriteAsset = JsonConvert.DeserializeObject<SpriteAsset>(completeFile);
                texture = new Texture(directory + "\\" + spriteAsset.ImageTitle);
                type = spriteAsset.Type;
                width = spriteAsset.Width;
                height = spriteAsset.Height;

                if (type == TextureContainer.IDENTIFIER)
                {
                    container = new TextureContainer(texture, width, height);
                    _textures.Add(name, container);
                    return container;
                }
                else if (type == RectangleTextureContainer.IDENTIFIER)
                {
                    container = new RectangleTextureContainer(texture, width, height);
                    _textures.Add(name, container);
                    return container;
                } 
                else if (type == CircleTextureContainer.IDENTIFIER)
                {
                    float radius = spriteAsset.Radius;
                    container = new CircleTextureContainer(texture, width, height, radius);
                    _textures.Add(name, container);
                    return container;
                } 
                else if (type == PolygonTextureContainer.IDENTIFIER)
                {
                    Vertices vert = new Vertices();

                    foreach (SpriteVectorAsset sva in spriteAsset.Vertices)
                    {
                        vert.Add(new Vector2(sva.X, sva.Y));
                    }

                    container = new PolygonTextureContainer(texture, width, height, vert);
                }
                if(container != null){
                    _textures.Add(name, container);
                    return container;
                }
            }   
            return _textures[name];

        }

        public TextureContainer getTexture(String name)
        {
            return _textures[name];
        }

        public List<TextureContainer> getAllTextures()
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
