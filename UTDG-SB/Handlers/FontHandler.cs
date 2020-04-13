using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTDG_SB.Handlers
{
    public class FontHandler
    {
        public SpriteFont devFont;

        public void LoadContent(Game game)
        {
            devFont = game.Content.Load<SpriteFont>("fonts/devFont");
        }
    }
}
