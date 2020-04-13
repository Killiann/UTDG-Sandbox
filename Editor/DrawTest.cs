using Microsoft.Xna.Framework;
using MonoGame.Forms.Controls;

namespace Editor
{
    public class DrawTest : MonoGameControl
    {
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw()
        {
            base.Draw();

            Editor.spriteBatch.Begin();            
            Editor.spriteBatch.End();
        }
    }
}
