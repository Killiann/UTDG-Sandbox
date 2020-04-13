using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_SB.Entities
{
    public class TrainingDummy
    {
        private Main game;
        private Texture2D texture;
        private Texture2D collisionBoxTexture;
        private Vector2 position;
        public Vector2 GetPosition() { return position; }        
        public Rectangle bounds { get { return new Rectangle((int)position.X, (int)position.Y, 32, 32); } }
        public Rectangle collisionBounds { get { return new Rectangle((int)position.X + 6, (int)position.Y + 16, 20, 16); } }

        public float depth = 0.7f;

        private bool isHit;
        private Texture2D hitTexture;
        private int currentFrame;
        private int frameLength = 3;
        private int frameTimer = 0;

        public TrainingDummy( Main game, Vector2 position)
        {
            hitTexture = game.textureHandler.trainingDummyHit;
            texture = game.textureHandler.trainingDummyTexture;

            this.game = game;

            Color[] az = Enumerable.Range(0, 1).Select(i => Color.Red).ToArray();
            collisionBoxTexture = game.textureHandler.colorTexture;
            collisionBoxTexture.SetData(az);

            this.position = position;
        }

        public void Hit()
        {
            frameTimer = 0;
            currentFrame = 0;
            isHit = true;            
        }

        public void Update()
        {
            if (isHit)
            {
                if (currentFrame == hitTexture.Width / 16 - 1)
                    isHit = false;

                if (frameTimer == frameLength)
                {
                    frameTimer = 0;
                    currentFrame++;
                }

                frameTimer++;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isHit)
                spriteBatch.Draw(hitTexture, bounds, new Rectangle(currentFrame * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, SpriteEffects.None, depth);
            else
                spriteBatch.Draw(texture, bounds, null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, depth);

            if (game.devMode)
            {
                spriteBatch.Draw(collisionBoxTexture, new Rectangle(collisionBounds.X, collisionBounds.Y, collisionBounds.Width, 2), Color.Red);
                spriteBatch.Draw(collisionBoxTexture, new Rectangle(collisionBounds.X + collisionBounds.Width - 2, collisionBounds.Y, 2, collisionBounds.Height), Color.Red);
                spriteBatch.Draw(collisionBoxTexture, new Rectangle(collisionBounds.X, collisionBounds.Y + collisionBounds.Height - 2, collisionBounds.Width, 2), Color.Red);
                spriteBatch.Draw(collisionBoxTexture, new Rectangle(collisionBounds.X, collisionBounds.Y, 2, collisionBounds.Height), Color.Red);
            }
        }
    }
}
