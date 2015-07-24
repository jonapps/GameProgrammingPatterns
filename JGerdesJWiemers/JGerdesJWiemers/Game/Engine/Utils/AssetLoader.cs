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
using JGerdesJWiemers.Game.Engine.Utils.Helper.LevelAssets;
using System.IO;
using System.Media;
using NAudio.Wave;

namespace JGerdesJWiemers.Game.Engine.Utils
{
    class AssetLoader
    {

        private static readonly String _LEVEL_FILE_MAP = "\\map.json";
        private static readonly String _LEVEL_FILE_WAVES = "\\waves.json";
        private static readonly String _LEVEL_FILE_ENEMIES = "\\enemies.json";
        private static readonly String _LEVEL_FILE_TOWER = "\\tower.json";
        private static readonly String _LEVEL_FILE_INFO = "\\info.json";
        private static readonly String _LEVEL_FILE_PREVIEW = "\\preview.png";
        private static readonly String _LEVEL_FILE_MUSIC = "\\music.mp3";

        public static readonly String DATA_FILE_ENDING = "json";
        public static readonly String TEXTURE_TOWER_BASE = @"tower\tower_base";
        public static readonly String TEXTURE_TOWER_TOP = @"tower\tower_top";
        public static readonly String TEXTURE_BULLET = @"weapons\bullet";
        public static readonly String TEXTURE_SHADOW = @"shadow";

        public static readonly String TEXTURE_UI_TOWER_SELECTION_BUTTON = @"ui\tower_button";
        public static readonly String TEXTURE_UI_TOWER_SELECTION_TOP = @"ui\tower_button_top";

        public static readonly String TEXTURE_UI_ICON_ENEGRY = @"ui\icon_energy";
        public static readonly String TEXTURE_UI_ICON_MISSED = @"ui\icon_missed";
        public static readonly String TEXTURE_UI_ICON_MONEY = @"ui\icon_money";
        public static readonly String TEXTURE_UI_ICON_WARN = @"ui\icon_warn";

        public static readonly String TEXTURE_SPLAH_AWSM = @"ui\awsm";

        public static readonly String FONT_ROBOTO_THIN = @"Roboto-Thin.ttf";

        public static readonly String AUDIO_SHOT_1 = @"shot_1.wav";
        public static readonly String AUDIO_SHOT_2 = @"shot_2.wav";
        public static readonly String AUDIO_SHOT_3 = @"shot_3.wav";
        public static readonly String AUDIO_SHOT_4 = @"shot_4.wav";
        public static readonly String AUDIO_SHOT_5 = @"shot_5.wav";
        public static readonly String AUDIO_BUILD = @"build.wav";
        public static readonly String AUDIO_BUILD_NOT = @"build_not.wav";
        public static readonly String AUDIO_MISSED = @"missed.wav";
        public static readonly String AUDIO_START_LEVEL = @"start_level.wav";
        public static readonly String AUDIO_SELECT_LEVEL = @"select_level.wav";
        public static readonly String AUDIO_SELECT_TOWER = @"select_tower.wav";

        public static readonly String AUDIO_MUSIC_1 = @"music\1.mp3";

        public static readonly String CONFIG_INPUT = "input.json";

        private static AssetLoader _instance;
        private readonly String DIR_FONTS = @"Assets\Fonts\";
        private readonly String DIR_LEVELS = @"Assets\Levels\";
        private readonly String DIR_ENEMIES = @"Assets\Enemies\";
        private readonly String DIR_TEXTURES = @"Assets\Graphics\";
        private readonly String DIR_AUDIO = @"Assets\Audio\";
        private readonly String DIR_MAPS = @"Assets\Maps\";
        private readonly String DIR_SETTINGS = @"Assets\Configuration";

        private Dictionary<String, Font> _fonts;
        private Dictionary<String, TextureContainer> _textures;
        private Dictionary<String, CachedSound> _sounds;
        private List<LevelAsset> _levels;
        

