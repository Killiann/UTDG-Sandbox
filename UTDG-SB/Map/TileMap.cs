using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        public int mapW = 11, mapH = 11, tileWidth = 32;
        public int[] tempMapLayout = new int[] {
             12, 7, 7, 7, 7, 7, 7, 7, 7, 8, 9,
             15, 6, 6, 6, 6, 6, 6, 6, 6, 6, 16,
             15, 1, 5, 5, 5, 5, 5, 5, 5, 4, 16,
             15, 2, 0, 0, 0, 0, 0, 0, 0, 3, 16,
             15, 2, 0, 0, 0, 0, 0, 0, 0, 3, 16,
             15, 2, 0, 0, 0, 0, 0, 0, 0, 3, 16,
             15, 2, 0, 0, 0, 0, 0, 0, 0, 3, 16,
             15, 2, 0, 0, 0, 0, 0, 0, 0, 3, 16,
             15, 2, 0, 0, 0, 0, 0, 0, 0, 3, 16,
             15, 2, 0, 0, 0, 0, 0, 0, 0, 3, 16,
             11, 14, 13, 13, 13, 13, 13, 13, 13, 13, 10 };

        public int[] collisionMap = new int[]
        {
            1,1,1,1,1,1,1,1,1,1,1,
            1,1,1,1,1,1,1,1,1,1,1,
            1,0,0,0,0,0,0,0,0,0,1,
            1,0,0,0,0,0,0,0,0,0,1,
            1,0,0,0,0,0,0,0,0,0,1,
            1,0,0,0,0,0,0,0,0,0,1,
            1,0,0,0,0,0,0,0,0,0,1,
            1,0,0,0,0,0,0,0,0,0,1,
            1,0,0,0,0,0,0,0,0,0,1,
            1,0,0,0,0,0,0,0,0,0,1,
            1,0,0,0,0,0,0,0,0,0,1,
            1,1,1,1,1,1,1,1,1,1,1
        };

        public int[] depthMap = new int[]
        {
            9,9,9,9,9,9,9,9,9,9,9,
            9,9,9,9,9,9,9,9,9,9,9,
            9,9,9,9,9,9,9,9,9,9,9,
            9,9,9,9,9,9,9,9,9,9,9,
            9,9,9,9,9,9,9,9,9,9,9,
            9,9,9,9,9,9,9,9,9,9,9,
            9,9,9,9,9,9,9,9,9,9,9,
            9,9,9,9,9,9,9,9,9,9,9,
            9,9,9,9,9,9,9,9,9,9,9,
            9,9,9,9,9,9,9,9,9,9,9,
            9,1,1,1,1,1,1,1,1,1,9            
        };

        public TileMap(Main game)
        {
            this.game = game;
            tileMap = game.textureHandler.tileMapTexture;
            Color[] az = Enumerable.Range(0, 1).Select(i => Color.Blue).ToArray();
            collisionTexture = game.textureHandler.colorTexture;
            collisionTexture.SetData(az);
        }                        

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw background
            int startPos = -320;
            for(int y= 0; y < 30; y++)
            {
                for(int x = 0; x < 30; x++)
                {
                    spriteBatch.Draw(tileMap, new Rectangle(startPos + (x * tileWidth), startPos + (y * tileWidth), tileWidth, tileWidth), new Rectangle(17 * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.99f);
                }
            }

            //draw tilemap
            int currentTile = 0;
            for(int y = 0; y < mapW; y++)
            {
                for(int x=0; x < mapH; x++)
                {
                    spriteBatch.Draw(tileMap, new Rectangle(x * tileWidth, y * tileWidth, tileWidth, tileWidth), new Rectangle(tempMapLayout[currentTile] * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, SpriteEffects.None, (float)depthMap[currentTile] / 10f);
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
