using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UTDG_DEV.Handlers;
using UTDG_DEV.Scene_Objects;

namespace UTDG_DEV
{    
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public TextureHandler textureHandler;

        public readonly int TileSize = 32;

        public Scene tempScene;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            textureHandler = new TextureHandler();
            base.Initialize();
            tempScene = new Scene(this);
        }        
        
        protected override void LoadContent()
        {         
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureHandler.LoadContent(this);
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            tempScene.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            tempScene.Draw(graphics, spriteBatch);
            
            base.Draw(gameTime);
        }
    }
}
