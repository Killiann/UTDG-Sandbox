using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;

namespace UTDG_SB_Editor.Handlers
{
    public class TextureHandler
    {
        public Texture2D tileMapTexture;
        public Texture2D playerTexture;
        //public Texture2D playerWalk;
        //public Texture2D playerWalkUp;
        //public Texture2D playerIdle;
        public Texture2D trainingDummyTexture;
        //public Texture2D trainingDummyHit;
        //public Texture2D colorTexture;

        //public Texture2D consoleBG;
        //public Texture2D consoleSelectedBG;

        //public Texture2D cursorTexture;

        public void LoadContent(MG_Main main)
        {
            tileMapTexture = main.Editor.Content.Load<Texture2D>("images/dungeontiles");
            playerTexture = main.Editor.Content.Load<Texture2D>("images/player");
            //playerWalk = game.Content.Load<Texture2D>("images/player_walk");
            //playerWalkUp = game.Content.Load<Texture2D>("images/player_walk_up");
            //playerIdle = game.Content.Load<Texture2D>("images/player_idle");
            trainingDummyTexture = main.Editor.Content.Load<Texture2D>("images/testdummy");
            //trainingDummyHit = game.Content.Load<Texture2D>("images/testdummy_hit");
            //cursorTexture = game.Content.Load<Texture2D>("images/cursor");

            //consoleBG = game.Content.Load<Texture2D>("images/console_bg");
            //consoleSelectedBG = game.Content.Load<Texture2D>("images/console_selected_bg");

            //colorTexture = new Texture2D(game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        }
    }
}
