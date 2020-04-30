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
    class Chest : CollidableEntity
    {
        private Scene scene;
        private ToggleAnimationHandler animationHandler;
        private float Depth() { return scene.player.GetPosition().Y < position.Y ? 0.6f : 0.4f; }
        public override Rectangle RenderBounds() { return new Rectangle((int)position.X - (int)offset.X, (int)position.Y - (int)offset.Y, 32, 32); }
        public override Rectangle CollisionBounds() { return new Rectangle((int)position.X, (int)position.Y, 20, 16); }

        public bool isOpen = false;

        public Chest(Scene scene, Vector2 position, int id)
        {
            this.id = id;
            this.scene = scene;
            offset = new Vector2(6, 16);
            this.position = position += offset;            
            animationHandler = new ToggleAnimationHandler(scene.game, scene.game.textureHandler.chest, 0, scene.game.textureHandler.chest_opening, 1);
        }

        public void Open()
        {
            if (!isOpen)
            {
                if (animationHandler.ToggleOn())
                    isOpen = true;
            }
        }

        public void Close()
        {
            if (isOpen)
            {
                if(animationHandler.ToggleOff())
                    isOpen = false;
            }
        }

        public override void DrawBehindWalls(SpriteBatch spriteBatch)
        {
            animationHandler.DrawBehindWall(this, spriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animationHandler.Draw(this, spriteBatch, Depth());
        }        
    }
}
