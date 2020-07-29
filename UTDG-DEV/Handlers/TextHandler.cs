using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_DEV.Handlers
{
    public class Message
    {
        private Vector2 position;
        private SpriteFont font;
        public string Text;

        public Message(SpriteFont font, string text, Vector2 position)
        {
            Text = text;
            this.font = font;
            this.position = position;
        }        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Text, position, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
        }
    }
}
