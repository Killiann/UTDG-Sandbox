using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UTDG_DEV.Handlers;

namespace UTDG_DEV.Scene_Objects.Entities
{
    class TrainingDummy : CollidableEntity
    {
        private Scene scene;
        public override Rectangle RenderBounds() { return new Rectangle((int)position.X - (int)offset.X, (int)position.Y - (int)offset.Y, 32, 32); }
        public override Rectangle CollisionBounds() { return new Rectangle((int)position.X, (int)position.Y, 20, 16); }
        private float Depth() { return scene.player.GetPosition().Y < position.Y ? 0.6f : 0.4f; }
        private SingleAnimationHandler animationHandler;

        public TrainingDummy(Scene scene, Vector2 position, int id)
        {
            this.scene = scene;
            offset = new Vector2(6, 16);
            this.position = position += offset;
            this.id = id;
            
            animationHandler = new SingleAnimationHandler(scene.game, scene.game.textureHandler.dummy_idle, null, scene.game.textureHandler.dummy_hit, 3); 
            entityType = EntityType.SingleAnimation;
        }

        public void Hit()
        {
            animationHandler.RunAnimation();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            animationHandler.Draw(this, spriteBatch, Depth());
        }
        public override void DrawBehindWalls(SpriteBatch spriteBatch)
        {
            animationHandler.DrawBehindWall(this, spriteBatch);
        }
    }
}
