using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_SB.Entities
{
    class Chest
    {
        Vector2 position;
        Texture2D texture;
        Texture2D itemTexture;
        private int closedFrame;
        private int openFrame;

        private bool isOpening = false;
        private bool isClosing = false;

        private int currentFrame = 0;
        private int frameTime = 2;
        private int currentTime = 0;

        private int hoverAmount;
        private int maxHover = 80;
        private bool hoverUp = true;

        public bool IsOpen(){ return currentFrame == openFrame; }

        public Chest(Main game) {
            texture = game.textureHandler.chestOpening;
            itemTexture = game.textureHandler.sword;
            closedFrame = 0;
            openFrame = (texture.Width / 16) - 1;
            position = new Vector2(96, 64);
        }

        public void Open()
        {
            if(isOpening == false && isClosing == false)
                isOpening = true;
        }

        public void Close()
        {
            if (isOpening == false && isClosing == false)
                isClosing = true;
        }

        public void Update()
        {
            if (isOpening)
            {
                if (currentFrame < openFrame)
                {
                    if (currentTime > frameTime)
                    {
                        currentTime = 0;
                        currentFrame++;
                    }
                    else currentTime++;
                }
                else if (currentFrame == openFrame)
                {
                    isOpening = false;
                    currentTime = 0;
                }
            }
            else if (isClosing)
            {
                if (currentFrame > closedFrame)
                {
                    if (currentTime > frameTime)
                    {
                        currentTime = 0;
                        currentFrame--;
                    }
                    else currentTime++;
                }
                else if (currentFrame == closedFrame)
                {
                    isClosing = false;
                    currentTime = 0;
                }                    
            }

            if (hoverUp)
            {
                if (hoverAmount < maxHover)
                {
                    hoverAmount++;
                }
                else hoverUp = false;
            }else if(!hoverUp)
            {
                if (hoverAmount > 0)
                {
                    hoverAmount--;
                }
                else hoverUp = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 32, 32), new Rectangle(currentFrame * 16, 0, 16, 16), Color.White);
            if (IsOpen())
            {
                spriteBatch.Draw(itemTexture, new Rectangle((int)position.X, (int)position.Y - 16 - (hoverAmount / 10), 32, 32), Color.White);
            }
        }
    }
}
