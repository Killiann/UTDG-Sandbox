using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_DEV.Scene_Objects;
using UTDG_DEV.Scene_Objects.Entities;

namespace UTDG_DEV.Handlers
{
    class CollisionHandler
    {
        private readonly int[] collisionMap;
        private readonly Vector2 mapDimensions;
        private readonly int tileSize;
        private Scene scene;

        private enum Direction
        {
            VERTICAL,
            HORIZONTAL
        }

        public CollisionHandler(Scene scene)
        {
            collisionMap = scene.map.GetCollisionMap();
            mapDimensions = scene.map.GetMapDimensions();
            tileSize = scene.game.TileSize;
            this.scene = scene;
        }

        public void Update(DynamicEntity entity)
        {
            //x
            Rectangle xBounds = new Rectangle((int)entity.CollisionBounds().X + (int)Math.Round(entity.velocity.X), (int)entity.CollisionBounds().Y, entity.CollisionBounds().Width, entity.CollisionBounds().Height);
            for(int i = 0; i < collisionMap.Length; i++)
            {
                if(collisionMap[i] == 1)
                {
                    int xPos = i % (int)mapDimensions.X * tileSize;
                    int yPos = i / (int)mapDimensions.X * tileSize;
                    Rectangle tileBounds = new Rectangle(xPos, yPos, tileSize, tileSize);
                    if (tileBounds.Intersects(xBounds))
                    {                       
                        float intersectionDepth = GetHorzDepth(xBounds, tileBounds);
                        xBounds.X += (int)intersectionDepth;
                        entity.SetXPosition(xBounds.X);
                        entity.velocity.X = 0;
                    }
                }
            }

            for (int i = 0; i < scene.LiveEntities.Count; i++)
            {
                if (scene.LiveEntities[i] is CollidableEntity)
                {
                    Rectangle entityCollision = ((CollidableEntity)scene.LiveEntities[i]).CollisionBounds();
                    if (entityCollision.Intersects(xBounds) && scene.LiveEntities[i].GetId() != entity.GetId())
                    {
                        float intersectionDepth = GetHorzDepth(xBounds, entityCollision);
                        xBounds.X += (int)intersectionDepth;
                        entity.SetXPosition(xBounds.X);
                        entity.velocity.X = 0;
                    }
                }
            }

            //y
            Rectangle yBounds = new Rectangle((int)entity.CollisionBounds().X, (int)entity.CollisionBounds().Y + (int)Math.Round(entity.velocity.Y), entity.CollisionBounds().Width, entity.CollisionBounds().Height); 
            for(int i = 0; i < collisionMap.Length; i++)
            {
                if(collisionMap[i] == 1)
                {
                    int xPos = i % (int)mapDimensions.X * tileSize;
                    int yPos = i / (int)mapDimensions.X * tileSize;
                    Rectangle tileBounds = new Rectangle(xPos, yPos, tileSize, tileSize);
                    if (tileBounds.Intersects(yBounds))
                    {
                        float intersectionDepth = GetVertDepth(yBounds, tileBounds);
                        yBounds.Y += (int)intersectionDepth;
                        entity.SetYPosition(yBounds.Y);
                        entity.velocity.Y = 0;
                    }
                }
            }

            //other entities
            for (int i = 0; i < scene.LiveEntities.Count; i++)
            {
                if (scene.LiveEntities[i] is CollidableEntity)
                {
                    Rectangle entityCollision = ((CollidableEntity)scene.LiveEntities[i]).CollisionBounds();
                    if (entityCollision.Intersects(yBounds) && scene.LiveEntities[i].GetId() != entity.GetId())
                    {
                        float intersectionDepth = GetVertDepth(yBounds, entityCollision);
                        yBounds.Y += (int)intersectionDepth;
                        entity.SetYPosition(yBounds.Y);
                        entity.velocity.Y = 0;
                    }
                }
            }
        }

        public bool IsColliding(DynamicEntity entity, out Entity collidedEntity)
        {
            collidedEntity = null;

            //x
            Rectangle xBounds = new Rectangle((int)entity.CollisionBounds().X + (int)Math.Round(entity.velocity.X), (int)entity.CollisionBounds().Y, entity.CollisionBounds().Width, entity.CollisionBounds().Height);
            for (int i = 0; i < collisionMap.Length; i++)
            {
                if (collisionMap[i] == 1)
                {
                    int xPos = i % (int)mapDimensions.X * tileSize;
                    int yPos = i / (int)mapDimensions.X * tileSize;
                    Rectangle tileBounds = new Rectangle(xPos, yPos, tileSize, tileSize);
                    if (tileBounds.Intersects(xBounds))                    
                        return true;                    
                }
            }

            for (int i = 0; i < scene.LiveEntities.Count; i++)
            {
                if (scene.LiveEntities[i] is CollidableEntity)
                {
                    Rectangle entityCollision = ((CollidableEntity)scene.LiveEntities[i]).CollisionBounds();
                    if (entityCollision.Intersects(xBounds) && scene.LiveEntities[i].GetId() != entity.GetId())
                    {
                        collidedEntity = scene.LiveEntities[i];
                        return true;
                    }
                }
            }

            //y
            Rectangle yBounds = new Rectangle((int)entity.CollisionBounds().X, (int)entity.CollisionBounds().Y + (int)Math.Round(entity.velocity.Y), entity.CollisionBounds().Width, entity.CollisionBounds().Height);
            for (int i = 0; i < collisionMap.Length; i++)
            {
                if (collisionMap[i] == 1)
                {
                    int xPos = i % (int)mapDimensions.X * tileSize;
                    int yPos = i / (int)mapDimensions.X * tileSize;
                    Rectangle tileBounds = new Rectangle(xPos, yPos, tileSize, tileSize);
                    if (tileBounds.Intersects(yBounds))         
                        return true;                    
                }
            }

            //other entities
            for (int i = 0; i < scene.LiveEntities.Count; i++)
            {
                if (scene.LiveEntities[i] is CollidableEntity)
                {
                    Rectangle entityCollision = ((CollidableEntity)scene.LiveEntities[i]).CollisionBounds();
                    if (entityCollision.Intersects(yBounds) && scene.LiveEntities[i].GetId() != entity.GetId())
                    {
                        collidedEntity = scene.LiveEntities[i];
                        return true;
                    }
                }
            }            
            return false;
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
