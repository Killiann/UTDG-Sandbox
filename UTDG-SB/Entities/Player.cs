using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_SB.Handlers;
using UTDG_SB.Map;

namespace UTDG_SB.Entities
{
    public class Player
    {
        //handlers
        InputHandler inputHandler;
        public PhysicsHandler physicsHandler;
        CollisionHandler collisionHandler;

        private TileMap map;
        public Game1 game;

        //local vars
        private Vector2 prevPos;
        private Vector2 position;

        public Vector2 GetPosition() { return position; }
        public void SetXPosition(float newPos) { position.X = newPos; }
        public void SetYPosition(float newPos) { position.Y = newPos; }

        private Vector2 velocity;
        private Texture2D texture;
        private Texture2D movingTexture;
        private int currentFrame = 0;
        private int currentIdleFrame = 0;
        private int frameTimer = 0;
        private int frameLength = 5;
        private int idleFrameLength = 50;
        public Rectangle bounds { get { return new Rectangle((int)position.X - 6, (int)position.Y -16, 32, 32); } }

        public Rectangle collisionBounds { get { return new Rectangle((int)position.X, (int)position.Y, 20, 16); } }
            
        private Texture2D collisionBoxTexture;
        public bool facingLeft = false;

        public bool isColliding = false;

        //get/setters
        public Vector2 GetVelocity() { return velocity; }
        public void SetYVelocity(float newVel) { velocity.Y = newVel; }
        public void SetXVelocity(float newVel) { velocity.X = newVel; }        

        public Player(Game1 game)
        {
            this.map = game.map;
            this.game = game;
            
            inputHandler = new InputHandler();
            physicsHandler = new PhysicsHandler();
            collisionHandler = new CollisionHandler(this.map);

            Color[] az = Enumerable.Range(0, 1).Select(i => Color.Red).ToArray();
            collisionBoxTexture = game.textureHandler.colorTexture;
            collisionBoxTexture.SetData(az);

            position = new Vector2(160, 160);
            prevPos = position;

            this.texture = game.textureHandler.playerTexture;
            this.movingTexture = game.textureHandler.playerWalk;            
        }

        public void Update()
        {            
            inputHandler.Update(physicsHandler);
            physicsHandler.Update(this);

            if (velocity.X < 0) facingLeft = true;
            if (velocity.X > 0) facingLeft = false;
            
            collisionHandler.Update(this);            
            position += velocity;
            position.X = (float)Math.Round(position.X);
            position.Y = (float)Math.Round(position.Y);

            //animation
            if (prevPos == position) physicsHandler.isMoving = false;
            if (physicsHandler.isMoving)
            {
                if (prevPos.X == position.X && prevPos.Y > position.Y)
                    movingTexture = game.textureHandler.playerWalkUp;
                else movingTexture = game.textureHandler.playerWalk;

                if (frameTimer < frameLength)
                    frameTimer++;
                else
                {
                    frameTimer = 0;
                    if (currentFrame < (movingTexture.Width / 16) - 1)
                        currentFrame++;
                    else currentFrame = 0;
                }
            }
            else
            {
                currentFrame = 0;
                if (frameTimer < idleFrameLength)
                    frameTimer++;
                else
                {
                    frameTimer = 0;
                    if (currentIdleFrame < (game.textureHandler.playerIdle.Width / 16) - 1)
                        currentIdleFrame++;
                    else currentIdleFrame = 0;
                }
            }
            prevPos = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            if(!physicsHandler.isMoving)
                spriteBatch.Draw(game.textureHandler.playerIdle, bounds, new Rectangle(currentIdleFrame * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.5f);
            else
            {
                spriteBatch.Draw(movingTexture, bounds, new Rectangle(currentFrame * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0.5f);                
            }

            ////draw collision box
            //spriteBatch.Draw(collisionBoxTexture, new Rectangle(collisionBounds.X, collisionBounds.Y, collisionBounds.Width, 2), Color.Red);
            //spriteBatch.Draw(collisionBoxTexture, new Rectangle(collisionBounds.X + collisionBounds.Width - 2, collisionBounds.Y, 2, collisionBounds.Height), Color.Red);
            //spriteBatch.Draw(collisionBoxTexture, new Rectangle(collisionBounds.X, collisionBounds.Y + collisionBounds.Height - 2, collisionBounds.Width, 2), Color.Red);
            //spriteBatch.Draw(collisionBoxTexture, new Rectangle(collisionBounds.X, collisionBounds.Y, 2, collisionBounds.Height), Color.Red);
        }
    }
}
