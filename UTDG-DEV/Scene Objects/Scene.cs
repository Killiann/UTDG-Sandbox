﻿using Microsoft.Xna.Framework;
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
        public List<Bullet> bullets;

        RangedWeapon testWeapon;

        public Scene(Main game)
        {
            this.game = game;
            LiveEntities = new List<Entity>();
            bullets = new List<Bullet>();
            LoadScene();
            
            camera = new Camera(game.GraphicsDevice.Viewport);

            testWeapon = new RangedWeapon(this, 0, 10, 5, 1, 5, game.textureHandler.rayGun, game.textureHandler.rayGun, game.textureHandler.basicBullet);

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
            //==========MAP==========
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

           //========ENTITY========
            int[] entityMap = Array.ConvertAll(split[4].Split(','), int.Parse);
            int idCounter = 2;
            for(int i = 0; i < entityMap.Length; i++)
            {
                if(entityMap[i] != 0)
                {
                    int xPos = (i % mapW) * game.TileSize;
                    int yPos = (i / mapW) * game.TileSize;
                    switch (entityMap[i])
                    {
                        case 1:
                            player = new Player(this, new Vector2(xPos, yPos));
                            LiveEntities.Add(player);
                            break;
                        case 2:
                            Torch torch = new Torch(this, new Vector2(xPos, yPos), idCounter);
                            LiveEntities.Add(torch);
                            idCounter++;
                            break;
                        case 3:
                            TrainingDummy dummy = new TrainingDummy(this, new Vector2(xPos, yPos), idCounter);
                            LiveEntities.Add(dummy);
                            idCounter++;
                            break;
                        case 4:
                            Chest chest = new Chest(this, new Vector2(xPos, yPos), idCounter);
                            LiveEntities.Add(chest);
                            idCounter++;
                            break;
                    }
                }
            }
        }

        public void Update()
        {
            foreach(Entity e in LiveEntities)
            {
                e.Update();
            }
                    
            if (game.mouseInputHandler.IsLeftDown())
                testWeapon.Fire();
            testWeapon.Update(game.mouseInputHandler.GetRealMousePos());

            int count = bullets.Count;
            for(int i =0;i<count;i++)
            {
                Bullet b = bullets[i];
                if (b.canDestroy)
                {
                    bullets.Remove(bullets[i]);
                    count--;
                    i--;
                }
                b.Update();
            }

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
                TwoSidedStencilMode = true,
                StencilFunction = CompareFunction.Always,
                StencilPass = StencilOperation.Replace,
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };            
            var s2 = new DepthStencilState //rendering texture
            {                
                StencilEnable = true,                
                StencilFunction = CompareFunction.LessEqual,
                StencilPass = StencilOperation.Keep,                
                ReferenceStencil = 1,
                DepthBufferEnable = false,
            };
            var s3 = new DepthStencilState
            {
                StencilEnable = true,
                StencilFunction = CompareFunction.Equal,
                StencilPass = StencilOperation.Keep,
                ReferenceStencil = 1,
                DepthBufferEnable = false
            };

            graphics.PreferMultiSampling = false;
            //graphics.GraphicsDevice.PresentationParameters.MultiSampleCount = 1;

            //draw scene
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointWrap, null, null, null, camera.TranslationMatrix);
            map.Draw(spriteBatch);
            foreach (Entity entity in LiveEntities)            
                entity.Draw(spriteBatch);
            testWeapon.Draw(spriteBatch);
            foreach(Bullet bullet in bullets)            
                bullet.Draw(spriteBatch);            
            spriteBatch.End();

            Color spritecol = Color.White * 0.01f;

            //radius of view around sprite
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, s1, null, a);
            //spriteBatch.Draw(game.textureHandler.player_radius, new Rectangle((int)player.GetPosition().X + 8, (int)player.GetPosition().Y + 8, game.textureHandler.player_radius.Width, game.textureHandler.player_radius.Height), null, spritecol, 0f, new Vector2(game.textureHandler.player_radius.Width / 2, game.textureHandler.player_radius.Height / 2), SpriteEffects.None, 0f); //The mask                                   
            spriteBatch.Draw(mapMask, new Rectangle(0, 0, mapMask.Width * game.TileSize, mapMask.Height * game.TileSize), Color.White); //The mask  
            spriteBatch.End();

            ////behind the wall mask
            //spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, s2, null, a);
            //spriteBatch.End();           

            //behind the wall textures
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, s3, null, a);
            foreach (Entity entity in LiveEntities) 
                entity.DrawBehindWalls(spriteBatch);
            foreach (Bullet b in bullets)
                b.DrawBehindWalls(spriteBatch);
            spriteBatch.End();
                       
            //game.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.Stencil, Color.Transparent, 0, 0);
        }
    }
}
