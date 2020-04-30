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
    public class Player : DynamicEntity
    {       
        //textures
        private Texture2D tempTexture;
        private Scene scene;
        
        private PlayerInputHandler inputHandler;
        private PhysicsHandler physicsHandler;
        private CollisionHandler collisionHandler;
        public AnimationHandler animationHandler;

        public Player(Scene scene, Vector2 SpawnPos)
        {
            position = SpawnPos;
            offset = new Vector2(6, 16);
            position += offset;
            id = 1;

            this.scene = scene;
            tempTexture = scene.game.textureHandler.redTexture;

            maxVelocity = 3f;
            acceleration = 0.2f;

            inputHandler = new PlayerInputHandler();
            physicsHandler = new PhysicsHandler();
            collisionHandler = new CollisionHandler(scene);
            animationHandler = new DynamicAnimationHandler(scene.game, scene.game.textureHandler.player_idle, 20, scene.game.textureHandler.player_moveX, null, scene.game.textureHandler.player_moveUp, null);
        }

        public override Rectangle CollisionBounds(){ return new Rectangle((int)position.X, (int)position.Y, 20, 16); }
        public override Rectangle RenderBounds() { return new Rectangle((int)position.X - (int)offset.X, (int)position.Y - (int)offset.Y, 32, 32); }
        public Vector2 GetOrigin() { return new Vector2(position.X + 16, position.Y + 16); }

        public override void Update()        
        {
            inputHandler.Update(this);
            physicsHandler.Update(this);
            collisionHandler.Update(this);

            position += velocity;
            position = new Vector2((float)Math.Round(position.X), (float)Math.Round(position.Y));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {            
            animationHandler.Draw(this, spriteBatch, 0.5f);
        }
        public override void DrawBehindWalls(SpriteBatch spriteBatch)
        {
            animationHandler.DrawBehindWall(this, spriteBatch);
        }
    }
}
