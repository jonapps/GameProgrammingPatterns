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


        public static readonly String TEXTURE_GUY = @"guy";
        public static readonly String TEXTURE_TOWER_BASE = @"tower\tower_base";
        public static readonly String TEXTURE_TOWER_TOP = @"tower\tower_top";
        public static readonly String TEXTURE_BULLET = @"weapons\bullet";
        public static readonly String TEXTURE_SHADOW = @"shadow";

        public static readonly String CONFIG_INPUT = "input.json";

        private static AssetLoader _instance;
        private readonly String DIR_FONTS = @"Assets\Fonts\";
        private readonly String DIR_TEXTURES = @"Assets\Graphics\";
        private readonly String DIR_MAPS = @"Assets\Maps\";
        private readonly String DIR_SETTINGS = @"Assets\Configuration";

        private Dictionary<String, Font> _fonts;
        private Dictionary<String, TextureContainer> _textures;
        

        private AssetLoader()
        {
            _fonts = new Dictionary<string, Font>();
            _textures = new Dictionary<string, TextureContainer>();

            LoadTexture(TEXTURE_GUY, TEXTURE_GUY);
            LoadTexture(TEXTURE_SHADOW, TEXTURE_SHADOW);
            LoadTexture(TEXTURE_TOWER_BASE, TEXTURE_TOWER_BASE);
            LoadTexture(TEXTURE_TOWER_TOP, TEXTURE_TOWER_TOP);
            LoadTexture(TEXTURE_BULLET, TEXTURE_BULLET);

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
        public MapAsset LoadMap(string pathToMap)
        {
            string filepath = DIR_MAPS + pathToMap;
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            string directory = filepath.Substring(0, filepath.LastIndexOf('\\'));
            string completeFile = file.ReadToEnd();
            file.Close();
            JObject jsonObj = JObject.Parse(completeFile);
           
            MapAsset mapAsset = JsonConvert.DeserializeObject<MapAsset>(completeFile);

            // Because of senseless object notation in tiled exported json
            JToken jobject = jsonObj["tilesets"][0]["tiles"];
            IJEnumerable<JToken> values = jobject.Values();
            int size = values.Count();
            foreach (JToken token in values)
            {
                String str = token.ToString();
                TileImageAsset imageAsset = JsonConvert.DeserializeObject<TileImageAsset>(str);
                Texture tex = new Texture(DIR_TEXTURES + imageAsset.Image);
                if (!_textures.ContainsKey(imageAsset.Image))
                {
                    _textures.Add(imageAsset.Image, new TextureContainer(tex));
                }
                
                // we are working with only one tileset. if not refactor this!
                mapAsset.TileSets[0].TileImages.Add(imageAsset);
            }
            return mapAsset;
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
