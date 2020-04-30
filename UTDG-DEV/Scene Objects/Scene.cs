using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_DEV.Scene_Objects.Entities;

namespace UTDG_DEV.Scene_Objects
{
    public class Scene
    {
        public Map map;
        public Texture2D mapMask;

        public Camera camera;
        public Main game;
        public Player player;        

        public List<Entity> LiveEntities;

        public Scene(Main game)
        {
            this.game = game;            
            LoadScene();

            camera = new Camera(game.GraphicsDevice.Viewport);
            player = new Player(this, new Vector2(128, 128));

            LiveEntities = new List<Entity>();
            LiveEntities.Add(player);        


            //=================================TEMP====================================
            //temp loading of training dummys
            Random rnd = new Random();
            for(int i = 0; i < 5; i++)
            {
                int x = 0;
                int y = 0;
                while(map.GetCollisionMap()[(y * (int)map.GetMapDimensions().X) + x] == 1)
                {
                    x = rnd.Next(0, (int)map.GetMapDimensions().X);
                    y = rnd.Next(0, (int)map.GetMapDimensions().Y);
                }
                TrainingDummy dummy = new TrainingDummy(this, new Vector2(x * game.TileSize, y * game.TileSize), i + 1);
                LiveEntities.Add(dummy);
            }

            //temporary single entities for testing
            Torch torch = new Torch(this, new Vector2(64, 32), 7);
            Torch torch2 = new Torch(this, new Vector2(160, 32), 8);
            Torch torch3 = new Torch(this, new Vector2(64, 128), 9);
            Chest chest = new Chest(this, new Vector2(128, 64), 10);
            LiveEntities.Add(torch);
            LiveEntities.Add(torch2);
            LiveEntities.Add(torch3);
            LiveEntities.Add(chest);
            //=================================TEMP====================================

            //setup mask for the map
            mapMask = new Texture2D(game.GraphicsDevice, (int)map.GetMapDimensions().X, (int)map.GetMapDimensions().X);
            Color[] Data = new Color[mapMask.Width * mapMask.Height];
            int[] depth = map.GetDepthMap();
            for (int i = 0; i < map.GetDepthMap().Length; i++)
            {
                int x = i % (int)map.GetMapDimensions().X;
                int y = i / (int)map.GetMapDimensions().X;
                if (map.GetDepthMap()[i] == 1)
                {
                    Data[(y * (int)map.GetMapDimensions().X) + x] = Color.White * 0.005f;
                }
                else Data[(y * (int)map.GetMapDimensions().X) + x] = Color.Transparent;
            }
            mapMask.SetData(0, null, Data, 0, Data.Length);
        }

        public void LoadScene()
        {
            string text = File.ReadAllText("map.txt");
            string[] split = text.Split('#');
            int mapW = Convert.ToInt32(split[0].Split(',')[0]);
            int mapH = Convert.ToInt32(split[0].Split(',')[0]);

            int[] tileMap = new int[mapW * mapH];
            int[] collisionMap = new int[mapW * mapH];
            int[] depthMap = new int[mapW * mapH];

            string[] tileMapTest = split[1].Split(',');
            tileMap = Array.ConvertAll(split[1].Split(','), int.Parse);
            collisionMap = Array.ConvertAll(split[2].Split(','), int.Parse);
            depthMap = Array.ConvertAll(split[3].Split(','), int.Parse);

            MapData mapData = new MapData()
            {
                Dimensions = new Vector2(mapW, mapH),
                TileMap = tileMap,
                CollisionMap = collisionMap,
                DepthMap = depthMap
            };

            map = new Map(game, game.textureHandler.dungeonTileMapTexture, mapData);
        }

        public void Update()
        {            
            player.Update();
            camera.SetPosition(player.GetOrigin());
        }

        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            game.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.Stencil, Color.Transparent, 0, 0);

            var m = Matrix.CreateOrthographicOffCenter(0,
                graphics.GraphicsDevice.PresentationParameters.BackBufferWidth,
                graphics.GraphicsDevice.PresentationParameters.BackBufferHeight,
                0, 0, 1
            );            
            var a = new AlphaTestEffect(graphics.GraphicsDevice)
            {
                Projection = m,
                View = camera.TranslationMatrix
            };

            var s1 = new DepthStencilState //radius mask
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.Always,
                StencilPass = StencilOperation.Replace,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };
            //var s2 = new DepthStencilState //behind wall mask
            //{
            //    StencilEnable = true,
            //    StencilFunction = CompareFunction.NotEqual,
            //    //StencilPass = StencilOperation.Replace,
            //    StencilFail = StencilOperation.Replace,
            //    ReferenceStencil = 0,
            //    DepthBufferEnable = false
            //};
            var s3 = new DepthStencilState //rendering texture
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.LessEqual,
                StencilPass = StencilOperation.Keep,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };
                       
            //draw scene
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, camera.TranslationMatrix);
            map.Draw(spriteBatch);
            foreach (Entity entity in LiveEntities)            
                entity.Draw(spriteBatch);
            spriteBatch.End();

            Color spritecol = Color.White * 0.01f;

            ////radius of view around sprite
            //spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, s1, null, a);
            //spriteBatch.Draw(game.textureHandler.player_radius, new Rectangle((int)player.GetPosition().X + 8, (int)player.GetPosition().Y + 8, game.textureHandler.player_radius.Width, game.textureHandler.player_radius.Height), null, spritecol, 0f, new Vector2(game.textureHandler.player_radius.Width / 2, game.textureHandler.player_radius.Height / 2), SpriteEffects.None, 0f); //The mask                                   
            //spriteBatch.End();

            //behind the wall mask
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, s1, null, a);
            spriteBatch.Draw(mapMask, new Rectangle(0, 0, mapMask.Width * game.TileSize, mapMask.Height * game.TileSize), Color.White); //The mask  
            spriteBatch.End();
           
            //behind the wall textures
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, s3, null, a);
            foreach (Entity entity in LiveEntities)
                entity.DrawBehindWalls(spriteBatch);
            spriteBatch.End();
        }
    }
}
