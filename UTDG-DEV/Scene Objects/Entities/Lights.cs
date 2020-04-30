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
    class Torch : Entity
    {
        private AnimationHandler animationHandler;
        private Scene scene;

        public Torch(Scene scene, Vector2 position, int id)
        {
            this.id = id;
            this.scene = scene;
            this.position = position;
            animationHandler = new AnimationHandler(scene.game, scene.game.textureHandler.torch, 3);
        }

        public override void DrawBehindWalls(SpriteBatch spriteBatch)
        {
            animationHandler.DrawBehindWall(this, spriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            animationHandler.Draw(this, spriteBatch, 0.4f);
        }
    }
}
