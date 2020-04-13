using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_SB.Entities;
using UTDG_SB.Map;

namespace UTDG_SB.Handlers
{
    class CollisionHandler
    {
        private TileMap map;
        private enum Direction
        {
            VERTICAL,
            HORIZONTAL
        }
        public CollisionHandler(TileMap map)
        {
            this.map = map;
        }
        public void Update(Player player)
        {
            //world collisions
            //handle X 
            Rectangle xBounds = new Rectangle((int)player.GetPosition().X + (int)Math.Round(player.GetVelocity().X), (int)player.GetPosition().Y, player.collisionBounds.Width, player.collisionBounds.Height);
            for (int i = 0; i < map.collisionMap.Length; i++)
            {
                if (map.collisionMap[i] == 1)
                {
                    int xPos = i % map.mapW * map.tileWidth;
                    int yPos = i / map.mapW * map.tileWidth;
                    Rectangle tileBounds = new Rectangle(xPos, yPos, map.tileWidth, map.tileWidth);
                    if (tileBounds.Intersects(xBounds))
                    {
                        float intersectionDepth = GetHorzDepth(xBounds, tileBounds);
                        xBounds.X += (int)intersectionDepth;
                        player.SetXPosition(xBounds.X);
                        player.SetXVelocity(0);
                    }
                }
            }
            if (player.game.trainingDummy.collisionBounds.Intersects(xBounds))
            {
                float intersectionDepth = GetHorzDepth(xBounds, player.game.trainingDummy.collisionBounds);
                xBounds.X += (int)intersectionDepth;
                player.SetXPosition(xBounds.X);
                player.SetXVelocity(0);
            }

            //Handle Y
            Rectangle yBounds = new Rectangle((int)player.GetPosition().X, (int)player.GetPosition().Y + (int)Math.Round(player.GetVelocity().Y), player.collisionBounds.Width, player.collisionBounds.Height);
            for (int i = 0; i < map.collisionMap.Length; i++)
            {
                if (map.collisionMap[i] == 1)
                {
                    int xPos = i % map.mapW * map.tileWidth;
                    int yPos = i / map.mapW * map.tileWidth;
                    Rectangle tileBounds = new Rectangle(xPos, yPos, map.tileWidth, map.tileWidth);
                    if (tileBounds.Intersects(yBounds))
                    {
                        float intersectionDepth = GetVertDepth(yBounds, tileBounds);
                        yBounds.Y += (int)intersectionDepth;
                        player.SetYPosition(yBounds.Y);
                        player.SetYVelocity(0);
                    }
                }
            }
            if (player.game.trainingDummy.collisionBounds.Intersects(yBounds))
            {
                float intersectionDepth = GetVertDepth(yBounds, player.game.trainingDummy.collisionBounds);
                yBounds.Y += (int)intersectionDepth;
                player.SetYPosition(yBounds.Y);
                player.SetYVelocity(0);
            }

        }
        
        private bool CalculateIntersect(Rectangle player, Rectangle tile, Direction direction, out Vector2 depth)
        {
            depth = Vector2.Zero;
            if (direction == Direction.VERTICAL)
                depth = new Vector2(0, GetVertDepth(player, tile));
            else if (direction == Direction.HORIZONTAL)
                depth = new Vector2(GetHorzDepth(player, tile), 0);
            return depth.Y != 0 || depth.X != 0;
        }
        private float GetHorzDepth(Rectangle rect1, Rectangle rect2)
        {
            float halfW1 = rect1.Width / 2;
            float halfW2 = rect2.Width / 2;
            float centre1 = rect1.X + halfW1;
            float centre2 = rect2.X + halfW2;

            float minDist = halfW1 + halfW2;
            float dist = centre1 - centre2;
            if (Math.Abs(dist) >= minDist)
                return 0f;
            return dist > 0 ? minDist - dist : -minDist - dist;
        }
        private float GetVertDepth(Rectangle rect1, Rectangle rect2)
        {
            float halfH1 = rect1.Height / 2;
            float halfH2 = rect2.Height / 2;
            float centre1 = rect1.Y + halfH1;
            float centre2 = rect2.Y + halfH2;

            float minDist = halfH1 + halfH2;
            float dist = centre1 - centre2;
            if (Math.Abs(dist) >= minDist)
                return 0f;
            return dist > 0 ? minDist - dist : -minDist - dist;
        }
    }
}
