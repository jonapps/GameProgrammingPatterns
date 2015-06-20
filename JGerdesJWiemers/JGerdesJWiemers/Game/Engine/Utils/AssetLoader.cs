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

        
        public static readonly String TEXTURE_EXPLOSION1 = @"explosions\explosion1";

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

            LoadTexture(TEXTURE_EXPLOSION1, TEXTURE_EXPLOSION1);

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
