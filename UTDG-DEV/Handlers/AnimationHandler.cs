using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTDG_DEV.Scene_Objects.Entities;

namespace UTDG_DEV.Handlers
{
    public class AnimationHandler
    {
        public int currentFrame = 0;
        protected int frameRate = 5;
        protected int currentTimer = 0;
        protected readonly Texture2D idleTexture;
        protected Texture2D currentTexture;
        protected bool movingLeft = false;

        //behind wall
        protected Texture2D idleTextureMask;
        protected Texture2D currentMask;

        protected Texture2D GetMask(GraphicsDevice graphics, Texture2D original)
        {
            Color[] originalData = new Color[original.Width * original.Height];
            original.GetData(0, original.Bounds, originalData, 0, originalData.Length);
            Texture2D textureMask = new Texture2D(graphics, original.Width, original.Height);
            textureMask.SetData(originalData);

            Color[] tcolor = new Color[textureMask.Width * textureMask.Height];
            Rectangle rect = new Rectangle(0, 0, textureMask.Width, textureMask.Height);
            textureMask.GetData(0, rect, tcolor, 0, tcolor.Length);
            for (int i = 0; i < tcolor.Length; i++)
                if (tcolor[i] != Color.Transparent) tcolor[i] = Color.LightGray * 0.3f;
            textureMask.SetData(0, null, tcolor, 0, tcolor.Length);
            return textureMask;
        }

        public AnimationHandler(Main main, Texture2D idleTexture, int? frameRate)
        {
            this.idleTexture = idleTexture;
            if(frameRate != null) this.frameRate = (int)frameRate;
            currentTexture = idleTexture;

            idleTextureMask = GetMask(main.GraphicsDevice, idleTexture);
            currentMask = idleTextureMask;
        }

        public virtual void Update()
        {
            if (currentTimer < frameRate)
                currentTimer++;
            else
            {
                currentTimer = 0;
                if (currentFrame == (currentTexture.Width / 16) - 1)
                    currentFrame = 0;
                else currentFrame++;
            }
        }

        public virtual void Draw(Entity entity, SpriteBatch spriteBatch, float depth)
        {
            Update();
            spriteBatch.Draw(currentTexture, entity.RenderBounds(), new Rectangle(currentFrame * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, movingLeft == true ? SpriteEffects.FlipHorizontally : SpriteEffects.None, depth);
        }

        public virtual void DrawBehindWall(Entity entity, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentMask, entity.RenderBounds(), new Rectangle(currentFrame * 16, 0, 16, 16), Color.White, 0f, Vector2.Zero, movingLeft == true ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
        }                  
    }

    public class SingleAnimationHandler : AnimationHandler
    {
        private Texture2D singleAnimation;
        private Texture2D singleAnimationMask;
        private bool isAnimationRunning = false;
        public SingleAnimationHandler(Main game, Texture2D idleTexture, int? frameRate, Texture2D singleAnimation) : base(game, idleTexture, frameRate)
        {
            this.singleAnimation = singleAnimation;
            singleAnimationMask = GetMask(game.GraphicsDevice, singleAnimation);
        }

        public void RunAnimation()
        {
            currentTexture = singleAnimation;
            currentMask = singleAnimationMask;
            isAnimationRunning = true;
            currentFrame = 0;            
        }

        public override void Update()
        {
            if(!isAnimationRunning)
                base.Update();
            else
            {
                if (currentTimer < frameRate)
                    currentTimer++;
                else
                {
                    currentTimer = 0;
                    if (currentFrame == (currentTexture.Width / 16) - 1)
                    {
                        isAnimationRunning = false;
                        currentTexture = idleTexture;
                        currentMask = idleTextureMask;
                        currentFrame = 0;
                    }
                    else currentFrame++;
                }
            }
        }

        public override void Draw(Entity entity, SpriteBatch spriteBatch, float depth)
        {
            base.Draw(entity, spriteBatch, depth);
        }        
    }

    public class DynamicAnimationHandler : AnimationHandler
    {
        private readonly Texture2D movingXTexture;
        private readonly Texture2D movingUpTexture;
        private readonly Texture2D movingXTextureMask;
        private readonly Texture2D movingUpTextureMask;
        private Vector2 prevDirection;

        public DynamicAnimationHandler(Main game, Texture2D idleTexture, int? frameRate, Texture2D movingXTexture, Texture2D movingUpTexture) : base(game, idleTexture, frameRate)
        {
            this.movingXTexture = movingXTexture;
            this.movingUpTexture = movingUpTexture;
            prevDirection = Vector2.Zero;

            movingXTextureMask = GetMask(game.GraphicsDevice, movingXTexture);
            movingUpTextureMask = GetMask(game.GraphicsDevice, movingUpTexture);
        }

        private void ChangeTexture(Texture2D newTexture, Texture2D newMask)
        {
            currentTexture = newTexture;
            currentMask = newMask;
            currentFrame = 0;
        }

        public override void Draw(Entity entity, SpriteBatch spriteBatch, float depth)
        {
            DynamicEntity currentEntity = (DynamicEntity)entity;

            if (currentEntity.directionVector == Vector2.Zero && prevDirection != Vector2.Zero)
                ChangeTexture(idleTexture, idleTextureMask);
            else if (currentEntity.directionVector.X == 0 && currentEntity.directionVector.Y == -1 &&
                (currentEntity.directionVector != prevDirection))
                ChangeTexture(movingUpTexture, movingUpTextureMask);
            else if ((currentEntity.directionVector.X != 0 || currentEntity.directionVector.Y == 1) && prevDirection.X != currentEntity.directionVector.X || prevDirection.Y != currentEntity.directionVector.Y)
                ChangeTexture(movingXTexture,movingXTextureMask);

            if (currentEntity.directionVector.X < 0) movingLeft = true;
            else if (currentEntity.directionVector.X > 0) movingLeft = false;

            base.Draw(entity, spriteBatch, depth);
            prevDirection = currentEntity.directionVector;
        }
    }
}
