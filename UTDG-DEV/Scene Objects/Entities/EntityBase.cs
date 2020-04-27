using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_DEV.Scene_Objects.Entities
{
    public class Entity
    {
        protected int id;
        protected Vector2 position;
        protected Rectangle renderBounds;

        public virtual int GetId() { return id; }
        public virtual Vector2 GetPosition() { return position; }       
        public virtual void SetXPosition(float newPos) { position.X = newPos; }
        public virtual void SetYPosition(float newPos) { position.Y = newPos; }
        public virtual Rectangle RenderBounds() { return renderBounds; }

        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void DrawBehindWalls(SpriteBatch spriteBatch) { }
    }

    public class CollidableEntity : Entity
    {
        protected Vector2 offset;
        protected Rectangle collisionBounds;
        public virtual Rectangle CollisionBounds() { return collisionBounds; }
        public virtual Vector2 GetOffset() { return offset; }
    }

    public class DynamicEntity : CollidableEntity
    {
        public Vector2 velocity = new Vector2(0, 0);
        protected float maxVelocity;
        protected float acceleration;

        public Vector2 directionVector;
       
        public virtual float GetMaxVelocity() { return maxVelocity; }
        public virtual float GetAcceleration() { return acceleration; }
    }
}
