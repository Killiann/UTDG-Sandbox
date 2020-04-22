using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_SB.Map
{
    public class TileMap
    {
        private Main game;
        private Texture2D tileMap;
        private Texture2D collisionTexture;
        public int mapW, mapH, tileWidth = 32;
        public int[] tempMapLayout;
        public int[] collisionMap;
        public int[] depthMap;

        public TileMap(Main game)
        {
            this.game = game;
            tileMap = game.textureHandler.tileMapTexture;
            Color[] az = Enumerable.Range(0, 1).Select(i => Color.Blue).ToArray();
            collisionTexture = game.textureHandler.colorTexture;
            collisionTexture.SetData(az);
            LoadMap();
        }                        

        private void LoadMap()
        {
            string text = File.ReadAllText("map.txt");
            string[] split = text.Split('#');
            mapW = Convert.ToInt32(split[0].Split(',')[0]);
            mapH = Convert.ToInt32(split[0].Split(',')[0]);

            tempMapLayout = new int[mapW * mapH];
            collisionMap = new int[mapW * mapH];
            depthMap = new int[mapW * mapH];

            string[] tileMapTest = split[1].Split(',');
            tempMapLayout = Array.ConvertAll(split[1].Split(','), int.Parse);
            collisionMap = Array.ConvertAll(split[2].Split(','), int.Parse);
            depthMap = Array.ConvertAll(split[3].Split(','), int.Parse);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw background
            int startPos = -320;
            for(int y= 0; y < 30; y++)
            {
                for(int x = 0; x < 30; x++)
                {
                    spriteBatch.Draw(tileMap, new Rectangle(startPos + (x * tileWidth), startPos + (y * tileWidth), tileWidth, tileWidth), new Rectangle(((tileMap.Width / 16) -1) * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.99f);
                }
            }

            //draw tilemap
            int currentTile = 0;
            for(int y = 0; y < mapW; y++)
            {
                for(int x=0; x < mapH; x++)
                {
                    spriteBatch.Draw(tileMap, new Rectangle(x * tileWidth, y * tileWidth, tileWidth, tileWidth), new Rectangle(tempMapLayout[currentTile] * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, SpriteEffects.None, (depthMap[currentTile] == 0? 9 : 1) / 10f);
                    currentTile++;
                }
            }

            //draw collidable bounds
            if (game.devMode)
            {
                for (int i = 0; i < collisionMap.Length; i++)
                {
                    if (collisionMap[i] == 1)
                    {
                        int y = (i / mapW) * tileWidth;
                        int x = (i % mapW) * tileWidth;
                        spriteBatch.Draw(collisionTexture, new Rectangle(x, y, tileWidth, tileWidth), Color.White * 0.5f);
                    }
                }
            }
        }
    }
}