        private AssetLoader()
        {
            _fonts = new Dictionary<string, Font>();
            _textures = new Dictionary<string, TextureContainer>();
            _sounds = new Dictionary<string, CachedSound>();
            _levels = new List<LevelAsset>();

            LoadFont(FONT_ROBOTO_THIN, FONT_ROBOTO_THIN);

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

        public void LoadSound(String name, String filename){
            _sounds.Add(name, new CachedSound(DIR_AUDIO + filename));
        }

        public Font getFont(String name){
            return _fonts[name];
        }

        public CachedSound GetSound(String name)
        {
            return _sounds[name];
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

                string filepath = DIR_TEXTURES + filename + "." + DATA_FILE_ENDING;
                System.IO.StreamReader file = new System.IO.StreamReader(filepath);
                string directory = filepath.Substring(0, filepath.LastIndexOf('\\'));
                string completeFile = file.ReadToEnd();
                file.Close();

                SpriteAsset spriteAsset = JsonConvert.DeserializeObject<SpriteAsset>(completeFile);
                texture = new Texture(directory + "\\" + spriteAsset.ImageTitle);

                width = (int)texture.Size.X / (spriteAsset.SpriteSheet == null ? 1 : spriteAsset.SpriteSheet.x);
                height = (int)texture.Size.Y / (spriteAsset.SpriteSheet == null ? 1 : spriteAsset.SpriteSheet.y);

                if (spriteAsset.Collider == null)
                {
                    container = new TextureContainer(texture, width, height);
                    _textures.Add(name, container);
                    return container;
                }
                else if (spriteAsset.Collider.Type == RectangleTextureContainer.IDENTIFIER)
                {
                    //default size
                    Vector2 center = new Vector2(width/2f, height - width/4f);
                    if(spriteAsset.Center != null){
                        center.X = spriteAsset.Center.x;
                        center.Y = spriteAsset.Center.y;
                    }
                    container = new RectangleTextureContainer(texture, width, height, center, new Vector2(spriteAsset.Collider.Width, spriteAsset.Collider.Height) * 24 * 2);
                    _textures.Add(name, container);
                    return container;
                }
                else if (spriteAsset.Collider.Type == CircleTextureContainer.IDENTIFIER)
                {
                    //default size
                    Vector2 center = new Vector2(width / 2f, height - width / 4f);
                    if (spriteAsset.Center != null)
                    {
                        center.X = spriteAsset.Center.x;
                        center.Y = spriteAsset.Center.y;
                    }
                    container = new CircleTextureContainer(texture, width, height, center, spriteAsset.Collider.Radius * 24);
                    _textures.Add(name, container);
                    return container;
                } 
                if(container != null){
                    _textures.Add(name, container);
                    return container;
                }
            }   
            return _textures[name];
        }

        /// <summary>
        /// loads a MapAsset from json file
        /// </summary>
        /// <param name="pathToMap"></param>
        /// <returns></returns>
        private MapAsset _LoadMap(string filepath)
        {
            string completeFile = _ReadFileCompletely(filepath);
            JObject jsonObj = JObject.Parse(completeFile);
            MapAsset mapAsset = JsonConvert.DeserializeObject<MapAsset>(completeFile);
            // Because of senseless object notation in tiled exported json
            JToken jobject, colorTiles = null;
            jobject = jsonObj["tilesets"];
            for (int i = 0; i < jobject.Count(); ++i)
            {
                if (jobject[i]["name"].ToString() == "Underground")
                {
                    colorTiles = jsonObj["tilesets"][i];
                }
            }
            if (colorTiles == null)
            {
                throw new Exception("No Underground defined in Map json");
            }
            TilesetsAsset animationSet = mapAsset.TileSets.FirstOrDefault(t => t.Name == "Animation");
            TilesetsAsset colorSet = mapAsset.TileSets.FirstOrDefault(t => t.Name == "Underground");

            IJEnumerable<JToken> values = colorTiles.Values();

            int textureNumber = 0;

            foreach (JToken token in colorTiles["tiles"])
            {
                String str = token.ToList()[0].ToString();
                TileImageAsset imageAsset = JsonConvert.DeserializeObject<TileImageAsset>(str);
                Texture tex = new Texture(DIR_TEXTURES + imageAsset.Image);
                Color texCol = GetColor(tex);

                imageAsset.Color = texCol;
                imageAsset.Image = animationSet.Image;
                imageAsset.Number = colorSet.Firstgid + textureNumber++;
                colorSet.TileImages.Add(imageAsset);
            }
            Texture aniTex = new Texture(DIR_TEXTURES + animationSet.Image);
            if (!_textures.ContainsKey(animationSet.Image))
            {
                _textures.Add(animationSet.Image, new TextureContainer(aniTex));
            }            
            return mapAsset;
        }

