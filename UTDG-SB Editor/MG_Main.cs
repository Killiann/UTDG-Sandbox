using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using UTDG_SB_Editor.Handlers;

namespace UTDG_SB_Editor
{
    public class MG_Main : MonoGameControl
    {
        public TextureHandler textureHandler;
        Texture2D testTexture;

        public int width = 64;
        public int height = 64;

        public MG_Main()
        {
            textureHandler = new TextureHandler();
        }

        protected override void Initialize()
        {
            base.Initialize();
            textureHandler.LoadContent(this);
            testTexture = textureHandler.playerTexture;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            Editor.spriteBatch.Draw(testTexture, new Rectangle(100, 100, width, height), Color.White);
            Editor.spriteBatch.End();
        }
    }
}
