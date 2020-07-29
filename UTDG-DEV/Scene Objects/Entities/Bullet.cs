using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_DEV.Handlers;

namespace UTDG_DEV.Scene_Objects.Entities
{
    public class Bullet : DynamicEntity
    {
        Texture2D texture;
        CollisionHandler collisionHandler;
        public bool canDestroy = false;
        public AnimationHandler animationHandler;

        public override Rectangle CollisionBounds(){ return new Rectangle((int)position.X, (int)position.Y, 2, 2); }

        public Bullet(Scene scene, Vector2 position, Vector2 velocity, Texture2D texture)
        {
            this.position = position;
            this.velocity = velocity;
            this.texture = texture;
            collisionHandler = new CollisionHandler(scene);
            animationHandler = new AnimationHandler(scene.game, texture, 0, new Vector2(texture.Width / 2, texture.Height / 2));
        }

        public override void Update()
        {
            position += velocity;
            Entity collidingWith = new Entity();
            if (collisionHandler.IsColliding(this, out collidingWith))
            {
                if (!(collidingWith is Player)) canDestroy = true;
                if (collidingWith is TrainingDummy) (collidingWith as TrainingDummy).Hit();
            }
        }

        public override void DrawBehindWalls(SpriteBatch spriteBatch)
        {
            animationHandler.DrawBehindWall(this, spriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 32, 32) , null, Color.White, 0f, new Vector2(texture.Width/2, texture.Height/ 2), SpriteEffects.None, 0.7f);
            animationHandler.Draw(this, spriteBatch, 0.7f);
        }
    }
}