        public void LoadEnemyTextures()
        {

            string[] pathes = Directory.GetFiles(DIR_ENEMIES);
            foreach (string path in pathes)
            {
                //only use json files
                string filename = System.IO.Path.GetFileNameWithoutExtension(path);
                if(System.IO.Path.GetExtension(path).Equals("."+DATA_FILE_ENDING) && !filename.Equals("enemies"))
                {
                    
                    LoadTexture(filename, @"..\Enemies\"+filename);
                }
            }
        }

        private Color GetColor(Texture tex)
        {
            for (uint x = 0; x < tex.Size.X; ++x)
            {
                for (uint y = 0; y < tex.Size.Y; ++y)
                {
                    Color c = tex.CopyToImage().GetPixel(x, y);
                    if (c.A == 255)
                    {
                        return c;
                    }
                }
            }
            throw new Exception("Every tile needs a color");
        }


        public TextureContainer getTexture(String name)
        {
            return _textures[name];
        }

        public List<TextureContainer> getAllTextures()
        {
            return _textures.Values.ToList();
        }

        public List<LevelAsset> GetLevels()
        {
            return _levels;
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



        public string[] GetLevelDirectories()
        {
           return Directory.GetDirectories(this.DIR_LEVELS);
        }



        /// <summary>
        /// Load a level from a given diretory
        /// </summary>
        /// <returns></returns>
        public LevelAsset LoadLevel(String dir)
        {
            bool alright = true;

            if (!File.Exists(dir + _LEVEL_FILE_INFO)) alright = false;
            if (!File.Exists(dir + _LEVEL_FILE_MAP)) alright = false;
            if (!File.Exists(dir + _LEVEL_FILE_WAVES)) alright = false;
            if (!File.Exists(dir + _LEVEL_FILE_ENEMIES)) alright = false;
            if (!File.Exists(dir + _LEVEL_FILE_TOWER)) alright = false;
            if (!File.Exists(dir + _LEVEL_FILE_PREVIEW)) alright = false;
            if (!File.Exists(dir + _LEVEL_FILE_MUSIC)) alright = false;
           
            if (!alright)
            {
                throw new Exception("some levelfiles are missing for '" + dir +  "'");
            }

            LevelAsset level = new LevelAsset();
            level.Info= this._LoadInfo(dir + _LEVEL_FILE_INFO);
            level.Map = this._LoadMap(dir + _LEVEL_FILE_MAP);
            level.Enemies = this._LoadEnemies(dir + _LEVEL_FILE_ENEMIES);
            level.Waves = this._LoadWaves(dir + _LEVEL_FILE_WAVES);
            level.Tower = this._LoadTower(dir + _LEVEL_FILE_TOWER);
            level.Info.Preview = new Texture(dir + _LEVEL_FILE_PREVIEW);
            level.Info.Music = new CachedSound(dir + _LEVEL_FILE_MUSIC);

            _levels.Add(level);

            return level;
        }

        private InfoAsset _LoadInfo(string filepath)
        {
            return JsonConvert.DeserializeObject<InfoAsset>(_ReadFileCompletely(filepath));
        }

        private List<TowerDefence.Entities.Tower.Def> _LoadTower(string filepath)
        {
            return JsonConvert.DeserializeObject <TowerAsset>(_ReadFileCompletely(filepath)).Tower;
        }

        /// <summary>
        /// load WavesAsset from filepath
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private WavesAsset _LoadWaves(string filepath)
        {
            return JsonConvert.DeserializeObject<WavesAsset>(_ReadFileCompletely(filepath)); 
        }



        /// <summary>
        /// load EnemiesAsset from filepath
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private EnemiesAsset _LoadEnemies(string filepath)
        {
            return JsonConvert.DeserializeObject<EnemiesAsset>(_ReadFileCompletely(filepath));
        }

        /// <summary>
        /// Loads a complete textfile as string
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static string _ReadFileCompletely(string filepath)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            string directory = filepath.Substring(0, filepath.LastIndexOf('\\'));
            string completeFile = file.ReadToEnd();
            file.Close();
            return completeFile;
        }


    }
}
