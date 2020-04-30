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
        protected int frameRate;
        protected int idleFrameRate;
        protected readonly int baseFrameRate = 5;
        protected int currentTimer = 0;
        protected readonly Texture2D idleTexture;
        protected Texture2D currentTexture;
        protected bool movingLeft = false;

        //behind wall
        protected Texture2D idleTextureMask;
        protected Texture2D currentMask;

        public AnimationHandler(Main main, Texture2D idleTexture, int? idleFrameRate)
        {
            this.idleTexture = idleTexture;
            if (idleFrameRate != null) this.idleFrameRate = (int)idleFrameRate;
            else this.idleFrameRate = baseFrameRate;

            frameRate = this.idleFrameRate;
            currentTexture = idleTexture;

            idleTextureMask = GetMask(main.GraphicsDevice, idleTexture);
            currentMask = idleTextureMask;
        }

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

        public virtual void Update()
        {
            if (currentTimer < frameRate)
                currentTimer++;
            else
            {
                currentTimer = 0;
                if (currentFrame == (currentTexture.Width / 16) - 1)
                {
                    currentFrame = 0;
                    currentTimer = 0;
                }
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
        private int singleAnimFrameRate;

        public SingleAnimationHandler(Main game, Texture2D idleTexture, int? idleFrameRate, Texture2D singleAnimation, int? singleAnimFrameRate) : base(game, idleTexture, idleFrameRate)
        {
            this.singleAnimation = singleAnimation;

            if (singleAnimFrameRate == null)
                this.singleAnimFrameRate = baseFrameRate;
            else this.singleAnimFrameRate = (int)singleAnimFrameRate;

            singleAnimationMask = GetMask(game.GraphicsDevice, singleAnimation);
        }

        public void RunAnimation()
        {
            currentTexture = singleAnimation;
            frameRate = singleAnimFrameRate;
            currentMask = singleAnimationMask;
            isAnimationRunning = true;
            currentFrame = 0;
            currentTimer = 0;
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
                        frameRate = idleFrameRate;
                    }
                    else currentFrame++;
                }
            }
        }      
    }

    public class ToggleAnimationHandler : AnimationHandler
    {
        private Texture2D singleAnimation;
        private Texture2D singleAnimationMask;
        private int singleAnimationFrameRate;

        private bool IsOn = false;
        private bool IsAnimating = false;

        public ToggleAnimationHandler(Main game, Texture2D idleAnimation, int? idleFrameRate, Texture2D singleAnimation, int? singleAnimationFrameRate) : base(game, idleAnimation, idleFrameRate)
        {
            this.singleAnimation = singleAnimation;
            if (singleAnimationFrameRate != null) this.singleAnimationFrameRate = (int)singleAnimationFrameRate;
            else this.singleAnimationFrameRate = baseFrameRate;

            singleAnimationMask = GetMask(game.GraphicsDevice, this.singleAnimation);
        }

        public bool ToggleOn()
        {
            if (!IsOn && !IsAnimating)
            {
                IsAnimating = true;
                currentFrame = 0;
                currentTimer = 0;
                frameRate = singleAnimationFrameRate;
                currentTexture = singleAnimation;
                currentMask = singleAnimationMask;
                return true;
            }return false;
        }

        public bool ToggleOff()
        {
            if(IsOn && !IsAnimating)
            {
                IsAnimating = true;
                currentTimer = 0;
                return true;
            }return false;
        }

        public override void Update()
        {
            if (IsAnimating)
            {
                if (IsOn)
                {
                    if (currentTimer < frameRate)
                        currentTimer++;
                    else
                    {
                        currentTimer = 0;
                        if (currentFrame == 0)
                        {
                            IsAnimating = false;
                            IsOn = false;
                            frameRate = idleFrameRate;
                            currentTexture = idleTexture;
                            currentMask = idleTextureMask;
                        }
                        else currentFrame--;
                    }
                }
                else
                {
                    if (currentTimer < frameRate)
                        currentTimer++;
                    else
                    {
                        currentTimer = 0;
                        if (currentFrame == (currentTexture.Width / 16) - 1)
                        {
                            IsAnimating = false;
                            IsOn = true;
                        }
                        else currentFrame++;
                    }
                }
            }
        }        
    }

    public class DynamicAnimationHandler : AnimationHandler
    {
        private readonly Texture2D movingXTexture;
        private readonly Texture2D movingUpTexture;
        private readonly Texture2D movingXTextureMask;
        private readonly Texture2D movingUpTextureMask;
        private Vector2 prevDirection;
        private int movingXFrameRate;
        private int movingUpFrameRate;

        public DynamicAnimationHandler(Main game, Texture2D idleTexture, int? idleFrameRate, Texture2D movingXTexture, int? movingXFrameRate, Texture2D movingUpTexture, int? movingUpFrameRate) : base(game, idleTexture, idleFrameRate)
        {
            this.movingXTexture = movingXTexture;
            this.movingUpTexture = movingUpTexture;
            prevDirection = Vector2.Zero;

            if (movingXFrameRate == null) this.movingXFrameRate = baseFrameRate;
            else this.movingXFrameRate = (int)movingXFrameRate;

            if (movingUpFrameRate == null) this.movingUpFrameRate = baseFrameRate;
            else this.movingUpFrameRate = (int)movingUpFrameRate;

            movingXTextureMask = GetMask(game.GraphicsDevice, movingXTexture);
            movingUpTextureMask = GetMask(game.GraphicsDevice, movingUpTexture);
        }

        private void ChangeTexture(Texture2D newTexture, Texture2D newMask, int newFrameRate)
        {
            frameRate = newFrameRate;
            currentTexture = newTexture;
            currentMask = newMask;
            currentFrame = 0;
        }

        public override void Draw(Entity entity, SpriteBatch spriteBatch, float depth)
        {
            DynamicEntity currentEntity = (DynamicEntity)entity;

            //if (currentEntity.velocity != new Vector2(0, 0)) 
            //{
                if (currentEntity.directionVector == Vector2.Zero && prevDirection != Vector2.Zero)
                    ChangeTexture(idleTexture, idleTextureMask, idleFrameRate);
                else if (currentEntity.directionVector.X == 0 && currentEntity.directionVector.Y == -1 &&
                    (currentEntity.directionVector != prevDirection))
                    ChangeTexture(movingUpTexture, movingUpTextureMask, movingUpFrameRate);
                else if ((currentEntity.directionVector.X != 0 || currentEntity.directionVector.Y == 1) && prevDirection.X != currentEntity.directionVector.X || prevDirection.Y != currentEntity.directionVector.Y)
                    ChangeTexture(movingXTexture, movingXTextureMask, movingXFrameRate);

                if (currentEntity.directionVector.X < 0) movingLeft = true;
                else if (currentEntity.directionVector.X > 0) movingLeft = false;
            //}
            //else
            //{
            //    if (currentTexture != idleTexture)
            //        ChangeTexture(idleTexture, idleTextureMask);
            //}
            
            base.Draw(entity, spriteBatch, depth);
            prevDirection = currentEntity.directionVector;
        }
    }
}
