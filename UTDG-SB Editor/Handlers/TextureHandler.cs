using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;

namespace UTDG_SB_Editor.Handlers
{
    public class TextureHandler
    {
        public Texture2D tileMapTexture;
        public Texture2D playerTexture;
        public Texture2D trainingDummyTexture;

        public Texture2D collisionMap;
        public Texture2D depthMap;

        public Texture2D colorTexture;

        public void LoadContent(MonoGameControl main)
        {
            tileMapTexture = main.Editor.Content.Load<Texture2D>("images/dungeontiles");
            playerTexture = main.Editor.Content.Load<Texture2D>("images/player");
            trainingDummyTexture = main.Editor.Content.Load<Texture2D>("images/testdummy");

            collisionMap = main.Editor.Content.Load<Texture2D>("images/collisionMap");
            depthMap = main.Editor.Content.Load<Texture2D>("images/depthMap");

            colorTexture = new Texture2D(main.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        }
    }
}
