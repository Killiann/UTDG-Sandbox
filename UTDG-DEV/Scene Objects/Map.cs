using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_DEV.Scene_Objects
{

    public class MapData
    {
        public Vector2 Dimensions { get; set; }        
        public int[] TileMap { get; set; }
        public int[] CollisionMap { get; set; }
        public int[] DepthMap { get; set; }
    }

    public class Map
    {
        private MapData mapData;
        public int[] GetCollisionMap() { return mapData.CollisionMap; }
        public int[] GetDepthMap() { return mapData.DepthMap; }
        public Vector2 GetMapDimensions() { return mapData.Dimensions; }

        private Texture2D tileMapTexture;

        private Main game;
        private int tileSize;

        public Map(Main game, Texture2D tileMapTexture, MapData mapData)
        {
            this.game = game;
            this.tileSize = game.TileSize;
            this.tileMapTexture = tileMapTexture;
            this.mapData = mapData;
        }        

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw background tiles
            for (int y = -50; y < mapData.Dimensions.Y + 50; y++) {
                for (int x = -50; x < mapData.Dimensions.X + 50; x++)
                {
                    spriteBatch.Draw(tileMapTexture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), new Rectangle(((tileMapTexture.Width / 16) - 1) * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                }
            }

            //draw tilemap
            for (int i = 0; i < mapData.TileMap.Length; i++){
                int x = i % (int)mapData.Dimensions.X;
                int y = i / (int)mapData.Dimensions.X;
                spriteBatch.Draw(tileMapTexture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), new Rectangle(mapData.TileMap[i] * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, SpriteEffects.None, (mapData.DepthMap[i] == 0 ? 2 : 9) / 10f);
            }
        }
    }
}
