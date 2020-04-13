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
        private Texture2D texture;
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

        public TrainingDummy(Texture2D texture, Vector2 position, Texture2D hitTexture)
        {
            this.hitTexture = hitTexture;
            this.texture = texture;
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

        }
    }
}
